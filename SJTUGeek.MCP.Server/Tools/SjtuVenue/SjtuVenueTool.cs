using Microsoft.Extensions.AI;
using ModelContextProtocol;
using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SJTUGeek.MCP.Server.Extensions;
using SJTUGeek.MCP.Server.Helpers;
using System.ComponentModel;

namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    [McpServerToolType]
    public class SjtuVenueTool
    {
        private readonly ILogger<SjtuVenueTool> _logger;
        private readonly SjtuVenueService _venue;
        private readonly RerankHelper _rerank;

        public SjtuVenueTool(ILogger<SjtuVenueTool> logger, SjtuVenueService venue, RerankHelper rerank)
        {
            _logger = logger;
            _venue = venue;
            _rerank = rerank;
        }

        [McpServerTool(Name = "book_venue"), Description("Book a sports venue. All parameters must be real values from user except field_name (which can be null), otherwise ask user to supplement missing parameters first.")]
        public async Task<object> ToolBookVenue(
            [Description("Name of the venue to be booked.")]
            string venue_name,
            [Description("Name of sport to be booked (e.g. 乒乓球、篮球、健身房、钢琴)")]
            string room_or_sports_name,
            [Description("Name of the field in the venue. (use `null` if the user does not explicitly specify)")]
            string? field_name,
            [Description($"Appointment date in the format YYYY-MM-dd.")]
            string date,
            [Description("Appointment beginning time in the format HH:mm.")]
            string beginning_time,
            [Description("Appointment ending time in the format HH:mm.")]
            string ending_time
        )
        {
            await _venue.Login();
            VenueItem v = await SearchForVenue(venue_name);

            //return $"{venue_name} 匹配 {vs[matchedVenueInfo.Item2].VenueName}（可信度：{matchedVenueInfo.Item1}）";
            var v_info = await _venue.GetVenueInfo(v.VenueId);

            VenueMotionType m = await SearchForMotionType(room_or_sports_name, v_info.MotionTypes.Select(x=>new VenueMotionType() { Id = x.Id, Img = x.Image, VenueType = x.Name, VenueTypeEn = x.NameEn}).ToList());

            //return $"{room_or_sports_name} 匹配 {ms[matchedMotionInfo.Item2].VenueType}（可信度：{matchedMotionInfo.Item1}）";

            if (m == null)
            {
                throw new McpException($"找不到区域 {room_or_sports_name}");
            }
            var r = v_info.MotionTypes.First(x => x.Id == m.Id);

            var res35 = await _venue.GetDateInfo();
            var targetDate = res35.FirstOrDefault(x => x.Date == date);
            if (targetDate == null)
            {
                throw new McpException($"您要预约的日期尚未开放");
            }

            bool returnFieldInfo = true;
            var fs = await _venue.GetFieldInfo(v.VenueId, r.Id, date, targetDate.DateId);
            VenueFieldInfo? f = null;
            List<bool> fieldAllAvailable = Enumerable.Range(0, fs.Count).Select(x => true).ToList();
            if (field_name != null && field_name != "null")
            {
                f = fs.FirstOrDefault(x => x.FieldName == field_name);
                if (f == null)
                {
                    throw new McpException($"找不到场地 {f}");
                }
                returnFieldInfo = false;
            }

            DateOnly d = DateOnly.Parse(date);
            TimeOnly t1 = TimeOnly.Parse(beginning_time);
            TimeOnly t2 = TimeOnly.Parse(ending_time);
            TimeOnly[] times = SjtuVenueHelper.GetTimeIntervals(t1, t2, TimeSpan.FromHours(1));
            VenueOrderInput order = new VenueOrderInput();
            order.ReturnUrl = "https://sports.sjtu.edu.cn/#/paymentResult/1";
            order.VenueId = v.VenueId;
            order.FieldType = r.Name;
            order.VenTypeId = r.Id;
            order.ScheduleDate = date;
            order.TenSity = r.Tension switch { "0" => "正常", "1" => "紧张", "2" => "很紧张", "3" => "非常紧张", _ => "" };
            order.Week = ((int)d.DayOfWeek).ToString();
            order.Spaces = new List<VenueOrderSpaceInput>();
            foreach (var t in times)
            {
                var hour = t.Hour;
                if (hour < 7 || hour > 21)
                    throw new McpException($"无效的时间段：{hour}:00-{hour + 1}:00");
                if (returnFieldInfo)
                {
                    fieldAllAvailable = fieldAllAvailable.Zip(fs.Select(x => x.PriceList[hour - 7].Status == "0"), (a, b) => a && b).ToList();
                }
                else
                    order.Spaces.Add(new VenueOrderSpaceInput()
                    {
                        Count = 1,
                        VenuePrice = f.PriceList[hour - 7].Price,
                        Status = 1,
                        ScheduleTime = $"{hour.ToString("D2")}:00-{(hour + 1).ToString("D2")}:00",
                        SubSitename = f.FieldName,
                        SubSiteId = f.FieldId,
                        Tensity = f.FieldDetailStatus,
                        VenueNum = 1,
                        Sign = f.PriceList[hour - 7].Sign,
                    });
            }
            if (returnFieldInfo)
            {
                if (fieldAllAvailable.All(x => x == false))
                {
                    return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = $"请补全场地信息！（为您查询到 {v.VenueName} {m.VenueType} 所有场地在指定时段均无法预约，请您更换时间）" } } };
                }
                var fieldAllAvailableStr = string.Join("、", fs.Zip(fieldAllAvailable).Where(x => x.Second).Select(x => x.First.FieldName));
                return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = $"请补全场地信息！（为您查询到 {v.VenueName} {m.VenueType} 的 {fieldAllAvailableStr} 在指定时段空闲）" } } };
            }
            else
            {
                var res5 = await _venue.Reserve(order);
                return $"预约成功，请及时前往交我办“场馆预约”进行付款！（订单号：{res5}）";
            }

            //if (input.Accompany != null)
            //{
            //    var res6 = _venue.GetAccompanyInfo();
            //    if (!res6.Success)
            //    {
            //        return new CommonResult(false, res6.Message);
            //    }

            //    List<VenueAccompanyRecord> records = new List<VenueAccompanyRecord>();
            //    foreach (var p in res6.Result.Records)
            //    {
            //        if (input.Accompany.Contains(p.Name) || input.Accompany.Contains(p.UserId))
            //        {
            //            records.Add(p);
            //            p.Checked = true;
            //        }
            //    }

            //    var acc = new VenueAccompanySaveInput()
            //    {
            //        Flag = 2,
            //        Type = "1",
            //        OrderId = res5.Result,
            //        EntourageFamilies = records
            //    };

            //    var res7 = _venue.SaveAccompanies(acc);
            //    if (!res7.Success)
            //    {
            //        return new CommonResult(false, res7.Message);
            //    }
            //}
        }


        [McpServerTool(Name = "get_venue_info"), Description("Get information of a venue by its name.")]
        public async Task<object> ToolGetVenueInfo(
            [Description("Name of the venue to get information from.")]
            string venue_name
        )
        {
            await _venue.Login();
            VenueItem v = await SearchForVenue(venue_name);
            var res = await _venue.GetVenueInfo(v.VenueId);
            return RenderVenueInfo(res);
        }

        [McpServerTool(Name = "get_field_availability_map"), Description("Obtain a map of the venue reservation status for a specified venue, sport, and date.")]
        public async Task<object> ToolGetFieldAvailabilityMap(
            [Description("Name of the venue to be booked.")]
            string venue_name,
            [Description("Name of sport to be booked (e.g. 乒乓球、篮球、健身房、钢琴)")]
            string room_or_sports_name,
            [Description($"Appointment date in the format YYYY-MM-dd.")]
            string date
        )
        {
            await _venue.Login();
            VenueItem v = await SearchForVenue(venue_name);

            //return $"{venue_name} 匹配 {vs[matchedVenueInfo.Item2].VenueName}（可信度：{matchedVenueInfo.Item1}）";
            var v_info = await _venue.GetVenueInfo(v.VenueId);

            VenueMotionType m = await SearchForMotionType(room_or_sports_name, v_info.MotionTypes.Select(x => new VenueMotionType() { Id = x.Id, Img = x.Image, VenueType = x.Name, VenueTypeEn = x.NameEn }).ToList());

            //return $"{room_or_sports_name} 匹配 {ms[matchedMotionInfo.Item2].VenueType}（可信度：{matchedMotionInfo.Item1}）";

            if (m == null)
            {
                throw new McpException($"找不到区域 {room_or_sports_name}");
            }
            var r = v_info.MotionTypes.First(x => x.Id == m.Id);

            var res35 = await _venue.GetDateInfo();
            var targetDate = res35.FirstOrDefault(x => x.Date == date);
            if (targetDate == null)
            {
                throw new McpException($"您要预约的日期尚未开放");
            }

            var fs = await _venue.GetFieldInfo(v.VenueId, r.Id, date, targetDate.DateId);

            MemoryStream ms = new MemoryStream();
            Image<Rgba32> image = new(100, 100);
            image.Mutate(ctx => ctx.Fill(Color.Coral));
            await image.SaveAsPngAsync(ms);
            return new CallToolResult() {
                IsError = false,
                Content = new List<ContentBlock>() {
                    new TextContentBlock() { Text = $"{targetDate.Date} 的 {v.VenueName} {m.VenueType} 场地状态图" } ,
                    new DataContent(ms.ToArray(), "image/png").ToContent(),
                }
            };
        }

        [McpServerTool(Name = "get_venue_orders"), Description("Get personal venue reservation orders.")]
        public async Task<object> ToolGetPersonalOrders()
        {
            await _venue.Login();
            var res = await _venue.GetOrderList();
            return RenderOrderList(res);
        }

        [McpServerTool(Name = "cancel_venue_order"), Description("Cancel an venue reservation order by its ID.")]
        public async Task<object> ToolCancelOrder(
            string order_id
        )
        {
            await _venue.Login();
            var res = await _venue.CancelOrder(order_id);
            if (res)
            {
                return new CallToolResult() { IsError = false, Content = new List<ContentBlock>() { new TextContentBlock() { Text = "订单取消成功" } } };
            }
            else
            {
                return new CallToolResult() { IsError = true, Content = new List<ContentBlock>() { new TextContentBlock() { Text = $"订单取消失败" } } };
            }
        }

        private async Task<VenueItem> SearchForVenue(string venue_name)
        {
            var vs = await _venue.GetVenues();

            // simple fuzzy match
            vs.FindAll(v => v.VenueName == "霍英东体育中心").ForEach(v => v.VenueName = v.VenueName + "（霍体）");
            vs.FindAll(v => v.VenueName == "南区体育馆").ForEach(v => v.VenueName = v.VenueName + "（南体）");
            vs.FindAll(v => v.VenueName == "胡法光体育场").ForEach(v => v.VenueName = v.VenueName + "（光体）");
            vs.FindAll(v => v.VenueName == "学生创新中心").ForEach(v => v.VenueName = v.VenueName + "（学创）");

            var matchedVenueInfo = await _rerank.FindMostRelevant(venue_name, vs.Select(v => v.VenueName).ToList());
            var v = vs[matchedVenueInfo.Item2];

            if (matchedVenueInfo.Item1 < 0.5)
            {
                throw new McpException($"无法匹配场馆 {venue_name}，请检查输入是否正确");
            }

            return v;
        }

        private async Task<VenueMotionType> SearchForMotionType(string room_or_sports_name, List<VenueMotionType> motionTypes = null)
        {
            var ms = motionTypes ?? await _venue.GetMotionTypes();

            // add english
            var ms_doc = ms.Select(m => $"{m.VenueType}（{m.VenueTypeEn}）").ToList();

            var matchedMotionInfo = await _rerank.FindMostRelevant(room_or_sports_name, ms_doc);
            var m = ms[matchedMotionInfo.Item2];

            // print debug info
            _logger.LogInformation($"MotionType match: {room_or_sports_name} -> {m.VenueType}（{m.VenueTypeEn}），可信度：{matchedMotionInfo.Item1}");

            // it should be high
            if (matchedMotionInfo.Item1 < 0.7)
            {
                throw new McpException($"无法匹配运动类型 {room_or_sports_name}，请检查输入是否正确");
            }

            return m;
        }

        private string RenderVenueInfo(VenueInfoDto v)
        {
            var res =
            $"- {v.VenueName}" + "\n" +
            $"  场馆设施：{HtmlHelper.HtmlToPlainText(v.VenueFacilities)}" + "\n" +
            $"  场馆介绍：{HtmlHelper.HtmlToPlainText(v.VenueInfo)}" + "\n" +
            $"  使用须知：{HtmlHelper.HtmlToPlainText(v.VenueRule)}" + "\n" +
            ""
            ;
            return res;
        }

        private string RenderSingleOrder(VenueOrderInfo order)
        {
            var res =
            $"- 订单编号：{order.Id}" + "\n" +
            $"  场馆：{order.VenueName}" + "\n" +
            $"  日期：{order.ScDate}" + "\n" +
            $"  场地：{order.SpaceInfo}" + "\n" +
            $"  创建日期：{order.OrderCreatement}" + "\n" +
            "" // Todo: 随行人
            ;
            return res;
        }

        private string RenderOrderList(List<VenueOrderInfo> list)
        {
            if (list.Count == 0)
                return "还没有个人订单哦~";
            return string.Join('\n', list.Select(x => RenderSingleOrder(x)));
        }
    }
}
