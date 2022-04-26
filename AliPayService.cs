using dotnetCampus.Configurations;
using dotnetCampus.Configurations.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PayListener
{
    internal class AliPayService
    {
        private static HttpClient HttpClient;
        private static readonly CookieContainer CookieContainer = new();
        public static string? userid;
        public static string? ctoken;
        public static bool randomPage;

        public static async Task<bool> AliPayManagerAsync(bool type)
        {

            var config = Program.config;
            try
            {
                StdSchedulerFactory factory = new StdSchedulerFactory();
                IScheduler scheduler = await factory.GetScheduler();
                var gropName = "Tasks";
                var jobName = "AliPay_job";
                var tiggerName = "AliPay_tigger";
                if (type)
                {
                    IJobDetail job_heartbeat = JobBuilder.Create<AliPayJob>()
                                        .WithIdentity(jobName, gropName)
                                        //.UsingJobData("key", "value")//为任务的具体任务传递参数，键值对（非必须）
                                        .Build();//创建
                    ITrigger trigger_heartbeat = TriggerBuilder.Create()
                                           .WithIdentity(tiggerName, gropName) //为触发器的tiggerName和gropName赋值，相当与给予一个身份
                                           .StartNow()                        //现在开始
                                           .WithSimpleSchedule(x => x
                                               .WithIntervalInSeconds(config.AlipayInterval)     //触发时间
                                               .RepeatForever())              //不间断重复执行
                                           .Build();                          //最终创建

                    await scheduler.ScheduleJob(job_heartbeat, trigger_heartbeat);      //把任务，触发器加入调度器。

                    await scheduler.Start();

                    randomPage = true;
                    new Task(async () =>
                    {
                        try
                        {
                            System.IO.StreamReader file = System.IO.File.OpenText(@"\AliPayPages.txt");
                            file.ReadToEnd().Split();
                            string[] pages = file.ReadToEnd().Split('\n');
                            Random rd = new Random();
                            var count = 0;
                            while (randomPage)
                            {
                                count = rd.Next(0, pages.Length - 1);
                                if (!(pages?[count]?.Contains("alipay.com") ?? false)) continue;
                                await Get(pages[count]);
                                Task.Delay(rd.Next(5000, 60000)).Wait();
                            }
                            
                        }
                        catch (Exception e)
                        {

                        }
                    }).Start();

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

                    randomPage = false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static void Init(string cookie)
        {
            var handler = new HttpClientHandler();
            handler.CookieContainer = CookieContainer;
            HttpClient = new HttpClient(handler);

            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 Gecko/20100101 Firefox/99.0");
            //HttpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla / 5.0(Windows NT 10.0; Win64; x64; rv: 99.0) Gecko / 20100101 Firefox / 99.0");
            HttpClient.DefaultRequestHeaders.Add("referer", "https://b.alipay.com/");
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpClient.DefaultRequestHeaders.Add("cookie", cookie.Replace("\r\n", ""));
        }
        public static void UpdateCookie(string cookie)
        {
            HttpClient?.DefaultRequestHeaders?.Remove("cookie");
            HttpClient?.DefaultRequestHeaders?.Add("cookie", cookie.Replace("\r\n", ""));
        }
        public static async Task<string> Post(string url, FormUrlEncodedContent data)
        {
            //var httpContent = new StringContent(data.ToString().Replace("\r\n",""), Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = HttpClient.PostAsync(url, data).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            //httpContent.Dispose();

            return result;
        }

        public static async Task<string> Get(string url)
        {
            var response = HttpClient.GetAsync(url).Result;
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }

        public static void UserInit(int times = 1)
        {
            JObject? userInfo = (JObject?)JsonConvert.DeserializeObject(Get("https://mrchportalweb.alipay.com/interface/login/index/queryuser?_input_charset=gbk").Result);
            if(userInfo != null && (bool)(userInfo?["success"]??false))
            {
                userid = userInfo?["data"]?["userId"]?.ToString();
                Regex regex = new Regex(@"ctoken=(?<ctoken>[\s\S]*?);", RegexOptions.IgnoreCase);
                Match match = regex.Match(HttpClient.DefaultRequestHeaders.GetValues("cookie").First());
                if (match.Success) ctoken = match.Groups["ctoken"].Value;
            }
            else if (times < 10)
            {
                if (GetCookie() == "")
                {
                    UserInit(10);
                    return;
                }
                UserInit(times+1);
            }
            else
            {
                throw new Exception("用户数据获取失败");
            }
        }
        public static string GetCookie()
        {


            [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
            static extern bool StrongNameSignatureVerificationEx(string wszFilePath, bool fForceVerification, ref bool pfWasVerified);
            bool b = true;
            if (!StrongNameSignatureVerificationEx(@".\AliLogin.exe", true, ref b))
            {
                throw new Exception("登录程序签名校验失败!\n请确认程序来源\n使用未知来源的程序可能会造成你的隐私(cookie)泄露");
            }
            string output;
            Process p = new Process();
            p.StartInfo.FileName = @"AliLogin.exe";//可执行程序路径
            p.StartInfo.Arguments = "";//参数以空格分隔，如果某个参数为空，可以传入""
            p.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
            p.Start();
            p.WaitForExit();
            if (p.ExitCode != 0)
            {
                output = p.StandardError.ReadToEnd();
                output = output.ToString().Replace(System.Environment.NewLine, string.Empty);
                output = output.ToString().Replace("\n", string.Empty);
                throw new Exception(output.ToString());
            }
            else
            {
                output = p.StandardOutput.ReadToEnd();
            }
            if (!output.Contains("ctoken"))
            {
                return "";
            }
            UpdateCookie(output.Replace("\r\n", ""));
            return output.Replace("\r\n", "");
        }
    }

    [DisallowConcurrentExecution]
    public class AliPayJob : IJob
    {
        public static bool expried = false;
        public static DateTimeOffset LastUpdateTime;
        Task IJob.Execute(IJobExecutionContext context)
        {
            if (expried) return Task.CompletedTask;
            try
            {
                var config = Program.config;
                var bills = GetBills();
                if (bills == "failed")
                {
                    return Task.CompletedTask;
                }
                JObject? billInfo = (JObject?)JsonConvert.DeserializeObject(bills);
                if (billInfo?["status"]?.ToString() != "succeed" || billInfo?["result"]?["detail"] == null) return Task.CompletedTask;
                foreach (JToken item in billInfo["result"]["detail"])
                {
                    if (item?["tradeTransAmount"]?.ToString() == "0.00") continue;

                    DataRow dr = Program.alipay_DataTable.NewRow();
                    dr[0] = item["gmtCreate"].ToString();
                    dr[1] = item["tradeTransAmount"].ToString();
                    dr[2] = item?["buyerMemo"]?.ToString();
                    dr[4] = item["tradeNo"];

                    Console.WriteLine((DateTime.ParseExact(item["gmtCreate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds.ToString());
                    var state = RemoteService.AppPush((DateTime.ParseExact(item["gmtCreate"].ToString(), "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) - TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1))).TotalSeconds.ToString(), "2", item["tradeTransAmount"].ToString());

                    if (state != "上报成功") state = "失败: " + state;
                    dr[3] = state;

                    Program.alipay_DataTable.Rows.Add(dr);
                }


            }
            catch (Exception e)
            {

                throw;
            }
            return Task.CompletedTask;
        }
        
        private string GetBills(int calltimes = 1)
        {
            var timenow = DateTime.Now;
            var data = new List<KeyValuePair<string, string>>();
            //data.Add(new KeyValuePair<string, string>("ctoken", AliPayService.ctoken));
            Console.WriteLine($"{timenow:yyyy-MM-dd hh:mm:ss}");
            data.Add(new KeyValuePair<string, string>("billUserId", AliPayService.userid));
            data.Add(new KeyValuePair<string, string>("targetTradeOwner", "USERID"));
            data.Add(new KeyValuePair<string, string>("zftSmid", ""));
            data.Add(new KeyValuePair<string, string>("pageNum", "1"));
            data.Add(new KeyValuePair<string, string>("pageSize", "100"));
            data.Add(new KeyValuePair<string, string>("status", "ALL"));
            data.Add(new KeyValuePair<string, string>("sortType", "0"));
            data.Add(new KeyValuePair<string, string>("_input_charset", "gbk"));

            data.Add(new KeyValuePair<string, string>("startTime", $"{LastUpdateTime.ToLocalTime():yyyy-MM-dd HH:mm:ss}"));
            data.Add(new KeyValuePair<string, string>("endTime", $"{timenow.ToLocalTime():yyyy-MM-dd HH:mm:ss}"));

            string res = AliPayService.Post($"https://mbillexprod.alipay.com/enterprise/tradeListQuery.json?ctoken={AliPayService.ctoken}&_output_charset=utf-8", new FormUrlEncodedContent(data)).Result;

            Console.WriteLine(res);
            JObject? resInfo = (JObject?)JsonConvert.DeserializeObject(res);
            if(resInfo?["status"]?.ToString() == "succeed" || res == "")
            {
                expried = false;
                LastUpdateTime = timenow.AddSeconds(1);
                return res;
            }
            else if (resInfo?["status"]?.ToString() == "failed")
            {
                return "failed";
            }
            else if(calltimes < 10)
            {
                expried=true;
                AliPayService.UserInit();
                return GetBills(calltimes+1);
            }
            else
            {
                expried=true;
                throw new Exception("多次尝试后仍然无法获取支付宝账单");
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

    }
    public class tradeListQueryData
    {
        public string ctoken { get; set; }
        public string billUserId { get; set; }
        public string entityFilterType { get; set; }
        public string tradeFrom { get; set; }
        public string targetTradeOwner { get; set; }
        public string pageNum { get; set; }
        public string pageSize { get; set; }
        public string status { get; set; }
        public string sortType { get; set; }
        public string _input_charset { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
