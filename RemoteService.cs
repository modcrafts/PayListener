using dotnetCampus.Configurations;
using dotnetCampus.Configurations.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace PayListener
{
    internal class RemoteService
    {

        /// <summary>
        /// 启动心跳上报
        ///     true: 启动 
        ///     false: 停止
        /// </summary>
        public static async Task<bool> HeartBeatManagerAsync(bool type)
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

        public class List
        {
            public int code { get; set; }
            public string msg { get; set; }
        }
        public static string AppPush(string t, string type, string price)
        {
            var config = Program.config;
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            string sign = MD5Encrypt32( type + price + t + config.CallbackKey);
            var data = new JsonObject();
            data.Add("t", t);
            data.Add("type", type);
            data.Add("price", price);
            data.Add("sign", sign.ToLower());
            try
            {
                string res = Post("http://" + config.CallbackHost + "/appPush", data);
                JObject? PostInfoList = (JObject?)JsonConvert.DeserializeObject(res);
                if (PostInfoList?["code"]?.ToString() == "1")
                {
                    return "上报成功";
                }
                else
                {
                    return PostInfoList?["msg"]?.ToString() ?? "无法获取到错误信息";
                }

            }
            catch (Exception e)
            {
                return e.Message;
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

        public static string Post(string url, JsonObject data)
        {
            HttpClient httpClient = new HttpClient();
            var httpContent = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(url, httpContent).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            httpContent.Dispose();
            return result;
        }
    }

    public class HeartBeatJob : IJob
    {
        public static string LastStatus = "暂未上报";
        Task IJob.Execute(IJobExecutionContext context)
        {
            var config = Program.config;
            string timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString();
            string sign = MD5Encrypt32(timestamp + config.CallbackKey);
            var data = new JsonObject();
            data.Add("t", timestamp);
            data.Add("sign", sign.ToLower());
            try
            {
                string res = Post("http://" + config.CallbackHost + "/appHeart", data).Result;
                //MessageBox.Show("上报成功:\n" + res);
                JObject? resInfo = (JObject?)JsonConvert.DeserializeObject(res);
                var rowdata = new object[] { new object[] { $"{DateTime.Now.ToLocalTime():yyyy-MM-dd HH:mm:ss}", resInfo?["code"]?.ToString() == "1" ? "成功" : "失败", res } };
                Form1.form1.listView1.Invoke(Form1.updateHbList, rowdata);
                //Form1.form1.Invoke(Form1.updateHbList, rowdata);
                LastStatus = res;

            }
            catch (Exception a)
            {
                MessageBox.Show("发生未预料的错误:\n" + a.Message);
                throw;
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
