namespace SJTUGeek.MCP.Server.Tools.SjtuMail
{
    public static class SjtuMailHelper
    {
        public static string ConvertOtherParticipants(IEnumerable<ZimbraMailParticipant?> participants)
        {
            IEnumerable<string> InternalConvert(IEnumerable<ZimbraMailParticipant?> participants)
            {
                foreach (var p in participants)
                {
                    //(f)rom, (t)o, (c)c, (b)cc, (r)eply-to, (s)ender, read-receipt (n)otification, (rf) resent-from
                    var type = p.T switch
                    {
                        "f" => "发件人",
                        "t" => "收件人",
                        "c" => "抄送",
                        "b" => "密送",
                        "r" => "回复",
                        "s" => "实际发送人",
                        "rf" => "重定向自",
                        _ => "未知类型参与人"
                    };
                    yield return $"  {type}：\"{p.P}\" <{p.A}>";
                }
            }
            return string.Join("\n", InternalConvert(participants));
        }

        public static string ConvertMailFlags(string f)
        {
            //(u)nread, (f)lagged, has (a)ttachment, (r)eplied, (s)ent by me, for(w)arded, calendar in(v)ite, (d)raft, IMAP-\Deleted (x), (n)otification sent, urgent (!), low-priority (?), priority (+)
            string InternalStateConvert(char c)
            {
                return c switch
                {
                    'u' => "未读",
                    'f' => "标记",
                    'a' => "有附件",
                    'r' => "已回复",
                    's' => "我发送的邮件",
                    'w' => "已转发",
                    'v' => "日程邀请",
                    'd' => "草稿",
                    'x' => "已删除",
                    'n' => "通知已发送",
                    '!' => "紧急",
                    '?' => "低重要性",
                    '+' => "重要",
                    _ => "未知"
                };
            }
            return string.Join('，', f.Select(x => InternalStateConvert(x)));
        }
    }
}
