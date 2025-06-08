using Newtonsoft.Json;

namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    public partial class VenueResWrapper<T>
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }
    }

    public partial class VenuePagedResWrapper<T>
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public long Msg { get; set; }

        [JsonProperty("rows")]
        public List<T> Rows { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class VenueOrderListResWrapper
    {
        [JsonProperty("current")]
        public long Current { get; set; }

        [JsonProperty("hitCount")]
        public bool HitCount { get; set; }

        [JsonProperty("optimizeCountSql")]
        public bool OptimizeCountSql { get; set; }

        [JsonProperty("orders")]
        public List<string> Orders { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("records")]
        public List<VenueOrderInfo> Records { get; set; }

        [JsonProperty("searchCount")]
        public bool SearchCount { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class VenueUserInfo
    {
        [JsonProperty("admin")]
        public bool Admin { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("classNo")]
        public string ClassNo { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("createTime")]
        public string CreateTime { get; set; }

        [JsonProperty("delFlag")]
        public string DelFlag { get; set; }

        [JsonProperty("dept")]
        public VenueDeptInfo Dept { get; set; }

        [JsonProperty("deptId")]
        public long DeptId { get; set; }

        [JsonProperty("loginName")]
        public string LoginName { get; set; }

        //[JsonProperty("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonProperty("phonenumber")]
        public string Phonenumber { get; set; }

        [JsonProperty("retirer")]
        public string Retirer { get; set; }

        //[JsonProperty("roles")]
        //public List<string> Roles { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("userType")]
        public string UserType { get; set; }

        //[JsonProperty("venues")]
        //public List<string> Venues { get; set; }
    }

    public partial class VenueDeptInfo
    {
        [JsonProperty("deptId")]
        public long DeptId { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("orderNum")]
        public string OrderNum { get; set; }

        //[JsonProperty("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonProperty("parentId")]
        public long ParentId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public partial class VenueMotionType
    {
        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("venueType")]
        public string VenueType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("venueTypeEn")]
        public string VenueTypeEn { get; set; }
    }

    public partial class VenueItem
    {
        [JsonProperty("campusId")]
        public string CampusId { get; set; }

        [JsonProperty("campusName")]
        public string CampusName { get; set; }

        [JsonProperty("createBy")]
        public string CreateBy { get; set; }

        [JsonProperty("createTime")]
        public string CreateTime { get; set; }

        [JsonProperty("images")]
        public string Images { get; set; }

        [JsonProperty("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        [JsonProperty("otherPageNum")]
        public long OtherPageNum { get; set; }

        [JsonProperty("otherPageSize")]
        public long OtherPageSize { get; set; }

        //[JsonProperty("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonProperty("updateBy")]
        public string UpdateBy { get; set; }

        [JsonProperty("updateTime")]
        public string UpdateTime { get; set; }

        [JsonProperty("venueAddr")]
        public string VenueAddr { get; set; }

        [JsonProperty("venueAmin")]
        public string VenueAmin { get; set; }

        [JsonProperty("venueFacilities")]
        public string VenueFacilities { get; set; }

        [JsonProperty("venueId")]
        public string VenueId { get; set; }

        [JsonProperty("venueInfo")]
        public string VenueInfo { get; set; }

        [JsonProperty("venueMobile")]
        public string VenueMobile { get; set; }

        [JsonProperty("venueName")]
        public string VenueName { get; set; }

        [JsonProperty("venueNameEn")]
        public string VenueNameEn { get; set; }

        [JsonProperty("venueRule")]
        public string VenueRule { get; set; }

        [JsonProperty("venueRuleEn")]
        public string VenueRuleEn { get; set; }

        [JsonProperty("venueState")]
        public string VenueState { get; set; }
    }

    public partial class VenueInfoDto
    {
        [JsonProperty("campusId")]
        public string CampusId { get; set; }

        [JsonProperty("campusName")]
        public string CampusName { get; set; }

        [JsonProperty("createBy")]
        public string CreateBy { get; set; }

        [JsonProperty("createTime")]
        public string CreateTime { get; set; }

        [JsonProperty("images")]
        public string Images { get; set; }

        [JsonProperty("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("motionTypes")]
        public List<VenueInfoMotionType> MotionTypes { get; set; }

        [JsonProperty("openTime")]
        public string OpenTime { get; set; }

        [JsonProperty("orderType")]
        public string OrderType { get; set; }

        [JsonProperty("otherPageNum")]
        public long OtherPageNum { get; set; }

        [JsonProperty("otherPageSize")]
        public long OtherPageSize { get; set; }

        //[JsonProperty("params")]
        //public Dictionary<string, object> Params { get; set; }

        [JsonProperty("updateBy")]
        public string UpdateBy { get; set; }

        [JsonProperty("updateTime")]
        public string UpdateTime { get; set; }

        [JsonProperty("venueAddr")]
        public string VenueAddr { get; set; }

        [JsonProperty("venueAmin")]
        public string VenueAmin { get; set; }

        [JsonProperty("venueFacilities")]
        public string VenueFacilities { get; set; }

        [JsonProperty("venueId")]
        public string VenueId { get; set; }

        [JsonProperty("venueInfo")]
        public string VenueInfo { get; set; }

        [JsonProperty("venueMobile")]
        public string VenueMobile { get; set; }

        [JsonProperty("venueName")]
        public string VenueName { get; set; }

        [JsonProperty("venueRule")]
        public string VenueRule { get; set; }

        [JsonProperty("venueState")]
        public string VenueState { get; set; }
    }

    public partial class VenueInfoMotionType
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nowDate")]
        public string NowDate { get; set; }

        [JsonProperty("tension")]
        public string Tension { get; set; }
    }

    public partial class VenueDateInfo
    {
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("dateId")]
        public string DateId { get; set; }

        [JsonProperty("isAll")]
        public bool IsAll { get; set; }

        [JsonProperty("open")]
        public bool Open { get; set; }
    }

    public partial class VenueFieldInfo
    {
        [JsonProperty("detailSort")]
        public long DetailSort { get; set; }

        [JsonProperty("fieldDetailStatus")]
        public string FieldDetailStatus { get; set; }

        [JsonProperty("fieldId")]
        public string FieldId { get; set; }

        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("imageWhite")]
        public string ImageWhite { get; set; }

        [JsonProperty("priceList")]
        public List<VenueFieldPriceInfo> PriceList { get; set; }
    }

    public partial class VenueFieldPriceInfo
    {
        [JsonProperty("count")]
        public string Count { get; set; }

        [JsonProperty("flag")]
        public string Flag { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("sign")]
        public string Sign { get; set; }
    }

    public partial class VenueOrderInput
    {
        [JsonProperty("fieldType")]
        public string FieldType { get; set; }

        [JsonProperty("returnUrl")]
        public string ReturnUrl { get; set; }

        [JsonProperty("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonProperty("spaces")]
        public List<VenueOrderSpaceInput> Spaces { get; set; }

        [JsonProperty("tenSity")]
        public string TenSity { get; set; }

        [JsonProperty("venTypeId")]
        public string VenTypeId { get; set; }

        [JsonProperty("venueId")]
        public string VenueId { get; set; }

        [JsonProperty("week")]
        public string Week { get; set; }
    }

    public partial class VenueOrderSpaceInput
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public long? Count { get; set; }

        [JsonProperty("scheduleTime", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduleTime { get; set; }

        [JsonProperty("sign", NullValueHandling = NullValueHandling.Ignore)]
        public string Sign { get; set; }

        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public long? Status { get; set; }

        [JsonProperty("subSiteId", NullValueHandling = NullValueHandling.Ignore)]
        public string SubSiteId { get; set; }

        [JsonProperty("subSitename", NullValueHandling = NullValueHandling.Ignore)]
        public string SubSitename { get; set; }

        [JsonProperty("tensity", NullValueHandling = NullValueHandling.Ignore)]
        public string Tensity { get; set; }

        [JsonProperty("venueNum", NullValueHandling = NullValueHandling.Ignore)]
        public long? VenueNum { get; set; }

        [JsonProperty("venuePrice", NullValueHandling = NullValueHandling.Ignore)]
        public string VenuePrice { get; set; }
    }

    public partial class VenueOrderInfo
    {
        [JsonProperty("pOrderid")]
        public string Id { get; set; }

        [JsonProperty("accompanyings")]
        public List<VenueOrderAccompanyInfo> Accompanyings { get; set; }

        [JsonProperty("actPrice")]
        public long ActPrice { get; set; }

        [JsonProperty("arrears")]
        public long Arrears { get; set; }

        [JsonProperty("campusName")]
        public string CampusName { get; set; }

        [JsonProperty("countPrice")]
        public long CountPrice { get; set; }

        [JsonProperty("images")]
        public string Images { get; set; }

        [JsonProperty("isEntourage")]
        public string IsEntourage { get; set; }

        [JsonProperty("maxNum")]
        public long MaxNum { get; set; }

        [JsonProperty("orderstateid")]
        public string OrderStateId { get; set; }

        [JsonProperty("ordercreatement")]
        public string OrderCreatement { get; set; }

        [JsonProperty("price")]
        public long Price { get; set; }

        [JsonProperty("refundPrice")]
        public long RefundPrice { get; set; }

        [JsonProperty("remainingTime")]
        public long RemainingTime { get; set; }

        [JsonProperty("scheduleDate")]
        public string ScheduleDate { get; set; }

        [JsonProperty("scDate")]
        public string ScDate { get; set; }

        [JsonProperty("spaces")]
        public List<VenueOrderSpaceInfo> Spaces { get; set; }

        [JsonProperty("spaceInfo")]
        public string SpaceInfo { get; set; }

        [JsonProperty("sumMoney")]
        public long SumMoney { get; set; }

        [JsonProperty("venueAddress")]
        public string VenueAddress { get; set; }

        [JsonProperty("venueName")]
        public string VenueName { get; set; }

        [JsonProperty("week")]
        public string Week { get; set; }
    }

    public partial class VenueOrderAccompanyInfo
    {
        [JsonProperty("callBack", NullValueHandling = NullValueHandling.Ignore)]
        public string CallBack { get; set; }

        [JsonProperty("countPrice", NullValueHandling = NullValueHandling.Ignore)]
        public long? CountPrice { get; set; }

        [JsonProperty("deptName", NullValueHandling = NullValueHandling.Ignore)]
        public string DeptName { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty("isavaliable", NullValueHandling = NullValueHandling.Ignore)]
        public string Isavaliable { get; set; }

        [JsonProperty("mobile", NullValueHandling = NullValueHandling.Ignore)]
        public string Mobile { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("orderCreatement", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderCreatement { get; set; }

        [JsonProperty("orderFinishTime", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderFinishTime { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty("orderStateId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderStateId { get; set; }

        [JsonProperty("orderUpdateTime", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderUpdateTime { get; set; }

        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public string UserId { get; set; }

        [JsonProperty("venId", NullValueHandling = NullValueHandling.Ignore)]
        public string VenId { get; set; }

        [JsonProperty("venName", NullValueHandling = NullValueHandling.Ignore)]
        public string VenName { get; set; }

        [JsonProperty("venueAddress", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueAddress { get; set; }

        [JsonProperty("venueId", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueId { get; set; }

        [JsonProperty("venueName", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueName { get; set; }

        [JsonProperty("writeCode", NullValueHandling = NullValueHandling.Ignore)]
        public string WriteCode { get; set; }
    }

    public partial class VenueOrderSpaceInfo
    {
        [JsonProperty("isAvailable", NullValueHandling = NullValueHandling.Ignore)]
        public string IsAvailable { get; set; }

        [JsonProperty("isUsed", NullValueHandling = NullValueHandling.Ignore)]
        public string IsUsed { get; set; }

        [JsonProperty("lockField", NullValueHandling = NullValueHandling.Ignore)]
        public string LockField { get; set; }

        [JsonProperty("maxNum", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxNum { get; set; }

        [JsonProperty("orderId", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderId { get; set; }

        [JsonProperty("orderType", NullValueHandling = NullValueHandling.Ignore)]
        public string OrderType { get; set; }

        //[JsonProperty("params", NullValueHandling = NullValueHandling.Ignore)]
        //public Dictionary<string, object> Params { get; set; }

        [JsonProperty("scheduleDate", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduleDate { get; set; }

        [JsonProperty("scheduleId", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduleId { get; set; }

        [JsonProperty("scheduleTime", NullValueHandling = NullValueHandling.Ignore)]
        public string ScheduleTime { get; set; }

        [JsonProperty("subSiteId", NullValueHandling = NullValueHandling.Ignore)]
        public string SubSiteId { get; set; }

        [JsonProperty("subSitename", NullValueHandling = NullValueHandling.Ignore)]
        public string SubSitename { get; set; }

        [JsonProperty("tensity", NullValueHandling = NullValueHandling.Ignore)]
        public string Tensity { get; set; }

        [JsonProperty("venued", NullValueHandling = NullValueHandling.Ignore)]
        public string Venued { get; set; }

        [JsonProperty("venueId", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueId { get; set; }

        [JsonProperty("venueName", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueName { get; set; }

        [JsonProperty("venueNum", NullValueHandling = NullValueHandling.Ignore)]
        public long? VenueNum { get; set; }

        [JsonProperty("venuePrice", NullValueHandling = NullValueHandling.Ignore)]
        public long? VenuePrice { get; set; }

        [JsonProperty("venueWeek", NullValueHandling = NullValueHandling.Ignore)]
        public string VenueWeek { get; set; }
    }

    public partial class VenueAccompanyListInfo
    {
        [JsonProperty("current")]
        public long Current { get; set; }

        [JsonProperty("hitCount")]
        public bool HitCount { get; set; }

        [JsonProperty("optimizeCountSql")]
        public bool OptimizeCountSql { get; set; }

        //[JsonProperty("orders")]
        //public List<string> Orders { get; set; }

        [JsonProperty("pages")]
        public long Pages { get; set; }

        [JsonProperty("records")]
        public List<VenueAccompanyRecord> Records { get; set; }

        [JsonProperty("searchCount")]
        public bool SearchCount { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class VenueAccompanyRecord
    {
        [JsonProperty("checked")]
        public bool Checked { get; set; }

        [JsonProperty("creatby")]
        public string Creatby { get; set; }

        [JsonProperty("creatDate")]
        public string CreatDate { get; set; }

        [JsonProperty("dept")]
        public string Dept { get; set; }

        [JsonProperty("deptName")]
        public string DeptName { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isAvailable")]
        public string IsAvailable { get; set; }

        [JsonProperty("loginName")]
        public string LoginName { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }
    }

    public partial class VenueAccompanySaveInput
    {
        [JsonProperty("entourageFamilies")]
        public List<VenueAccompanyRecord> EntourageFamilies { get; set; }

        [JsonProperty("flag")]
        public long Flag { get; set; }

        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
