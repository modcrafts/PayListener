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
            //CheckForIllegalCrossThreadCalls = false; 允许跨线程


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
        /// <summary>
        /// 启动心跳上报
        ///     true: 启动 
        ///     false: 停止
        /// </summary>
        public async Task<bool> HeartBeatManagerAsync(bool type)
        {
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                var gropName = "Tasks";
                var jobName = "HeartBeat_job";
                var tiggerName = "HeartBeat_tigger";
                if (type)
                {
                    IJobDetail job_heartbeat = JobBuilder.Create<HeartBeatJob>()
                                        .WithIdentity(jobName, gropName)
                                        //.UsingJobData("key", "value")//为任务的具体任务传递参数，键值对（非必须）
                                        .Build();//创建
                    ITrigger trigger_heartbeat = TriggerBuilder.Create()
                                           .WithIdentity(tiggerName, gropName) //为触发器的tiggerName和gropName赋值，相当与给予一个身份
                                           .StartNow()                        //现在开始
                                           .WithSimpleSchedule(x => x
                                               .WithIntervalInSeconds(30)     //触发时间，30秒一次。
                                               .RepeatForever())              //不间断重复执行
                                           .Build();                          //最终创建

                    await scheduler.ScheduleJob(job_heartbeat, trigger_heartbeat);      //把任务，触发器加入调度器。

                    await scheduler.Start();
                }
                else
                {
                    TriggerKey triggerKey = new TriggerKey(tiggerName, gropName);
                    JobKey jobKey = new JobKey(jobName, gropName);
                    if (scheduler != null)
                    {
                        await scheduler.PauseTrigger(triggerKey);
                        await scheduler.UnscheduleJob(triggerKey);
                        await scheduler.DeleteJob(jobKey);
                        await scheduler.Shutdown();// 关闭
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        private void button_startbeat_Click(object sender, EventArgs e)
        {
            var config = Configurator.Of<StateConfiguration>();
            if (config.CallbackHost == null || config.CallbackKey == null)
            {
                SetLabelText(remoteTipLabel, "请先设置回调地址/Key", Color.Red);
                return;
            }
            if (button_startbeat.Text == "启动心跳上报")
            {
                HeartBeatManagerAsync(true);
                SetLabelText(remoteTipLabel, "心跳上报启动成功", Color.Green);
                label_heartbeat_1.ForeColor = Color.Green;
                label_heartbeat_1.Text = "心跳上报运行中";
                button_startbeat.Text = "停止心跳上报";
            }
            else
            {
                HeartBeatManagerAsync(false);
                SetLabelText(remoteTipLabel, "心跳上报已停止", Color.Green);
                label_heartbeat_1.ForeColor = Color.CornflowerBlue;
                label_heartbeat_1.Text = "心跳上报未运行";
                button_startbeat.Text = "启动心跳上报";
            }
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
    }


    public class HeartBeatJob : IJob
    {
        public static string LastStatus = "暂未上报"; 
        private readonly static IAppConfigurator Configurator = ConfigurationFactory.FromFile(@".\config.coin").CreateAppConfigurator();
        Task IJob.Execute(IJobExecutionContext context)
        {
            var config = Configurator.Of<StateConfiguration>();
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            string sign = MD5Encrypt32(timestamp + config.CallbackKey);
            var data = new JsonObject();
            data.Add("t", timestamp);
            data.Add("sign", sign.ToLower());
            try
            {
                string res = Post("http://" + config.CallbackHost + "/appHeart", data).Result;
                //MessageBox.Show("上报成功:\n" + res);
                LastStatus = res;
                
            }
            catch (Exception a)
            {
                MessageBox.Show("发生未预料的错误:\n" + a.Message);
                //throw;
            }

            return Task.CompletedTask;
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
    }

}