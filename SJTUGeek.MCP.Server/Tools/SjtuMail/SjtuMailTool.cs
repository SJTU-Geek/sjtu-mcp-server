using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using SJTUGeek.MCP.Server.Modules;
using SJTUGeek.MCP.Server.Tools.SjtuMail;
using System.ComponentModel;

namespace SJTUGeek.MCP.Server.Tools
{
    [McpServerToolType]
    public class SjtuMailTool
    {
        private readonly ILogger<SjtuMailTool> _logger;
        private readonly SjtuMailService _mail;

        public SjtuMailTool(ILogger<SjtuMailTool> logger, SjtuMailService mail)
        {
            _logger = logger;
            _mail = mail;
        }

        [McpServerTool(Name = "get_personal_mails"), Description("Get the list of emails in the specified inbox in your personal mailbox.")]
        public async Task<object> ToolGetMails(
            [Description("The specified mailbox.")]
            string mailbox = "Inbox",
            [Description("Page index.")]
            int page = 1
        )
        {
            MailBoxTypeEnum _mailbox = Enum.Parse<MailBoxTypeEnum>(mailbox, true);
            await _mail.Login();
            var mails = await _mail.GetMails(_mailbox, page);
            if (mails.M == null || mails.M.Length == 0)
                return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = "找不到邮件！" } } };
            var res = RenderMailList(mails);
            return res;
        }

        [McpServerTool(Name = "get_mail"), Description("Get details of a single email.")]
        public async Task<object> ToolGetSingleMail(
            [Description("The id of the email, as returned by the mailing list result.")]
            int mailId
        )
        {
            await _mail.Login();
            var mail = await _mail.GetSingleMail(mailId, false);
            if (mail.M == null)
                return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = "找不到邮件！" } } };
            var res = RenderSingleMail(mail.M.First());
            return res;
        }

        [McpServerTool(Name = "mark_mail"), Description("Mark a single email as read.")]
        public async Task<object> ToolMarkSingleMail(
            [Description("The id of the email, as returned by the mailing list result.")]
            int mailId
        )
        {
            await _mail.Login();
            var mail = await _mail.GetSingleMail(mailId, true);
            if (mail.M == null)
                return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = "找不到邮件！" } } };
            return "标记成功！";
        }

        [McpServerTool(Name = "send_mail"), Description("Send an email to a specified user.")]
        public async Task<object> ToolSendMail(
            [Description("The recipient's email address.")]
            string receiver,
            [Description("The title of the email.")]
            string title,
            [Description("The content of the email, in plain text.")]
            string content,
            [Description("The email address of the CC recipient. Can be empty.")]
            string? cc = null
        )
        {
            await _mail.Login();
            var systemInfo = await _mail.GetSystemInfo();
            var selfInfo = systemInfo.Identities.Identity.First();
            List<ZimbraMailParticipant> participants = new List<ZimbraMailParticipant>();
            participants.Add(new ZimbraMailParticipant() { A = selfInfo.Attrs["zimbraPrefFromAddress"], P = selfInfo.Attrs["zimbraPrefFromDisplay"], T = "f" });
            participants.Add(new ZimbraMailParticipant() { A = receiver, P = receiver.Split('@').First(), T = "t" });
            if (cc != null)
            {
                participants.Add(new ZimbraMailParticipant() { A = cc, P = cc.Split('@').First(), T = "c" });
            }
            var mail = await _mail.SendMail(participants, title, content);
            return "发送成功！";
        }

        
        public string RenderSingleMail(ZimbraMailInfo m)
        {
            var sender = m.E.FirstOrDefault(x => x.T == "f");
            var res =
            $"- 来自 \"{sender?.P ?? "未知发件人"}\" <{sender?.A ?? "未知发件地址"}> 的邮件：" + "\n" +
            (m.E.Length > 1 ? SjtuMailHelper.ConvertOtherParticipants(m.E.Except([sender])) + "\n" : "") +
            $"  标题：{m.Su}" + "\n" +
            $"  id：{m.Id}" + "\n" +
            $"  时间：{DateTimeOffset.FromUnixTimeMilliseconds(m.D).DateTime.ToString("G")}" + "\n" +
            (m.F != null ? $"  属性：{SjtuMailHelper.ConvertMailFlags(m.F)}" + "\n" : "") +
            (m.Mp != null ? $"  内容：{m.Mp.First().Content}" : $"  摘要：{m.Fr}")
            ;
            return res;
        }

        public string RenderMailList(ZimbraSearchResponse list)
        {
            return string.Join('\n', list.M.Select(x => RenderSingleMail(x)));
        }
    }
}
