using dotnetCampus.Configurations;
using dotnetCampus.Configurations.Core;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using Quartz.Impl;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Data;

namespace PayListener
{
    public partial class Form1 : Form
    {
        private readonly static IAppConfigurator Configurator = ConfigurationFactory.FromFile(@".\config.coin").CreateAppConfigurator();
        public Form1()
        {
            InitializeComponent();

            var config = Configurator.Of<StateConfiguration>();
            remoteHostInput.Text = config.CallbackHost;
            remoteKeyInput.Text = config.CallbackKey;
            wechat_add_input.Text = config.WeChatFolder;
            wechat_port_input.Text = config.WeChatListenerPort.ToString();
            //CheckForIllegalCrossThreadCalls = false; 允许跨线程

            DataColumn c1 = new DataColumn("时间", typeof(long));
            DataColumn c2 = new DataColumn("金额", typeof(string));
            DataColumn c3 = new DataColumn("备注", typeof(string));
            DataColumn c4 = new DataColumn("上报", typeof(string));
            Program.dataTable.Columns.Add(c1);
            Program.dataTable.Columns.Add(c2);
            Program.dataTable.Columns.Add(c3);
            Program.dataTable.Columns.Add(c4);
            data_wechat_View.DataSource = Program.dataTable.DefaultView;  //DataGridView绑定数据源
            data_wechat_View.AllowUserToAddRows = false;		//删除空行

            new Task(async () =>
            {
                while (true)
                {
                    Delay(20000);
                    Action set = () => { label_lasthb.Text = HeartBeatJob.LastStatus;};
                    label_lasthb.Invoke(set);
                }
            }).Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (remoteHostInput.Text == "" || remoteKeyInput.Text == "")
            {
                SetLabelText(remoteTipLabel, "请填写远端地址/通信密钥!", Color.Red);
                return;
            }
            SetLabelText(remoteTipLabel, "正在发送心跳请求...", Color.Blue);
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            string sign = MD5Encrypt32(timestamp + remoteKeyInput.Text);
            var data = new JsonObject();
            data.Add("t", timestamp);
            data.Add("sign", sign.ToLower());
            new Task(async () =>
            {
                try
                {
                    string res = await Post("http://" + remoteHostInput.Text + "/appHeart", data);
                    if (res == "" || res == null)
                    {
                        remoteTipLabel.Invoke(new Action<Label, string, Color>(SetLabelText),
                                                 remoteTipLabel, "网址无效或无法访问", Color.Red);
                        return;
                    }
                    
                    var json = JsonNode.Parse(res);
                    if (json["code"]?.ToString() == "1")
                    {
                        remoteTipLabel.Invoke(new Action<Label, string, Color>(SetLabelText),
                                                 remoteTipLabel, "心跳包测试成功", Color.Green);
                    }
                    else
                    {
                        remoteTipLabel.Invoke(new Action<Label, string, Color>(SetLabelText),
                                                 remoteTipLabel, json["msg"], Color.Red);
                    }
                }
                catch (Exception a)
                {
                    remoteTipLabel.Invoke(new Action<Label, string, Color>(SetLabelText),
                                                 remoteTipLabel, "发生错误", Color.Red);
                    MessageBox.Show("发生未预料的错误:\n" + a.Message);
                    //throw;
                }
                
                
            }).Start();
            

        }
        public static void Delay(int milliSecond)
        {
            int start = Environment.TickCount;
            while (Math.Abs(Environment.TickCount - start) < milliSecond)
            {
                Application.DoEvents();
            }
        }

        public static string MD5Encrypt32(string txt)
        {
            byte[] sor = Encoding.UTF8.GetBytes(txt);
            MD5 md5 = MD5.Create();
            byte[] result = md5.ComputeHash(sor);
            StringBuilder strbul = new StringBuilder(40);
            for (int i = 0; i < result.Length; i++)
            {
                //加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
                strbul.Append(result[i].ToString("x2"));
            }
            return strbul.ToString();
        }

        public async Task<string> Post(string url, JsonObject data)
        {
            HttpClient httpClient = new HttpClient();
            var httpContent = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, httpContent);
            var result = await response.Content.ReadAsStringAsync();
            httpContent.Dispose();
            return result;
        }

        private async void SetLabelText(Label txt, string value, Color color)
        {
            txt.ForeColor = color;
            txt.Text = value;
            new Task(async () =>
            {
                Delay(2000);
                if (txt.Text == value)
                {
                    Action set = () => { txt.Text = ""; txt.ForeColor = Color.Black; };
                    txt.Invoke(set);
                }
            }).Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var config = Configurator.Of<StateConfiguration>();
            config.CallbackHost = remoteHostInput.Text;
            config.CallbackKey = remoteKeyInput.Text;
            SetLabelText(remoteTipLabel, "配置已保存", Color.Green);
        }

