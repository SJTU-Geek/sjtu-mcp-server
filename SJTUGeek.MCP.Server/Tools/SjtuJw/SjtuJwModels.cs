using System.Text.Json.Serialization;

namespace SJTUGeek.MCP.Server.Tools.SjtuJw
{
    public partial class JwPersonalCourseList
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("djdzList")]
        public string[] DjdzList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jfckbkg")]
        public bool? Jfckbkg { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jxhjkcList")]
        public string[] JxhjkcList { get; set; }

        [JsonPropertyName("kbList")]
        public KbList[] KbList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kblx")]
        public long? Kblx { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("qsxqj")]
        public string Qsxqj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("rqazcList")]
        public string[] RqazcList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sfxsd")]
        public string Sfxsd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sjfwkg")]
        public bool? Sjfwkg { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sjkList")]
        public string[] SjkList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xkkg")]
        public bool? Xkkg { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xnxqsfkz")]
        public string Xnxqsfkz { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xqbzxxszList")]
        public string[] XqbzxxszList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xqjmcMap")]
        public XqjmcMap XqjmcMap { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xsbjList")]
        public XsbjList[] XsbjList { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xskbsfxstkzt")]
        public string Xskbsfxstkzt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xsxx")]
        public Xsxx Xsxx { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zckbsfxssj")]
        public string Zckbsfxssj { get; set; }
    }

    public partial class KbList
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("bklxdjmc")]
        public string Bklxdjmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cd_id")]
        public string CdId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cdbh")]
        public string Cdbh { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cdlbmc")]
        public string Cdlbmc { get; set; }

        [JsonPropertyName("cdmc")]
        public string Cdmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cxbj")]
        public string Cxbj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("cxbjmc")]
        public string Cxbjmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dateDigit")]
        public string DateDigit { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dateDigitSeparator")]
        public string DateDigitSeparator { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("jc")]
        public string Jc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jcor")]
        public string Jcor { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jcs")]
        public string Jcs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jgh_id")]
        public string JghId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jgpxzd")]
        public string Jgpxzd { get; set; }

        [JsonPropertyName("jxb_id")]
        public string JxbId { get; set; }

        [JsonPropertyName("jxbmc")]
        public string Jxbmc { get; set; }

        [JsonPropertyName("jxbzc")]
        public string Jxbzc { get; set; }

        [JsonPropertyName("kcbj")]
        public string Kcbj { get; set; }

        [JsonPropertyName("kch")]
        public string Kch { get; set; }

        [JsonPropertyName("kch_id")]
        public string KchId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kclb")]
        public string Kclb { get; set; }

        [JsonPropertyName("kcmc")]
        public string Kcmc { get; set; }

        [JsonPropertyName("kcxszc")]
        public string Kcxszc { get; set; }

        [JsonPropertyName("kcxz")]
        public string Kcxz { get; set; }

        [JsonPropertyName("kczxs")]
        public string Kczxs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("khfsmc")]
        public string Khfsmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kkzt")]
        public string Kkzt { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("lh")]
        public string Lh { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("listnav")]
        public string Listnav { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("localeKey")]
        public string LocaleKey { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("oldjc")]
        public string Oldjc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("oldzc")]
        public string Oldzc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pageable")]
        public bool? Pageable { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pageTotal")]
        public long? PageTotal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pkbj")]
        public string Pkbj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("px")]
        public string Px { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("qqqh")]
        public string Qqqh { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("queryModel")]
        public QueryModel QueryModel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("rangeable")]
        public bool? Rangeable { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("rk")]
        public string Rk { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("rsdzjs")]
        public long? Rsdzjs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sfjf")]
        public string Sfjf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sfkckkb")]
        public bool? Sfkckkb { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("skfsmc")]
        public string Skfsmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("sxbj")]
        public string Sxbj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("totalResult")]
        public string TotalResult { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("userModel")]
        public UserModel UserModel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xf")]
        public string Xf { get; set; }

        [JsonPropertyName("xkbz")]
        public string Xkbz { get; set; }

        [JsonPropertyName("xm")]
        public string Xm { get; set; }

        [JsonPropertyName("xnm")]
        public string Xnm { get; set; }

        [JsonPropertyName("xqdm")]
        public string Xqdm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xqh1")]
        public string Xqh1 { get; set; }

        [JsonPropertyName("xqh_id")]
        public string XqhId { get; set; }

        [JsonPropertyName("xqj")]
        public string Xqj { get; set; }

        [JsonPropertyName("xqjmc")]
        public string Xqjmc { get; set; }

        [JsonPropertyName("xqm")]
        public string Xqm { get; set; }

        [JsonPropertyName("xqmc")]
        public string Xqmc { get; set; }

        [JsonPropertyName("xsdm")]
        public string Xsdm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xslxbj")]
        public string Xslxbj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("zcd")]
        public string Zcd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zcmc")]
        public string Zcmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zfjmc")]
        public string Zfjmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zhxs")]
        public string Zhxs { get; set; }

        [JsonPropertyName("zxs")]
        public string Zxs { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zxxx")]
        public string Zxxx { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zyfxmc")]
        public string Zyfxmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zyhxkcbj")]
        public string Zyhxkcbj { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zzrl")]
        public string Zzrl { get; set; }
    }

    public partial class QueryModel
    {
        [JsonPropertyName("currentPage")]
        public long CurrentPage { get; set; }

        [JsonPropertyName("currentResult")]
        public long CurrentResult { get; set; }

        [JsonPropertyName("entityOrField")]
        public bool EntityOrField { get; set; }

        [JsonPropertyName("limit")]
        public long Limit { get; set; }

        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [JsonPropertyName("pageNo")]
        public long PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public long PageSize { get; set; }

        [JsonPropertyName("showCount")]
        public long ShowCount { get; set; }

        [JsonPropertyName("sorts")]
        public string[] Sorts { get; set; }

        [JsonPropertyName("totalCount")]
        public long TotalCount { get; set; }

        [JsonPropertyName("totalPage")]
        public long TotalPage { get; set; }

        [JsonPropertyName("totalResult")]
        public long TotalResult { get; set; }
    }

    public partial class UserModel
    {
        [JsonPropertyName("monitor")]
        public bool Monitor { get; set; }

        [JsonPropertyName("roleCount")]
        public long RoleCount { get; set; }

        [JsonPropertyName("roleKeys")]
        public string RoleKeys { get; set; }

        [JsonPropertyName("roleValues")]
        public string RoleValues { get; set; }

        [JsonPropertyName("status")]
        public long Status { get; set; }

        [JsonPropertyName("usable")]
        public bool Usable { get; set; }
    }

    public partial class XqjmcMap
    {
        [JsonPropertyName("1")]
        public string The1 { get; set; }

        [JsonPropertyName("2")]
        public string The2 { get; set; }

        [JsonPropertyName("3")]
        public string The3 { get; set; }

        [JsonPropertyName("4")]
        public string The4 { get; set; }

        [JsonPropertyName("5")]
        public string The5 { get; set; }

        [JsonPropertyName("6")]
        public string The6 { get; set; }

        [JsonPropertyName("7")]
        public string The7 { get; set; }
    }

    public partial class XsbjList
    {
        [JsonPropertyName("xsdm")]
        public string Xsdm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("xslxbj")]
        public string Xslxbj { get; set; }

        [JsonPropertyName("xsmc")]
        public string Xsmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("ywxsmc")]
        public string Ywxsmc { get; set; }
    }

    public partial class Xsxx
    {
        [JsonPropertyName("BJMC")]
        public string Bjmc { get; set; }

        [JsonPropertyName("JFZT")]
        public long Jfzt { get; set; }

        [JsonPropertyName("KCMS")]
        public long Kcms { get; set; }

        [JsonPropertyName("KXKXXQ")]
        public string Kxkxxq { get; set; }

        [JsonPropertyName("NJDM_ID")]
        public string NjdmId { get; set; }

        [JsonPropertyName("XH")]
        public string Xh { get; set; }

        [JsonPropertyName("XH_ID")]
        public string XhId { get; set; }

        [JsonPropertyName("XKKG")]
        public string Xkkg { get; set; }

        [JsonPropertyName("XKKGXQ")]
        public string Xkkgxq { get; set; }

        [JsonPropertyName("XM")]
        public string Xm { get; set; }

        [JsonPropertyName("XNM")]
        public string Xnm { get; set; }

        [JsonPropertyName("XNMC")]
        public string Xnmc { get; set; }

        [JsonPropertyName("XQM")]
        public string Xqm { get; set; }

        [JsonPropertyName("XQMMC")]
        public string Xqmmc { get; set; }

        [JsonPropertyName("YWXM")]
        public string Ywxm { get; set; }

        [JsonPropertyName("ZYH_ID")]
        public string ZyhId { get; set; }

        [JsonPropertyName("ZYMC")]
        public string Zymc { get; set; }
    }

    public partial class JwCourseScoreList
    {
        [JsonPropertyName("currentPage")]
        public long CurrentPage { get; set; }

        [JsonPropertyName("currentResult")]
        public long CurrentResult { get; set; }

        [JsonPropertyName("entityOrField")]
        public bool EntityOrField { get; set; }

        [JsonPropertyName("items")]
        public JwCourseScoreItem[] Items { get; set; }

        [JsonPropertyName("limit")]
        public long Limit { get; set; }

        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [JsonPropertyName("pageNo")]
        public long PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public long PageSize { get; set; }

        [JsonPropertyName("showCount")]
        public long ShowCount { get; set; }

        [JsonPropertyName("sortName")]
        public string SortName { get; set; }

        [JsonPropertyName("sortOrder")]
        public string SortOrder { get; set; }

        [JsonPropertyName("sorts")]
        public string[] Sorts { get; set; }

        [JsonPropertyName("totalCount")]
        public long TotalCount { get; set; }

        [JsonPropertyName("totalPage")]
        public long TotalPage { get; set; }

        [JsonPropertyName("totalResult")]
        public long TotalResult { get; set; }
    }

    public partial class JwCourseScoreItem
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("dateDigit")]
        public string DateDigit { get; set; }

        [JsonPropertyName("dateDigitSeparator")]
        public string DateDigitSeparator { get; set; }

        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("jgpxzd")]
        public string Jgpxzd { get; set; }

        [JsonPropertyName("jxb_id")]
        public string JxbId { get; set; }

        [JsonPropertyName("jxbmc")]
        public string Jxbmc { get; set; }

        [JsonPropertyName("kch")]
        public string Kch { get; set; }

        [JsonPropertyName("kch_id")]
        public string KchId { get; set; }

        [JsonPropertyName("kcmc")]
        public string Kcmc { get; set; }

        [JsonPropertyName("kkbm_id")]
        public string KkbmId { get; set; }

        [JsonPropertyName("kkbmmc")]
        public string Kkbmmc { get; set; }

        [JsonPropertyName("listnav")]
        public string Listnav { get; set; }

        [JsonPropertyName("localeKey")]
        public string LocaleKey { get; set; }

        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("pageable")]
        public bool Pageable { get; set; }

        [JsonPropertyName("pageTotal")]
        public long PageTotal { get; set; }

        [JsonPropertyName("queryModel")]
        public QueryModel QueryModel { get; set; }

        [JsonPropertyName("rangeable")]
        public bool Rangeable { get; set; }

        [JsonPropertyName("row_id")]
        public string RowId { get; set; }

        [JsonPropertyName("totalResult")]
        public string TotalResult { get; set; }

        [JsonPropertyName("userModel")]
        public UserModel UserModel { get; set; }

        [JsonPropertyName("xf")]
        public string Xf { get; set; }

        [JsonPropertyName("xh_id")]
        public string XhId { get; set; }

        [JsonPropertyName("xmblmc")]
        public string Xmblmc { get; set; }

        [JsonPropertyName("xmcj")]
        public string Xmcj { get; set; }

        [JsonPropertyName("xnm")]
        public string Xnm { get; set; }

        [JsonPropertyName("xnmmc")]
        public string Xnmmc { get; set; }

        [JsonPropertyName("xqm")]
        public string Xqm { get; set; }

        [JsonPropertyName("xqmmc")]
        public string Xqmmc { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }
    }

    public partial class JwGpaQueryResult
    {
        [JsonPropertyName("currentPage")]
        public long CurrentPage { get; set; }

        [JsonPropertyName("currentResult")]
        public long CurrentResult { get; set; }

        [JsonPropertyName("entityOrField")]
        public bool EntityOrField { get; set; }

        [JsonPropertyName("items")]
        public JwGpaStatistic[] Items { get; set; }

        [JsonPropertyName("limit")]
        public long Limit { get; set; }

        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [JsonPropertyName("pageNo")]
        public long PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public long PageSize { get; set; }

        [JsonPropertyName("showCount")]
        public long ShowCount { get; set; }

        [JsonPropertyName("sortName")]
        public string SortName { get; set; }

        [JsonPropertyName("sortOrder")]
        public string SortOrder { get; set; }

        [JsonPropertyName("sorts")]
        public string[] Sorts { get; set; }

        [JsonPropertyName("totalCount")]
        public long TotalCount { get; set; }

        [JsonPropertyName("totalPage")]
        public long TotalPage { get; set; }

        [JsonPropertyName("totalResult")]
        public long TotalResult { get; set; }
    }

    public partial class JwGpaStatistic
    {
        [JsonPropertyName("bj")]
        public string Bj { get; set; }

        [JsonPropertyName("bjgmc")]
        public string Bjgmc { get; set; }

        [JsonPropertyName("bjgms")]
        public string Bjgms { get; set; }

        [JsonPropertyName("bjgxf")]
        public string Bjgxf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dateDigit")]
        public string DateDigit { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("dateDigitSeparator")]
        public string DateDigitSeparator { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("day")]
        public string Day { get; set; }

        [JsonPropertyName("gpa")]
        public string Gpa { get; set; }

        [JsonPropertyName("gpapm")]
        public string Gpapm { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("hdxf")]
        public string Hdxf { get; set; }

        [JsonPropertyName("jgmc")]
        public string Jgmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("jgpxzd")]
        public string Jgpxzd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kcfw")]
        public string Kcfw { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("listnav")]
        public string Listnav { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("localeKey")]
        public string LocaleKey { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("month")]
        public string Month { get; set; }

        [JsonPropertyName("ms")]
        public string Ms { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("njmc")]
        public string Njmc { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pageable")]
        public bool? Pageable { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pageTotal")]
        public long? PageTotal { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pm1")]
        public string Pm1 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("pm2")]
        public string Pm2 { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("queryModel")]
        public QueryModel QueryModel { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("rangeable")]
        public bool? Rangeable { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("row_id")]
        public string RowId { get; set; }

        [JsonPropertyName("tgl")]
        public string Tgl { get; set; }

        [JsonPropertyName("tj_id")]
        public string TjId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("totalResult")]
        public string TotalResult { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("userModel")]
        public UserModel UserModel { get; set; }

        [JsonPropertyName("xh")]
        public string Xh { get; set; }

        [JsonPropertyName("xh_id")]
        public string XhId { get; set; }

        [JsonPropertyName("xjf")]
        public string Xjf { get; set; }

        [JsonPropertyName("xjfpm")]
        public string Xjfpm { get; set; }

        [JsonPropertyName("xm")]
        public string Xm { get; set; }

        [JsonPropertyName("year")]
        public string Year { get; set; }

        [JsonPropertyName("zf")]
        public string Zf { get; set; }

        [JsonPropertyName("zxf")]
        public string Zxf { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("zymc")]
        public string Zymc { get; set; }
    }

    public partial class JwExamInfoResult
    {
        [JsonPropertyName("currentPage")]
        public long CurrentPage { get; set; }

        [JsonPropertyName("currentResult")]
        public long CurrentResult { get; set; }

        [JsonPropertyName("entityOrField")]
        public bool EntityOrField { get; set; }

        [JsonPropertyName("items")]
        public JwExamInfoItem[] Items { get; set; }

        [JsonPropertyName("limit")]
        public long Limit { get; set; }

        [JsonPropertyName("offset")]
        public long Offset { get; set; }

        [JsonPropertyName("pageNo")]
        public long PageNo { get; set; }

        [JsonPropertyName("pageSize")]
        public long PageSize { get; set; }

        [JsonPropertyName("showCount")]
        public long ShowCount { get; set; }

        [JsonPropertyName("sortName")]
        public string SortName { get; set; }

        [JsonPropertyName("sortOrder")]
        public string SortOrder { get; set; }

        [JsonPropertyName("totalCount")]
        public long TotalCount { get; set; }

        [JsonPropertyName("totalPage")]
        public long TotalPage { get; set; }

        [JsonPropertyName("totalResult")]
        public long TotalResult { get; set; }
    }

    public partial class JwExamInfoItem
    {
        [JsonPropertyName("bj")]
        public string Bj { get; set; }

        [JsonPropertyName("cdbh")]
        public string Cdbh { get; set; }

        [JsonPropertyName("cdjc")]
        public string Cdjc { get; set; }

        [JsonPropertyName("cdmc")]
        public string Cdmc { get; set; }

        [JsonPropertyName("cdxqmc")]
        public string Cdxqmc { get; set; }

        [JsonPropertyName("cxbj")]
        public string Cxbj { get; set; }

        [JsonPropertyName("jgmc")]
        public string Jgmc { get; set; }

        [JsonPropertyName("jsxx")]
        public string Jsxx { get; set; }

        [JsonPropertyName("jxbmc")]
        public string Jxbmc { get; set; }

        [JsonPropertyName("jxbzc")]
        public string Jxbzc { get; set; }

        [JsonPropertyName("jxdd")]
        public string Jxdd { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("kccc")]
        public string Kccc { get; set; }

        [JsonPropertyName("kch")]
        public string Kch { get; set; }

        [JsonPropertyName("kcmc")]
        public string Kcmc { get; set; }

        [JsonPropertyName("khfs")]
        public string Khfs { get; set; }

        [JsonPropertyName("kkxy")]
        public string Kkxy { get; set; }

        [JsonPropertyName("ksfs")]
        public string Ksfs { get; set; }

        [JsonPropertyName("ksmc")]
        public string Ksmc { get; set; }

        [JsonPropertyName("kssj")]
        public string Kssj { get; set; }

        [JsonPropertyName("njmc")]
        public string Njmc { get; set; }

        [JsonPropertyName("row_id")]
        public long RowId { get; set; }

        [JsonPropertyName("sjbh")]
        public string Sjbh { get; set; }

        [JsonPropertyName("sksj")]
        public string Sksj { get; set; }

        [JsonPropertyName("totalresult")]
        public long Totalresult { get; set; }

        [JsonPropertyName("xb")]
        public string Xb { get; set; }

        [JsonPropertyName("xf")]
        public string Xf { get; set; }

        [JsonPropertyName("xh")]
        public string Xh { get; set; }

        [JsonPropertyName("xh_id")]
        public string XhId { get; set; }

        [JsonPropertyName("xm")]
        public string Xm { get; set; }

        [JsonPropertyName("xnm")]
        public string Xnm { get; set; }

        [JsonPropertyName("xnmc")]
        public string Xnmc { get; set; }

        [JsonPropertyName("xqm")]
        public string Xqm { get; set; }

        [JsonPropertyName("xqmc")]
        public string Xqmc { get; set; }

        [JsonPropertyName("xqmmc")]
        public string Xqmmc { get; set; }

        [JsonPropertyName("zxbj")]
        public string Zxbj { get; set; }

        [JsonPropertyName("zymc")]
        public string Zymc { get; set; }
    }
}
