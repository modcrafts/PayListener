using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HttpListener = System.Net.HttpListener;
using HttpListenerContext = System.Net.HttpListenerContext;

namespace PayListener
{
    internal class WeChatService
    {
        public class HttpListenerPostValue
        {
            /// <summary>
            /// 0=> 参数
            /// 1=> 文件
            /// </summary>
            public int type = 0;
            public string name;
            public byte[] datas;
        }

        public class List
        {
            public string from { get; set; }
            public string content { get; set; }
        }

        public static Socket currentSocket; 
        private static HttpListener httpPostRequest = new HttpListener();
        public static bool Run(int port)
        {
            try
            {
                IPEndPoint ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //绑定端口和IP
                socket.Bind(ipe);
                //设置监听
                socket.Listen(10);
                //连接客户端
                AsyncAccept(socket);
                currentSocket = socket;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Stop()
        {
            try
            {
                if (currentSocket == null)
                    return true;

                try
                {
                    currentSocket.Shutdown(SocketShutdown.Both);
                }
                catch
                {
                }

                try
                {
                    currentSocket.Disconnect(true);
                }
                catch
                {
                }

                try
                {
                    currentSocket.Dispose();
                }
                catch
                {
                }

                try
                {
                    currentSocket.Close();
                }
                catch
                {
                }

                return true;
            }
            catch (Exception)
            {
                //throw;
                return false;
            }
            
        }
        private static void AsyncAccept(Socket socket)
        {
            socket.BeginAccept(asyncResult =>
            {
                try
                {
                    //获取客户端套接字
                    Socket client = socket.EndAccept(asyncResult);
                    AsyncReveive(client);
                }
                catch (Exception)
                {
                    return;
                }
            }, null);
        }

        private static void AsyncReveive(Socket socket)
        {
            byte[] data = new byte[1024];
            try
            {
                //开始接收消息
                socket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    try { int length = socket.EndReceive(asyncResult); }catch (Exception) { return; } //不想处理异常,直接丢了好了 XD
                    
                    List MessageInfoList = JsonConvert.DeserializeObject<List>(Encoding.UTF8.GetString(data));

                    if (MessageInfoList.from.IndexOf("gh_") == 0 || MessageInfoList.from == "notifymessage")
                    {
                        Regex regex = new Regex(@"<!\[CDATA\[微信支付收款(?<amount>[\s\S]*?)元\(朋友到店\)]]>[\s\S]*?付款方备注(?<note>[\s\S]*?)汇总今日第[\s\S]*?<pub_time>(?<time>[\s\S]*?)<\/pub_time>", RegexOptions.IgnoreCase);
                        Match match = regex.Match(MessageInfoList.content);
                        if (match.Success)
                        {
                            string amount = match.Groups["amount"].Value;
                            string time = match.Groups["time"].Value;
                            string note = match.Groups["note"].Value;

                            string state = RemoteService.AppPush(time, "1", amount);
                            if (state != "上报成功") state = "失败: " + state;
                            DataRow dr = Program.dataTable.NewRow();
                            DateTime.TryParse(time, out DateTime datetime);
                            dr[0] = datetime.ToString("yyyy-MM-dd hh:mm:ss");
                            dr[1] = amount;
                            dr[2] = note;
                            dr[3] = state;
                        }
                    }

                }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void AsyncSend(Socket client, string p)
        {
            if (client == null || p == string.Empty) return;
            //数据转码
            byte[] data = new byte[1024];
            data = Encoding.UTF8.GetBytes(p);
            try
            {
                //开始发送消息
                client.BeginSend(data, 0, data.Length, SocketFlags.None, asyncResult =>
                {
                    //完成消息发送
                    int length = client.EndSend(asyncResult);
                    //输出消息
                    Console.WriteLine(string.Format("服务器发出消息:{0}", p));
                }, null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
