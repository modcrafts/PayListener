using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        private static HttpListener httpPostRequest = new HttpListener();
        public static void Run(int port)
        {
            httpPostRequest.Prefixes.Add("http://127.0.0.1:" + port + "/wechat/");
            httpPostRequest.Start();

            Thread ThrednHttpPostRequest = new Thread(new ThreadStart(httpPostRequestHandle));
            ThrednHttpPostRequest.Start();
        }
        private static void httpPostRequestHandle()
        {
            while (true)
            {
                HttpListenerContext requestContext = httpPostRequest.GetContext();
                Thread threadsub = new Thread(new ParameterizedThreadStart((requestcontext) =>
                {
                    HttpListenerContext request = (HttpListenerContext)requestcontext;

                    //获取Post请求中的参数和值帮助类
                    HttpListenerPostParaHelper httppost = new HttpListenerPostParaHelper(request);
                    //获取Post过来的参数和数据
                    List<HttpListenerPostValue> lst = httppost.GetHttpListenerPostValue();

                    string from = "";
                    string content = "";
                    string suffix = "";
                    string adType = "";

                    //使用方法
                    foreach (var key in lst)
                    {
                            string value = Encoding.UTF8.GetString(key.datas).Replace("\r\n", "");
                            if (key.name == "from")
                            {
                                from = value;
                            }
                            if (key.name == "content")
                            {
                                content = value;
                            }
                            /*
                            string fileName = request.Request.QueryString["FileName"];
                            if (!string.IsNullOrEmpty(fileName))
                            {
                                string filePath = AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyMMdd_HHmmss_ffff") + Path.GetExtension(fileName).ToLower();
                                if (key.name == "File")
                                {
                                    FileStream fs = new FileStream(filePath, FileMode.Create);
                                    fs.Write(key.datas, 0, key.datas.Length);
                                    fs.Close();
                                    fs.Dispose();
                                }
                            }
                            */
                    }

                    //Response
                    request.Response.StatusCode = 200;
                    request.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    request.Response.ContentType = "application/json";
                    requestContext.Response.ContentEncoding = Encoding.UTF8;
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = "true", msg = "提交成功" }));
                    request.Response.ContentLength64 = buffer.Length;
                    var output = request.Response.OutputStream;
                    output.Write(buffer, 0, buffer.Length);
                    output.Close();

                    if (from == "gh_3dfda90e39d6")
                    {
                        Regex regex = new Regex(@"<!\[CDATA\[微信支付收款(?<amount>[\s\S]*?)元\(朋友到店\)]]>[\s\S]*?付款方备注(?<note>[\s\S]*?)汇总今日第[\s\S]*?<pub_time>(?<time>[\s\S]*?)<\/pub_time>", RegexOptions.IgnoreCase);
                        Match match = regex.Match(content);
                        if (match.Success)
                        {
                            string amount = match.Groups["amount"].Value;
                            string time = match.Groups["time"].Value;

                        }
                    }
                }));
                threadsub.Start(requestContext);
            }
        }

        /// <summary>
        /// 获取Post请求中的参数和值帮助类
        /// </summary>
        public class HttpListenerPostParaHelper
        {
            private HttpListenerContext request;

            public HttpListenerPostParaHelper(HttpListenerContext request)
            {
                this.request = request;
            }

            private bool CompareBytes(byte[] source, byte[] comparison)
            {
                try
                {
                    int count = source.Length;
                    if (source.Length != comparison.Length)
                        return false;
                    for (int i = 0; i < count; i++)
                        if (source[i] != comparison[i])
                            return false;
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            private byte[] ReadLineAsBytes(Stream SourceStream)
            {
                var resultStream = new MemoryStream();
                while (true)
                {
                    int data = SourceStream.ReadByte();
                    resultStream.WriteByte((byte)data);
                    if (data == 10)
                        break;
                }
                resultStream.Position = 0;
                byte[] dataBytes = new byte[resultStream.Length];
                resultStream.Read(dataBytes, 0, dataBytes.Length);
                return dataBytes;
            }

            /// <summary>
            /// 获取Post过来的参数和数据
            /// </summary>
            /// <returns></returns>
            public List<HttpListenerPostValue> GetHttpListenerPostValue()
            {
                try
                {
                    List<HttpListenerPostValue> HttpListenerPostValueList = new List<HttpListenerPostValue>();
                    if (request.Request.ContentType.Length > 20 && string.Compare(request.Request.ContentType.Substring(0, 20), "multipart /form-data;", true) == 0)
                    {
                        string[] HttpListenerPostValue = request.Request.ContentType.Split(';').Skip(1).ToArray();
                        string boundary = string.Join(";", HttpListenerPostValue).Replace("boundary=", "").Trim();
                        byte[] ChunkBoundary = Encoding.UTF8.GetBytes("--" + boundary + "\r\n");
                        byte[] EndBoundary = Encoding.UTF8.GetBytes("--" + boundary + "--\r\n");
                        Stream SourceStream = request.Request.InputStream;
                        var resultStream = new MemoryStream();
                        bool CanMoveNext = true;
                        HttpListenerPostValue data = null;
                        while (CanMoveNext)
                        {
                            byte[] currentChunk = ReadLineAsBytes(SourceStream);
                            if (!Encoding.UTF8.GetString(currentChunk).Equals("\r\n"))
                                resultStream.Write(currentChunk, 0, currentChunk.Length);
                            if (CompareBytes(ChunkBoundary, currentChunk))
                            {
                                byte[] result = new byte[resultStream.Length - ChunkBoundary.Length];
                                resultStream.Position = 0;
                                resultStream.Read(result, 0, result.Length);
                                CanMoveNext = true;
                                if (result.Length > 0)
                                    data.datas = result;
                                data = new HttpListenerPostValue();
                                HttpListenerPostValueList.Add(data);
                                resultStream.Dispose();
                                resultStream = new MemoryStream();

                            }
                            else if (Encoding.UTF8.GetString(currentChunk).Contains("Content-Disposition"))
                            {
                                byte[] result = new byte[resultStream.Length - 2];
                                resultStream.Position = 0;
                                resultStream.Read(result, 0, result.Length);
                                CanMoveNext = true;
                                data.name = Encoding.UTF8.GetString(result).Replace("Content-Disposition: form-data; name=\"", "").Replace("\"", "").Split(';')[0];
                                resultStream.Dispose();
                                resultStream = new MemoryStream();
                            }
                            else if (Encoding.UTF8.GetString(currentChunk).Contains("Content-Type"))
                            {
                                CanMoveNext = true;
                                data.type = 1;
                                resultStream.Dispose();
                                resultStream = new MemoryStream();
                            }
                            else if (CompareBytes(EndBoundary, currentChunk))
                            {
                                byte[] result = new byte[resultStream.Length - EndBoundary.Length - 2];
                                resultStream.Position = 0;
                                resultStream.Read(result, 0, result.Length);
                                data.datas = result;
                                resultStream.Dispose();
                                CanMoveNext = false;
                            }
                        }
                    }
                    return HttpListenerPostValueList;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }
    }
}
