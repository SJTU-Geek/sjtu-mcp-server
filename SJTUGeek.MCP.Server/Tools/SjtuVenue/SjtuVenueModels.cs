using System.Text.Json;
using System.Text.Json.Serialization;

namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    public partial class VenueResWrapper<T>
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }
    }

    public partial class VenuePagedResWrapper<T>
    {
        [JsonPropertyName("code")]
        public long Code { get; set; }

        [JsonPropertyName("msg")]
        public long Msg { get; set; }

        [JsonPropertyName("rows")]
        public List<T> Rows { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class VenueOrderListResWrapper
    {
        [JsonPropertyName("current")]
        public long Current { get; set; }

        [JsonPropertyName("hitCount")]
        public bool HitCount { get; set; }

        [JsonPropertyName("optimizeCountSql")]
        public bool OptimizeCountSql { get; set; }

        [JsonPropertyName("orders")]
        public List<string> Orders { get; set; }

        [JsonPropertyName("pages")]
        public long Pages { get; set; }

        [JsonPropertyName("records")]
        public List<VenueOrderInfo> Records { get; set; }

        [JsonPropertyName("searchCount")]
        public bool SearchCount { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class VenueUserInfo
    {
        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        [JsonPropertyName("balance")]
        public string Balance { get; set; }

        [JsonPropertyName("classNo")]
        public string ClassNo { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("createTime")]
        public string CreateTime { get; set; }

        [JsonPropertyName("delFlag")]
        public string DelFlag { get; set; }

        [JsonPropertyName("dept")]
        public VenueDeptInfo Dept { get; set; }

        [JsonPropertyName("deptId")]
        public long DeptId { get; set; }

        [JsonPropertyName("loginName")]
        public string LoginName { get; set; }

        //[JsonPropertyName("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonPropertyName("phonenumber")]
        public string Phonenumber { get; set; }

        [JsonPropertyName("retirer")]
        public string Retirer { get; set; }

        //[JsonPropertyName("roles")]
        //public List<string> Roles { get; set; }

        [JsonPropertyName("sex")]
        public string Sex { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("userId")]
        public long UserId { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("userType")]
        public string UserType { get; set; }

        //[JsonPropertyName("venues")]
        //public List<string> Venues { get; set; }
    }

    public partial class VenueDeptInfo
    {
        [JsonPropertyName("deptId")]
        public long DeptId { get; set; }

        [JsonPropertyName("deptName")]
        public string DeptName { get; set; }

        [JsonPropertyName("orderNum")]
        public string OrderNum { get; set; }

        //[JsonPropertyName("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonPropertyName("parentId")]
        public long ParentId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }
    }

    public partial class VenueMotionType
    {
        [JsonPropertyName("img")]
        public string Img { get; set; }

        [JsonPropertyName("venueType")]
        public string VenueType { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("venueTypeEn")]
        public string VenueTypeEn { get; set; }
    }

    public partial class VenueItem
    {
        [JsonPropertyName("campusId")]
        public string CampusId { get; set; }

        [JsonPropertyName("campusName")]
        public string CampusName { get; set; }

        [JsonPropertyName("createBy")]
        public string CreateBy { get; set; }

        [JsonPropertyName("createTime")]
        public string CreateTime { get; set; }

        [JsonPropertyName("images")]
        public string Images { get; set; }

        [JsonPropertyName("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("openTime")]
        public string OpenTime { get; set; }

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }

        [JsonPropertyName("otherPageNum")]
        public long OtherPageNum { get; set; }

        [JsonPropertyName("otherPageSize")]
        public long OtherPageSize { get; set; }

        //[JsonPropertyName("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonPropertyName("updateBy")]
        public string UpdateBy { get; set; }

        [JsonPropertyName("updateTime")]
        public string UpdateTime { get; set; }

        [JsonPropertyName("venueAddr")]
        public string VenueAddr { get; set; }

        [JsonPropertyName("venueAmin")]
        public string VenueAmin { get; set; }

        [JsonPropertyName("venueFacilities")]
        public string VenueFacilities { get; set; }

        [JsonPropertyName("venueId")]
        public string VenueId { get; set; }

        [JsonPropertyName("venueInfo")]
        public string VenueInfo { get; set; }

        [JsonPropertyName("venueMobile")]
        public string VenueMobile { get; set; }

        [JsonPropertyName("venueName")]
        public string VenueName { get; set; }

        [JsonPropertyName("venueNameEn")]
        public string VenueNameEn { get; set; }

        [JsonPropertyName("venueRule")]
        public string VenueRule { get; set; }

        [JsonPropertyName("venueRuleEn")]
        public string VenueRuleEn { get; set; }

        [JsonPropertyName("venueState")]
        public string VenueState { get; set; }
    }

    public partial class VenueInfoDto
    {
        [JsonPropertyName("campusId")]
        public string CampusId { get; set; }

        [JsonPropertyName("campusName")]
        public string CampusName { get; set; }

        [JsonPropertyName("createBy")]
        public string CreateBy { get; set; }

        [JsonPropertyName("createTime")]
        public string CreateTime { get; set; }

        [JsonPropertyName("images")]
        public string Images { get; set; }

        [JsonPropertyName("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("motionTypes")]
        public List<VenueInfoMotionType> MotionTypes { get; set; }

        [JsonPropertyName("openTime")]
        public string OpenTime { get; set; }

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }

        [JsonPropertyName("otherPageNum")]
        public long OtherPageNum { get; set; }

        [JsonPropertyName("otherPageSize")]
        public long OtherPageSize { get; set; }

        //[JsonPropertyName("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonPropertyName("updateBy")]
        public string UpdateBy { get; set; }

        [JsonPropertyName("updateTime")]
        public string UpdateTime { get; set; }

        [JsonPropertyName("venueAddr")]
        public string VenueAddr { get; set; }

        [JsonPropertyName("venueAmin")]
        public string VenueAmin { get; set; }

        [JsonPropertyName("venueFacilities")]
        public string VenueFacilities { get; set; }

        [JsonPropertyName("venueId")]
        public string VenueId { get; set; }

        [JsonPropertyName("venueInfo")]
        public string VenueInfo { get; set; }

        [JsonPropertyName("venueMobile")]
        public string VenueMobile { get; set; }

        [JsonPropertyName("venueName")]
        public string VenueName { get; set; }

        [JsonPropertyName("venueRule")]
        public string VenueRule { get; set; }

        [JsonPropertyName("venueState")]
        public string VenueState { get; set; }
    }

    public partial class VenueInfoMotionType
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; }

        [JsonPropertyName("nowDate")]
        public string NowDate { get; set; }

        [JsonPropertyName("tension")]
        public string Tension { get; set; }
    }

    public partial class VenueDateInfo
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("dateId")]
        public string DateId { get; set; }

        [JsonPropertyName("isAll")]
        public bool IsAll { get; set; }

        [JsonPropertyName("open")]
        public bool Open { get; set; }
    }

    public partial class VenueFieldInfo
    {
        [JsonPropertyName("detailSort")]
        public long DetailSort { get; set; }

        [JsonPropertyName("fieldDetailStatus")]
        public string FieldDetailStatus { get; set; }

        [JsonPropertyName("fieldId")]
        public string FieldId { get; set; }

        [JsonPropertyName("fieldName")]
        public string FieldName { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("imageWhite")]
        public string ImageWhite { get; set; }

        [JsonPropertyName("priceList")]
        public List<VenueFieldPriceInfo> PriceList { get; set; }
    }

    public partial class VenueFieldPriceInfo
    {
        [JsonPropertyName("count")]
        public string Count { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        [JsonPropertyName("price")]
        public string Price { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }
    }

    public partial class VenueOrderInput
    {
        [JsonPropertyName("fieldType")]
        public string FieldType { get; set; }

        [JsonPropertyName("returnUrl")]
        public string ReturnUrl { get; set; }

        [JsonPropertyName("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonPropertyName("spaces")]
        public List<VenueOrderSpaceInput> Spaces { get; set; }

        [JsonPropertyName("tenSity")]
        public string TenSity { get; set; }

        [JsonPropertyName("venTypeId")]
        public string VenTypeId { get; set; }

        [JsonPropertyName("venueId")]
        public string VenueId { get; set; }

        [JsonPropertyName("week")]
        public string Week { get; set; }
    }

    public partial class VenueOrderSpaceInput
    {
        [JsonPropertyName("count")]
        public long? Count { get; set; }

        [JsonPropertyName("scheduleTime")]
        public string ScheduleTime { get; set; }

        [JsonPropertyName("sign")]
        public string Sign { get; set; }

        [JsonPropertyName("status")]
        public long? Status { get; set; }

        [JsonPropertyName("subSiteId")]
        public string SubSiteId { get; set; }

        [JsonPropertyName("subSitename")]
        public string SubSitename { get; set; }

        [JsonPropertyName("tensity")]
        public string Tensity { get; set; }

        [JsonPropertyName("venueNum")]
        public long? VenueNum { get; set; }

        [JsonPropertyName("venuePrice")]
        public string VenuePrice { get; set; }
    }

    public partial class VenueOrderInfo
    {
        [JsonPropertyName("pOrderid")]
        public string Id { get; set; }

        [JsonPropertyName("accompanyings")]
        public List<VenueOrderAccompanyInfo> Accompanyings { get; set; }

        [JsonPropertyName("actPrice")]
        public long ActPrice { get; set; }

        [JsonPropertyName("arrears")]
        public long Arrears { get; set; }

        [JsonPropertyName("campusName")]
        public string CampusName { get; set; }

        [JsonPropertyName("countPrice")]
        public long CountPrice { get; set; }

        [JsonPropertyName("images")]
        public string Images { get; set; }

        [JsonPropertyName("isEntourage")]
        public string IsEntourage { get; set; }

        [JsonPropertyName("maxNum")]
        public long MaxNum { get; set; }

        [JsonPropertyName("orderstateid")]
        public string OrderStateId { get; set; }

        [JsonPropertyName("ordercreatement")]
        public string OrderCreatement { get; set; }

        [JsonPropertyName("price")]
        public long Price { get; set; }

        [JsonPropertyName("refundPrice")]
        public long RefundPrice { get; set; }

        [JsonPropertyName("remainingTime")]
        public long RemainingTime { get; set; }

        [JsonPropertyName("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonPropertyName("scDate")]
        public string ScDate { get; set; }

        [JsonPropertyName("spaces")]
        public List<VenueOrderSpaceInfo> Spaces { get; set; }

        [JsonPropertyName("spaceInfo")]
        public string SpaceInfo { get; set; }

        [JsonPropertyName("sumMoney")]
        public long SumMoney { get; set; }

        [JsonPropertyName("venueAddress")]
        public string VenueAddress { get; set; }

        [JsonPropertyName("venueName")]
        public string VenueName { get; set; }

        [JsonPropertyName("week")]
        public string Week { get; set; }
    }

    public partial class VenueOrderAccompanyInfo
    {
        [JsonPropertyName("callBack")]
        public string CallBack { get; set; }

        [JsonPropertyName("countPrice")]
        public long? CountPrice { get; set; }

        [JsonPropertyName("deptName")]
        public string DeptName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("isavaliable")]
        public string Isavaliable { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("orderCreatement")]
        public string OrderCreatement { get; set; }

        [JsonPropertyName("orderFinishTime")]
        public string OrderFinishTime { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("orderStateId")]
        public string OrderStateId { get; set; }

        [JsonPropertyName("orderUpdateTime")]
        public string OrderUpdateTime { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("venId")]
        public string VenId { get; set; }

        [JsonPropertyName("venName")]
        public string VenName { get; set; }

        [JsonPropertyName("venueAddress")]
        public string VenueAddress { get; set; }

        [JsonPropertyName("venueId")]
        public string VenueId { get; set; }

        [JsonPropertyName("venueName")]
        public string VenueName { get; set; }

        [JsonPropertyName("writeCode")]
        public string WriteCode { get; set; }
    }

    public partial class VenueOrderSpaceInfo
    {
        [JsonPropertyName("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonPropertyName("isUsed")]
        public string IsUsed { get; set; }

        [JsonPropertyName("lockField")]
        public string LockField { get; set; }

        [JsonPropertyName("maxNum")]
        public long? MaxNum { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("orderType")]
        public string OrderType { get; set; }

        //[JsonPropertyName("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonPropertyName("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonPropertyName("scheduleId")]
        public string ScheduleId { get; set; }

        [JsonPropertyName("scheduleTime")]
        public string ScheduleTime { get; set; }

        [JsonPropertyName("subSiteId")]
        public string SubSiteId { get; set; }

        [JsonPropertyName("subSitename")]
        public string SubSitename { get; set; }

        [JsonPropertyName("tensity")]
        public string Tensity { get; set; }

        [JsonPropertyName("venued")]
        public string Venued { get; set; }

        [JsonPropertyName("venueId")]
        public string VenueId { get; set; }

        [JsonPropertyName("venueName")]
        public string VenueName { get; set; }

        [JsonPropertyName("venueNum")]
        public long? VenueNum { get; set; }

        [JsonPropertyName("venuePrice")]
        public long? VenuePrice { get; set; }

        [JsonPropertyName("venueWeek")]
        public string VenueWeek { get; set; }
    }

    public partial class VenueAccompanyListInfo
    {
        [JsonPropertyName("current")]
        public long Current { get; set; }

        [JsonPropertyName("hitCount")]
        public bool HitCount { get; set; }

        [JsonPropertyName("optimizeCountSql")]
        public bool OptimizeCountSql { get; set; }

        //[JsonPropertyName("orders")]
        //public List<string> Orders { get; set; }

        [JsonPropertyName("pages")]
        public long Pages { get; set; }

        [JsonPropertyName("records")]
        public List<VenueAccompanyRecord> Records { get; set; }

        [JsonPropertyName("searchCount")]
        public bool SearchCount { get; set; }

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("total")]
        public long Total { get; set; }
    }

    public partial class VenueAccompanyRecord
    {
        [JsonPropertyName("checked")]
        public bool Checked { get; set; }

        [JsonPropertyName("creatby")]
        public string Creatby { get; set; }

        [JsonPropertyName("creatDate")]
        public string CreatDate { get; set; }

        [JsonPropertyName("dept")]
        public string Dept { get; set; }

        [JsonPropertyName("deptName")]
        public string DeptName { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonPropertyName("loginName")]
        public string LoginName { get; set; }

        [JsonPropertyName("mobile")]
        public string Mobile { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
    }

    public partial class VenueAccompanySaveInput
    {
        [JsonPropertyName("entourageFamilies")]
        public List<VenueAccompanyRecord> EntourageFamilies { get; set; }

        [JsonPropertyName("flag")]
        public long Flag { get; set; }

        [JsonPropertyName("orderId")]
        public string OrderId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    [JsonSerializable(typeof(VenueResWrapper<VenueUserInfo>))]
    [JsonSerializable(typeof(VenuePagedResWrapper<VenueItem>))]
    [JsonSerializable(typeof(List<VenueMotionType>))]
    [JsonSerializable(typeof(VenueResWrapper<VenueInfoDto>))]
    [JsonSerializable(typeof(VenueResWrapper<List<VenueDateInfo>>))]
    [JsonSerializable(typeof(VenueResWrapper<List<VenueFieldInfo>>))]
    [JsonSerializable(typeof(VenueResWrapper<string>))]
    [JsonSerializable(typeof(VenueResWrapper<int>))]
    [JsonSerializable(typeof(VenueResWrapper<VenueOrderInfo>))]
    [JsonSerializable(typeof(VenueOrderListResWrapper))]
    [JsonSerializable(typeof(VenueResWrapper<VenueAccompanyListInfo>))]
    [JsonSerializable(typeof(VenueOrderInput))]
    [JsonSerializable(typeof(VenueAccompanySaveInput))]
    internal partial class VenueModelContext : JsonSerializerContext
    {
    }
}
