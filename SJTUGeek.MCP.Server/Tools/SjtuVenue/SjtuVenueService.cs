using ModelContextProtocol;
using SJTUGeek.MCP.Server.Helpers;
using SJTUGeek.MCP.Server.Modules;
using System.Text;
using System.Text.Json;

namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    public class SjtuVenueService
    {
        private readonly HttpClient _client;

        public SjtuVenueService(HttpClientFactory clientFactory)
        {
            this._client = clientFactory.CreateClient();
        }

        public async Task<VenueUserInfo> Login()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "https://jaccount.sjtu.edu.cn/oauth2/authorize?response_type=code&scope=profile&client_id=mB5nKHqC00MusWAgnqSF&redirect_uri=https://sports.sjtu.edu.cn/oauth2Login");
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            HttpRequestMessage req2 = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/system/user/currentUser");
            var res2 = await _client.SendAsync(req2);
            if (!res2.IsSuccessStatusCode)
            {
                throw new McpException($"登录失败，服务器响应{res2.StatusCode}");
            }

            if (res2.Content.Headers.ContentLength == 0)
            {
                throw new McpException($"登录失败，服务器返回空");
            }

            var json2 = JsonSerializer.Deserialize<VenueResWrapper<VenueUserInfo>>(await res2.Content.ReadAsStringAsync());

            if (json2.Code != 0)
            {
                throw new McpException($"登录失败：{json2.Msg}");
            }

            return json2.Data;
        }

        public async Task<List<VenueItem>> GetVenues()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/venue/list");
            Dictionary<string, string> forms = new Dictionary<string, string>();
            forms.Add("pageNum", "1");
            forms.Add("pageSize", "100");
            forms.Add("flag", "0");
            FormUrlEncodedContent content = new FormUrlEncodedContent(forms);
            req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenuePagedResWrapper<VenueItem>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Rows;
        }

        public async Task<List<VenueMotionType>> GetMotionTypes()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/motionType/typeList");
            JsonContent content = JsonContent.Create("{}");
            req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<List<VenueMotionType>>(await res.Content.ReadAsStringAsync());
            if (json.Count == 0)
            {
                throw new McpException($"请求失败");
            }

            return json;
        }

        public async Task<VenueInfoDto> GetVenueInfo(string venueId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/venue/queryVenueById");
            Dictionary<string, string> forms = new Dictionary<string, string>();
            forms.Add("id", venueId);
            FormUrlEncodedContent content = new FormUrlEncodedContent(forms);
            req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<VenueInfoDto>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public async Task<List<VenueDateInfo>> GetDateInfo() //string venueId, string fieldId, string date
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/fieldDetail/queryFieldReserveSituationIsFull");
            //string payload = $$"""
            //    {
            //        "feildType": "{{fieldId}}",
            //        "date": "{{date}}",
            //        "id": "{{venueId}}"
            //    }
            //    """;
            //StringContent content = new StringContent(payload);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            //req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var body = await res.Content.ReadAsStringAsync();

            var json = JsonSerializer.Deserialize<VenueResWrapper<List<VenueDateInfo>>>(body);

            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public async Task<List<VenueFieldInfo>> GetFieldInfo(string venueId, string fieldId, string time, string dateId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/fieldDetail/queryFieldSituation");
            string payload = $$"""
                {
                    "fieldType": "{{fieldId}}",
                    "date": "{{time}}",
                    "dateId": "{{dateId}}",
                    "venueId": "{{venueId}}"
                }
                """;
            StringContent content = new StringContent(payload);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<List<VenueFieldInfo>>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public async Task<string> Reserve(VenueOrderInput input)
        {
            var jsonstr = JsonSerializer.Serialize(input);
            var jsonenc = AesHelper.Encrypt(
                jsonstr, 
                Encoding.Default.GetBytes(SjtuVenueHelper.AESKey),
                Encoding.Default.GetBytes(SjtuVenueHelper.AESKey)
            );

            StringContent content = new StringContent(Convert.ToBase64String(jsonenc));
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/venue/personal/ConfirmOrder");
            req.Content = content;
            req.Headers.Add("Sid", Convert.ToBase64String(
                RsaHelper.RSAEncrypt(
                    System.Text.Encoding.Default.GetBytes(SjtuVenueHelper.AESKey),
                    SjtuVenueHelper.PublicKey
                )
            ));
            req.Headers.Add("Tim", Convert.ToBase64String(
                RsaHelper.RSAEncrypt(
                    System.Text.Encoding.Default.GetBytes(TimeHelper.GetTimeStampMilliMinus8()),
                    SjtuVenueHelper.PublicKey
                )
            ));

            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }
            if (res.Content.Headers.ContentType.MediaType == "text/html")
            {
                var parser = new AngleSharp.Html.Parser.HtmlParser();
                var document = parser.ParseDocument(await res.Content.ReadAsStringAsync());

                var cell = document.QuerySelector("div.error-desc");
                if (cell == null)
                    throw new McpException($"请求失败，服务器返回非json");
                throw new McpException(cell.TextContent.Trim());
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<string>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public VenueOrderInfo GetOrderInfo(string orderId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/venue/personal/queryOrder");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<VenueOrderInfo>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }        
        
        public async Task<List<VenueOrderInfo>> GetOrderList()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/venue/personal/personalOrderlist?pageNo=1&pageSize=10");
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueOrderListResWrapper>(await res.Content.ReadAsStringAsync());

            return json.Records;
        }        
        
        public async Task<bool> CancelOrder(string orderId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/tRefundReceipt/tRefundReceipt/createUserReceipt");
            Dictionary<string, string> forms = new Dictionary<string, string>();
            forms.Add("orderId", orderId);
            forms.Add("type", "2");
            FormUrlEncodedContent content = new FormUrlEncodedContent(forms);
            req.Content = content;
            var res = await _client.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<string>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 4)
            {
                throw new McpException($"{json.Msg}");
            }

            return true;
        }

        public VenueAccompanyListInfo GetAccompanyInfo()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/relatives/entourage/queryEntourage" + "?pageNum=1&pageSize=100");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<VenueAccompanyListInfo>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public int SaveAccompanies(VenueAccompanySaveInput input)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/venue/personal/saveAccompanyingOrders");
            var jsoninput = JsonSerializer.Serialize(input);
            req.Content = new StringContent(jsoninput);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new McpException($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonSerializer.Deserialize<VenueResWrapper<int>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new McpException($"请求失败：{json.Msg}");
            }

            return json.Data;
        }
    }
}
