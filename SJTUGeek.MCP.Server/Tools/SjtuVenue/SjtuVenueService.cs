using Newtonsoft.Json;
using SJTUGeek.MCP.Server.Helpers;
using SJTUGeek.MCP.Server.Modules;
using System.Text;

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
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            HttpRequestMessage req2 = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/system/user/currentUser");
            var res2 = await _client.SendAsync(req2);
            if (!res2.IsSuccessStatusCode)
            {
                throw new Exception($"登录失败，服务器响应{res2.StatusCode}");
            }

            if (res2.Content.Headers.ContentLength == 0)
            {
                throw new Exception($"登录失败，服务器返回空");
            }

            var json2 = JsonConvert.DeserializeObject<VenueResWrapper<VenueUserInfo>>(await res2.Content.ReadAsStringAsync());

            if (json2.Code != 0)
            {
                throw new Exception($"登录失败：{json2.Msg}");
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
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenuePagedResWrapper<VenueItem>>(await res.Content.ReadAsStringAsync());
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Rows;
        }

        public VenueInfoDto GetVenueInfo(string venueId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/manage/venue/queryVenueById");
            Dictionary<string, string> forms = new Dictionary<string, string>();
            forms.Add("id", venueId);
            FormUrlEncodedContent content = new FormUrlEncodedContent(forms);
            req.Content = content;
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<VenueInfoDto>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public List<VenueDateInfo> GetDateInfo() //string venueId, string fieldId, string date
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
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var body = res.Content.ReadAsStringAsync().Result;

            var json = JsonConvert.DeserializeObject<VenueResWrapper<List<VenueDateInfo>>>(body);

            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public List<VenueFieldInfo> GetFieldInfo(string venueId, string fieldId, string time, string dateId)
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
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<List<VenueFieldInfo>>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public string Reserve(VenueOrderInput input)
        {
            var jsonstr = JsonConvert.SerializeObject(input);
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

            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }
            if (res.Content.Headers.ContentType.MediaType == "text/html")
            {
                var parser = new AngleSharp.Html.Parser.HtmlParser();
                var document = parser.ParseDocument(res.Content.ReadAsStringAsync().Result);

                var cell = document.QuerySelector("div.error-desc");
                if (cell == null)
                    throw new Exception($"请求失败，服务器返回非json");
                throw new Exception(cell.TextContent.Trim());
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<string>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public VenueOrderInfo GetOrderInfo(string orderId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/venue/personal/queryOrder");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<VenueOrderInfo>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }        
        
        public List<VenueOrderInfo> GetOrderList()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get, "https://sports.sjtu.edu.cn/venue/personal/personalOrderlist?pageNo=1&pageSize=10");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueOrderListResWrapper>(res.Content.ReadAsStringAsync().Result);

            return json.Records;
        }        
        
        public bool CancelOrder(string orderId)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/tRefundReceipt/tRefundReceipt/createUserReceipt");
            Dictionary<string, string> forms = new Dictionary<string, string>();
            forms.Add("orderId", orderId);
            forms.Add("type", "2");
            FormUrlEncodedContent content = new FormUrlEncodedContent(forms);
            req.Content = content;
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<string>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 4)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return true;
        }

        public VenueAccompanyListInfo GetAccompanyInfo()
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/relatives/entourage/queryEntourage" + "?pageNum=1&pageSize=100");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<VenueAccompanyListInfo>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }

        public int SaveAccompanies(VenueAccompanySaveInput input)
        {
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "https://sports.sjtu.edu.cn/venue/personal/saveAccompanyingOrders");
            var jsoninput = JsonConvert.SerializeObject(input);
            req.Content = new StringContent(jsoninput);
            req.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var res = _client.SendAsync(req).Result;
            if (!res.IsSuccessStatusCode)
            {
                throw new Exception($"请求失败，服务器响应{res.StatusCode}");
            }

            var json = JsonConvert.DeserializeObject<VenueResWrapper<int>>(res.Content.ReadAsStringAsync().Result);
            if (json.Code != 0)
            {
                throw new Exception($"请求失败：{json.Msg}");
            }

            return json.Data;
        }
    }
}
