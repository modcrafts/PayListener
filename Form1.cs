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
using System.ComponentModel;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace PayListener
{
    public partial class Form1 : Form
    {
        public delegate void updateHbListDelegate(Object[] data); // delegate type 
        public static updateHbListDelegate updateHbList; // delegate object
        public static Form1 form1;
        private delegate void InvokeHandler();

        public Form1()
        {
            InitializeComponent();
            //this.BindWaterMark();
            form1 = this;
            updateHbList = new updateHbListDelegate(updateHbtListMethod);

            Form_Resize(null,null);
            this.Resize += new EventHandler(Form_Resize);

            var config = Program.config;
            remoteHostInput.Text = config.CallbackHost;
            remoteKeyInput.Text = config.CallbackKey;
            wechat_add_input.Text = config.WeChatFolder;
            alipayIntervaltext.Text = config.AlipayInterval.ToString();
            checkBox1.Checked = config.Callbackssl;
            check_console.Checked = config.DebugMode;
            //CheckForIllegalCrossThreadCalls = false; 允许跨线程
            LoadConsole();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

            this.Invoke(new Action(delegate {

                DataColumn c1 = new DataColumn("时间", typeof(string));
                DataColumn c2 = new DataColumn("金额", typeof(string));
                DataColumn c3 = new DataColumn("备注", typeof(string));
                DataColumn c4 = new DataColumn("上报", typeof(string));
                Program.wechat_DataTable.Columns.Add(c1);
                Program.wechat_DataTable.Columns.Add(c2);
                Program.wechat_DataTable.Columns.Add(c3);
                Program.wechat_DataTable.Columns.Add(c4);
                data_wechat_View.DataSource = Program.wechat_DataTable.DefaultView;  //DataGridView绑定数据源
                data_wechat_View.AllowUserToAddRows = false;        //删除空行
                data_wechat_View.Columns[0].FillWeight = 35;
                data_wechat_View.Columns[1].FillWeight = 15;
                data_wechat_View.Columns[2].FillWeight = 20;
                data_wechat_View.Columns[3].FillWeight = 30;

                DataColumn d1 = new DataColumn("时间", typeof(string));
                DataColumn d2 = new DataColumn("金额", typeof(string));
                DataColumn d3 = new DataColumn("备注", typeof(string));
                DataColumn d4 = new DataColumn("上报", typeof(string));
                DataColumn d5 = new DataColumn("交易号", typeof(string));
                Program.alipay_DataTable.Columns.Add(d1);
                Program.alipay_DataTable.Columns.Add(d2);
                Program.alipay_DataTable.Columns.Add(d3);
                Program.alipay_DataTable.Columns.Add(d4);
                Program.alipay_DataTable.Columns.Add(d5);
                data_alipay_View.DataSource = Program.alipay_DataTable.DefaultView;  //DataGridView绑定数据源
                data_alipay_View.AllowUserToAddRows = false;        //删除空行
                data_alipay_View.Columns[0].FillWeight = 35;
                data_alipay_View.Columns[1].FillWeight = 25;
                data_alipay_View.Columns[2].FillWeight = 20;
                data_alipay_View.Columns[3].FillWeight = 30;
                data_alipay_View.Columns[4].FillWeight = 40;

            }));
            
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern void FreeConsole();
        private void LoadConsole()
        {
            if (Program.config.DebugMode)
            {
                AllocConsole();
            }
            else
            {
                FreeConsole();
            }

        }

        public void updateHbtListMethod(Object[] data)
        {
            //Console.WriteLine(data[0].ToString());
            //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
            listView1.BeginUpdate();
            ListViewItem listViewItem = new ListViewItem();
            listViewItem.SubItems[0].Text = data[0].ToString();
            for (int i = 1; i < data.Length; i++)
            {
                listViewItem.SubItems.Add(data[i].ToString());
            }
            listView1.Items.Add(listViewItem);
            //结束数据处理，UI界面一次性绘制。
            listView1.EndUpdate();
        }

        private async void Form_Resize(object? sender, System.EventArgs? e)
        {
            float totalColumnWidth = 10.0F;  //1.0+2.0+1.0 = 4.0

            float colPercentage0 = 3 / totalColumnWidth;
            listView1.Columns[0].Width = (int)(colPercentage0 * listView1.ClientRectangle.Width);

            float colPercentage1 = 1 / totalColumnWidth;
            listView1.Columns[1].Width = (int)(colPercentage1 * listView1.ClientRectangle.Width);

            float colPercentage2 = 6 / totalColumnWidth;
            listView1.Columns[2].Width = (int)(colPercentage2 * listView1.ClientRectangle.Width);
        }

        private async void SetLabelText(Label txt, string value, Color color)
        {
            txt.ForeColor = color;
            txt.Text = value;
            new Task(async () =>
            {
                Task.Delay(3000).Wait();
                if (txt.Text == value)
                {
                    Action set = () => { txt.Text = ""; txt.ForeColor = Color.Black; };
                    txt.Invoke(set);
                }
            }).Start();
        }

        const int WM_COPYDATA = 0x004A;//WM_COPYDATA消息的主要目的是允许在进程间传递只读数据。
        //Windows在通过WM_COPYDATA消息传递期间，不提供继承同步方式。
        //其中,WM_COPYDATA对应的十六进制数为0x004A
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:
                    COPYDATASTRUCT cdata = new COPYDATASTRUCT();
                    Type mytype = cdata.GetType();
                    cdata = (COPYDATASTRUCT)m.GetLParam(mytype);
                    string Data = cdata.lpData;
                    WeChatService.WechatCallback(Data);
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
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
                    string res = await Post((Program.config.Callbackssl ? "https://" : "http://") + remoteHostInput.Text + "/appHeart", data);
                    if (res == "" || res == null)
                    {
                        remoteTipLabel.Invoke(new Action<Label, string, Color>(SetLabelText),
                                                 remoteTipLabel, "网址无效或无法访问", Color.Red);
                        return;
                    }
                    
                    var json = JsonNode.Parse(res);
                    if (json?["code"]?.ToString() == "1")
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


        private void button2_Click(object sender, EventArgs e)
        {
            var config = Program.config;
            config.CallbackHost = remoteHostInput.Text;
            config.CallbackKey = remoteKeyInput.Text;
            SetLabelText(remoteTipLabel, "配置已保存", Color.Green);
        }

        private async void button_startbeat_Click(object sender, EventArgs e)
        {
            var config = Program.config;
            if (config.CallbackHost == "" || config.CallbackKey == "")
            {
                SetLabelText(remoteTipLabel, "请先设置远端地址/通信密钥", Color.Red);
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
                if (checkWeChatVersion(fbd.SelectedPath))
                {
                    wechat_add_input.Text = fbd.SelectedPath;
                }
                else
                {
                    MessageBox.Show("微信版本必须为 3.4.5.27 或微信路径无效");
                }
            }
        }

        private bool checkWeChatVersion(string path)
        {
            try
            {
                FileVersionInfo info = FileVersionInfo.GetVersionInfo(path + "\\WeChatWin.dll");
                return info.FileVersion == "3.4.5.27";
            }
            catch (Exception)
            {
                return false;
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
            try
            {
                if (wechat_add_input.Text == "")//|| wechat_port_input.Text == "")
                {
                    SetLabelText(label_wechat_tip, "请填写 微信目录", Color.Red);
                    return;
                }
                var config = Program.config;
                config.WeChatFolder = wechat_add_input.Text;
                Process[] avalible_p = Process.GetProcessesByName("WeChat");
                foreach (Process win_yg in avalible_p)
                {
                    win_yg.Kill();
                }
                Process myProcess = new Process();
                ProcessStartInfo myProcessStartInfo = new ProcessStartInfo(wechat_add_input.Text + "\\WeChat.exe");
                myProcess.StartInfo = myProcessStartInfo;
                myProcess.Start();
                while (FindWindow("WeChatLoginWndForPC", null) == IntPtr.Zero)
                {
                    System.Threading.Thread.Sleep(500);
                }
                if (InjectDll(myProcess) == 0) return;
                if (true)
                {
                    SetLabelText(label_wechat_tip, "启动成功", Color.Green);
                    return;
                }
                else
                {
                    SetLabelText(label_wechat_tip, "启动失败", Color.Red);
                    return;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            
        }
        private int InjectDll(Process myProcess)
        {
            //获取当前工作目录下的dll
            string dllfile = System.Windows.Forms.Application.StartupPath + "WeChatHook.dll";
            //MessageBox.Show(dllfile);
            if (!File.Exists(dllfile))
            {
                SetLabelText(label_wechat_tip, "错误: DLL文件丢失", Color.Red);
                return 0;
            }

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
                    SetLabelText(label_wechat_tip, "错误: 取得 LoadLibraryA 的地址失败", Color.Red);
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if(!int.TryParse(alipayIntervaltext.Text, out var time)) { MessageBox.Show("间隔时间无效"); return; }

                var config = Program.config;
                config.AlipayInterval = time;

                if (button_startAlipay.Text == "停止")
                {
                    AliPayService.AliPayManagerAsync(false);
                    button_startAlipay.Text = "启动";
                    button_alipayFreshCookie.Enabled = false;
                    return;
                }
                var cookie = AliPayService.GetCookie();
                if (cookie == "")
                {
                    MessageBox.Show("登录失败");
                    return;
                }
                //Console.WriteLine(cookie);
                AliPayService.Init(cookie);
                AliPayService.UserInit();
                AliPayJob.LastUpdateTime = DateTime.Now;
                if (AliPayService.AliPayManagerAsync(true).Result)
                {
                    button_startAlipay.Text = "停止";
                    button_alipayFreshCookie.Enabled = true;
                    //MessageBox.Show("启动成功");
                }
                else
                {
                    MessageBox.Show("启动失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void button_alipayFreshCookie_Click(object sender, EventArgs e)
        {
            AliPayService.UserInit();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.Callbackssl = checkBox1.Checked;
        }

        private void check_console_CheckedChanged(object sender, EventArgs e)
        {
            Program.config.DebugMode = check_console.Checked;
        }
    }

    public static class ControlExtension
    {
        const float cos30 = 0.866f;
        const float sin30 = 0.5f;
        public static void BindWaterMark(this Control ctrl)
        {
            if (ctrl == null || ctrl.IsDisposed)
                return;
            // 绘制水印
            if (ctrl.HaveEventHandler("Paint", "BindWaterMark"))
                return;
            ctrl.Paint += (sender, e) =>
            {
                System.Windows.Forms.Control paintCtrl = sender as System.Windows.Forms.Control;
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                // 计算控件位置
                int offsetX = 0;
                int offsetY = 0;
                while (paintCtrl.Parent != null)
                {
                    offsetX += paintCtrl.Location.X;
                    offsetY += paintCtrl.Location.Y;
                    paintCtrl = paintCtrl.Parent;
                }

                // 平移画布到窗体左上角
                g.TranslateTransform(0 - offsetX, 0 - offsetY + 32);

                //逆时针旋转30度
                g.RotateTransform(-30);

                for (int x = 0; x < e.ClipRectangle.Right + 64 + offsetX; x += 128)
                {
                    for (int y = 0; y < e.ClipRectangle.Bottom + 64 + offsetY; y += 128)
                    {
                        // 计算文字起点位置
                        float x1 = cos30 * x - sin30 * y;
                        float y1 = sin30 * x + cos30 * y;

                        //画上文字
                        g.DrawString("测试版本 0.1.0.5", new Font("微软雅黑", 16, FontStyle.Regular),
                            new SolidBrush(Color.FromArgb(50, 100, 100, 100)), x1, y1);
                        g.DrawString("QQ群: 312558935", new Font("微软雅黑", 10, FontStyle.Regular),
                            new SolidBrush(Color.FromArgb(50, 100, 100, 100)), x1, y1-10);
                    }
                }
            };
            // 子控件绑定绘制事件
            foreach (System.Windows.Forms.Control child in ctrl.Controls)
                BindWaterMark(child);
        }

        public static bool HaveEventHandler(this Control control, string eventName, string methodName)
        {
            //获取Control类定义的所有事件的信息
            PropertyInfo pi = (control.GetType()).GetProperty("Events", BindingFlags.Instance | BindingFlags.NonPublic);
            //获取Control对象control的事件处理程序列表
            EventHandlerList ehl = (EventHandlerList)pi.GetValue(control, null);

            //获取Control类 eventName 事件的字段信息
            FieldInfo fieldInfo = (typeof(Control)).GetField(string.Format("Event{0}", eventName), BindingFlags.Static | BindingFlags.NonPublic);
            //用获取的 eventName 事件的字段信息，去匹配 control 对象的事件处理程序列表，获取control对象 eventName 事件的委托对象
            //事件使用委托定义的，C#中的委托时多播委托，可以绑定多个事件处理程序，当事件发生时，这些事件处理程序被依次执行
            //因此Delegate对象，有一个GetInvocationList方法，用来获取这个委托已经绑定的所有事件处理程序
            Delegate d = ehl?[fieldInfo?.GetValue(null)];

            if (d == null)
                return false;

            foreach (Delegate del in d.GetInvocationList())
            {
                string anonymous = string.Format("<{0}>", methodName);
                //判断一下某个事件处理程序是否已经被绑定到 eventName 事件上
                if (del.Method.Name == methodName || del.Method.Name.StartsWith(anonymous))
                {
                    return true;
                }
            }

            return false;
        }
    }
}