        private async void button_startbeat_Click(object sender, EventArgs e)
        {
            var config = Configurator.Of<StateConfiguration>();
            if (config.CallbackHost == "" || config.CallbackKey == "")
            {
                SetLabelText(remoteTipLabel, "请先设置回调地址/Key", Color.Red);
                return;
            }
            if (button_startbeat.Text == "启动心跳上报")
            {
                if(await RemoteService.HeartBeatManagerAsync(true))
                {
                    SetLabelText(remoteTipLabel, "心跳上报启动成功", Color.Green);
                    label_heartbeat_1.ForeColor = Color.Green;
                    label_heartbeat_1.Text = "心跳上报运行中";
                    button_startbeat.Text = "停止心跳上报";
                }
                else
                {
                    SetLabelText(remoteTipLabel, "心跳上报启动失败", Color.Red);
                }
            }
            else
            {
                if(await RemoteService.HeartBeatManagerAsync(false))
                {
                    SetLabelText(remoteTipLabel, "心跳上报已停止", Color.Green);
                    label_heartbeat_1.ForeColor = Color.CornflowerBlue;
                    label_heartbeat_1.Text = "心跳上报未运行";
                    button_startbeat.Text = "启动心跳上报";
                }
                else
                {
                    SetLabelText(remoteTipLabel, "心跳上报停止失败", Color.Red);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                wechat_add_input.Text = fbd.SelectedPath;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "https://www.123pan.com/s/XfT9-YxPod");
        }

        [DllImport("kernel32.dll")]
        public static extern int VirtualAllocEx(IntPtr hwnd, int lpaddress, int size, int type, int tect);

        [DllImport("kernel32.dll")]
        public static extern int WriteProcessMemory(IntPtr hwnd, int baseaddress, string buffer, int nsize, int filewriten);

        [DllImport("kernel32.dll")]
        public static extern int GetProcAddress(int hwnd, string lpname);

        [DllImport("kernel32.dll")]
        public static extern int GetModuleHandleA(string name);

        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hwnd, int attrib, int size, int address, int par, int flags, int threadid);
        [DllImport("KERNEL32.DLL ")]
        public static extern int CloseHandle(IntPtr handle);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        private void button4_Click(object sender, EventArgs e)
        {
            if (wechat_add_input.Text == "" || wechat_port_input.Text == "")
            {
                SetLabelText(label_wechat_tip, "请填写 微信目录/监听端口", Color.Red);
                return;
            }
            var config = Configurator.Of<StateConfiguration>();
            config.WeChatFolder = wechat_add_input.Text;
            Process myProcess = new Process();
            ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(wechat_add_input.Text + "\\WeChat.exe");
            myProcess.StartInfo = myProcessStartInfo;
            myProcess.Start();

            InjectDll(myProcess);
        }
        private int InjectDll(Process myProcess)
        {
            //获取当前工作目录下的dll
            string dllfile = System.Windows.Forms.Application.StartupPath + "WeChatHook.dll";
            MessageBox.Show(dllfile);
            if (!File.Exists(dllfile))
            {
                SetLabelText(label_wechat_tip, "错误: DLL文件丢失", Color.Red);
                return 0;
            }
            //获取微信Pid

            //检测dll是否已经注入
            if (!CheckIsInject(myProcess.Id))
            {
                //在微信进程中申请内存
                Int32 AllocBaseAddress = VirtualAllocEx(myProcess.Handle, 0, dllfile.Length + 1, 4096, 4);
                if (AllocBaseAddress == 0)
                {
                    SetLabelText(label_wechat_tip, "错误: 内存分配失败", Color.Red);
                    return 0;
                }
                //写入dll路径到微信进程
                if (WriteProcessMemory(myProcess.Handle, AllocBaseAddress, dllfile, dllfile.Length + 1, 0) == 0)
                {
                    SetLabelText(label_wechat_tip, "错误: DLL写入失败", Color.Red);
                    return 0;
                }
                Int32 loadaddr = GetProcAddress(GetModuleHandleA("kernel32.dll"), "LoadLibraryA");
                if (loadaddr == 0)
                {
                    SetLabelText(label_wechat_tip, "错误: 取得LoadLibraryA的地址失败", Color.Red);
                    return 0;
                }
                IntPtr ThreadHwnd = CreateRemoteThread(myProcess.Handle, 0, 0, loadaddr, AllocBaseAddress, 0, 0);
                if (ThreadHwnd == IntPtr.Zero)
                {
                    SetLabelText(label_wechat_tip, "错误: 创建远程线程失败", Color.Red);
                    return 0;
                }
                CloseHandle(ThreadHwnd);
            }
            else
            {
                SetLabelText(label_wechat_tip, "提示: 当前微信存在监听控件, 请先重启微信", Color.Red);
                return 0;
            }
            return myProcess.Id;
        }
        private bool CheckIsInject(int wxProcessid)
        {
            Process[] mProcessList = Process.GetProcesses(); //取得所有进程

            foreach (Process mProcess in mProcessList) //遍历进程
            {
                if (mProcess.Id == wxProcessid)
                {

                    ProcessModuleCollection myProcessModuleCollection = mProcess.Modules;
                    ProcessModule myProcessModule;
                    for (int i = 0; i < myProcessModuleCollection.Count; i++)
                    {
                        myProcessModule = myProcessModuleCollection[i];
                        if (myProcessModule.ModuleName == "WeChatHook.dll")
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }


    internal class StateConfiguration : Configuration
    {
        /// <summary>
        /// 获取/设置回调地址。
        /// </summary>
        internal string CallbackHost
        {
            get => GetString();
            set => SetValue(value);
        }
        /// <summary>
        /// 获取/设置通信密钥。
        /// </summary>
        internal string CallbackKey
        {
            get => GetString();
            set => SetValue(value);
        }
        /// <summary>
        /// 获取/设置微信安装目录
        /// </summary>
        internal string WeChatFolder
        {
            get => GetString();
            set => SetValue(value);
        }
        internal int WeChatListenerPort
        {
            get => GetInt32() ?? 40035;
            set => SetValue(Equals(value, 40035) ? null : value);
        }
    }


    

}