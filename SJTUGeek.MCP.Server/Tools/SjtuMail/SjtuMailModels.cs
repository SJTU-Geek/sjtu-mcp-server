using System.Text.Json.Serialization;

namespace SJTUGeek.MCP.Server.Tools.SjtuMail
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MailBoxTypeEnum
    {
        Inbox, Sent, Drafts, Junk, Trash
    }

    public partial class ZimbraSingleResponseWrapper<T>
    {
        [JsonPropertyName("Body")]
        public Dictionary<string, T> Body { get; set; }

        [JsonPropertyName("Header")]
        public Dictionary<string, object> Header { get; set; }
    }

    public partial class ZimbraSearchResponse
    {
        [JsonPropertyName("m")]
        public ZimbraMailInfo[] M { get; set; }

        [JsonPropertyName("more")]
        public bool More { get; set; }

        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [JsonPropertyName("sortBy")]
        public string SortBy { get; set; }
    }

    public partial class ZimbraGetMsgResponse
    {
        [JsonPropertyName("m")]
        public ZimbraMailInfo[] M { get; set; }
    }

    public partial class ZimbraSendMsgResponse
    {
        [JsonPropertyName("m")]
        public ZimbraMailInfo[] M { get; set; }
    }

    public partial class ZimbraGetInfoResponse
    {
        [JsonPropertyName("accessed")]
        public long Accessed { get; set; }

        [JsonPropertyName("attSizeLimit")]
        public long AttSizeLimit { get; set; }

        [JsonPropertyName("crumb")]
        public string Crumb { get; set; }

        [JsonPropertyName("docSizeLimit")]
        public long DocSizeLimit { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("identities")]
        public ZimbraIdentities Identities { get; set; }

        [JsonPropertyName("isSpellCheckAvailable")]
        public bool IsSpellCheckAvailable { get; set; }

        [JsonPropertyName("isTrackingIMAP")]
        public bool IsTrackingImap { get; set; }

        [JsonPropertyName("lifetime")]
        public long Lifetime { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("pasteitcleanedEnabled")]
        public bool PasteitcleanedEnabled { get; set; }

        [JsonPropertyName("prevSession")]
        public long PrevSession { get; set; }

        [JsonPropertyName("publicURL")]
        public string PublicUrl { get; set; }

        [JsonPropertyName("recent")]
        public long Recent { get; set; }

        [JsonPropertyName("rest")]
        public string Rest { get; set; }

        [JsonPropertyName("soapURL")]
        public string SoapUrl { get; set; }

        [JsonPropertyName("used")]
        public long Used { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public partial class ZimbraIdentities
    {
        [JsonPropertyName("identity")]
        public ZimbraIdentity[] Identity { get; set; }
    }

    public partial class ZimbraIdentity
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("_attrs")]
        public Dictionary<string, string> Attrs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public partial class ZimbraMailInfo
    {
        [JsonPropertyName("cid")]
        public string Cid { get; set; }

        [JsonPropertyName("cm")]
        public bool Cm { get; set; }

        [JsonPropertyName("d")]
        public long D { get; set; }

        [JsonPropertyName("e")]
        public ZimbraMailParticipant[] E { get; set; }

        [JsonPropertyName("f")]
        public string? F { get; set; }

        [JsonPropertyName("fr")]
        public string Fr { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("l")]
        public string L { get; set; }

        [JsonPropertyName("rev")]
        public long Rev { get; set; }

        [JsonPropertyName("s")]
        public long S { get; set; }

        [JsonPropertyName("sf")]
        public string Sf { get; set; }

        [JsonPropertyName("su")]
        public string Su { get; set; }

        [JsonPropertyName("mp")]
        public ZimbraMailContent[] Mp { get; set; }
    }

    public partial class ZimbraMailContent
    {
        [JsonPropertyName("body")]
        public bool Body { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("ct")]
        public string Ct { get; set; }

        [JsonPropertyName("part")]
        public string Part { get; set; }

        [JsonPropertyName("s")]
        public int S { get; set; }
    }

    public partial class ZimbraMailParticipant
    {
        [JsonPropertyName("a")]
        public string A { get; set; }

        [JsonPropertyName("d")]
        public string D { get; set; }

        [JsonPropertyName("p")]
        public string P { get; set; }

        [JsonPropertyName("t")]
        public string T { get; set; }
    }
}
