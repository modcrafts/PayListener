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
using System.Xml;
using HttpListener = System.Net.HttpListener;
using HttpListenerContext = System.Net.HttpListenerContext;

namespace PayListener
{
    internal class WeChatService
    {

        public class List
        {
            public string from { get; set; }
            public string content { get; set; }
        }

        public static void WechatCallback(string msg)
        {
            Shell.WriteLine("{0}|微信：{1}", "信息", "获取到微信消息\n"+msg);
            if (true)//(MessageInfoList.from.IndexOf("gh_") == 0 || MessageInfoList.from == "notifymessage")
            {
                try
                {
                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(msg);
                    var root = xml.DocumentElement;

                    var pub_time = root?.SelectSingleNode("/msg/appmsg/mmreader/template_header/pub_time");
                    string? time = pub_time?.InnerText;

                    var topline = root?.SelectSingleNode("/msg/appmsg/mmreader/template_detail/line_content/topline");
                    string? amount = topline?["key"]?["word"]?.InnerText == "收款金额" ? topline?["value"]?["word"]?.InnerText : null;

                    var lines = root?.SelectNodes("/msg/appmsg/mmreader/template_detail/line_content/lines/line");
                    if (time == null || amount == null || lines == null) return;
                    string note = "";
                    foreach (XmlNode node in lines)
                    {
                        if (node?["key"]?["word"]?.InnerText == "付款方备注")
                        {
                            note = node?["value"]?["word"]?.InnerText ?? "";
                            break;
                        }
                    }
                    string state = RemoteService.AppPush(time, "1", amount.Replace("￥", ""));
                    if (state != "上报成功") state = "失败: " + state;
                    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(int.Parse(time));
                    DateTimeOffset.FromUnixTimeSeconds(long.Parse(time));
                    DataRow dr = Program.wechat_DataTable.NewRow();
                    dr[0] = dateTimeOffset.LocalDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    dr[1] = amount;
                    dr[2] = note;
                    dr[3] = state;
                    Form1.form1.Invoke(new Action(delegate
                    {
                        Program.wechat_DataTable.Rows.Add(dr);
                    }));
                    Shell.WriteLine("{0}|微信：{1}", "信息", "信息有效, 已处理");
                }  catch { }
                /*
                Regex regex = new Regex(@"<!\[CDATA\[微信支付收款(?<amount>[\s\S]*?)元\(朋友到店\)]]>[\s\S]*?付款方备注(?<note>[\s\S]*?)汇总今日第[\s\S]*?<pub_time>(?<time>[\s\S]*?)<\/pub_time>", RegexOptions.IgnoreCase);
                Match match = regex.Match(msg);
                if (match.Success)
                {
                    string amount = match.Groups["amount"].Value;
                    string time = match.Groups["time"].Value;
                    string note = match.Groups["note"].Value;

                    string state = RemoteService.AppPush(time, "1", amount);
                    if (state != "上报成功") state = "失败: " + state;
                    System.DateTime startTime = TimeZoneInfo.ConvertTimeFromUtc(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local);
                    var timedate = startTime.AddSeconds(int.Parse(time));
                    DataRow dr = Program.dataTable.NewRow();
                    dr[0] = timedate.ToString("yyyy-MM-dd hh:mm:ss");
                    dr[1] = amount;
                    dr[2] = note;
                    dr[3] = state;
                    Program.dataTable.Rows.Add(dr);
                }
                */
            }
        }
    }
}
