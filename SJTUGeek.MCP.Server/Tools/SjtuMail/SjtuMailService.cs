using ModelContextProtocol;
using SJTUGeek.MCP.Server.Modules;
using System.Net;
using System.Text.Json;
using Teru.Code.Zimbra;

namespace SJTUGeek.MCP.Server.Tools.SjtuMail
{
    public class SjtuMailService
    {
        private readonly ZimbraClient _zimbra;
        private readonly HttpClient _client;
        private readonly CookieContainerProvider _ccProvider;
        private readonly JaCookieProvider _cookieProvider;
        private readonly HttpClientFactory _clientFactory;

        private string? userToken;

        public SjtuMailService(CookieContainerProvider ccProvider, JaCookieProvider cookieProvider, HttpClientFactory clientFactory)
        {
            _ccProvider = ccProvider;
            _cookieProvider = cookieProvider;
            _clientFactory = clientFactory;
            _client = clientFactory.CreateClient();
            _zimbra = new ZimbraClient("https://mail.sjtu.edu.cn/service/soap");
        }

        public async Task<bool> Login()
        {
            var res = await _client.GetAsync("https://mail.sjtu.edu.cn");
            while (res.StatusCode == HttpStatusCode.Found && res.Headers.Location.Scheme == "http")
            {
                res = await _client.GetAsync(res.Headers.Location.OriginalString.Replace("http", "https"));
            }
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            if (!res.RequestMessage.RequestUri.OriginalString.StartsWith("https://mail.sjtu.edu.cn"))
            {
                throw new McpException("认证失败");
            }
            var cc = _ccProvider.GetCookieContainer(_cookieProvider.GetCookie());
            var cookies = cc.GetCookies(new Uri("https://mail.sjtu.edu.cn")).Cast<Cookie>().ToList();
            var tokenCookie = cookies.FirstOrDefault(x => x.Name == "ZM_AUTH_TOKEN");
            if (tokenCookie != null)
            {
                userToken = tokenCookie.Value;
            }
            else
            {
                throw new McpException("认证失败");
            }
            return true;
        }

        public async Task<ZimbraSearchResponse> GetMails(MailBoxTypeEnum mailbox, int page, int pageSize = 15)
        {
            JsonRequest request = _zimbra.GenRequest("json", userToken) as JsonRequest;
            request.AddRequest("SearchRequest", $$"""
                            {
                    "sortBy": "dateDesc",
                    "header": [
                        {
                        "n": "List-ID"
                        },
                        {
                        "n": "X-Zimbra-DL"
                        },
                        {
                        "n": "IN-REPLY-TO"
                        }
                    ],
                    "tz": {
                        "id": "Asia/Hong_Kong"
                    },
                    "locale": {
                        "_content": "zh_CN"
                    },
                    "offset": {{(page - 1) * pageSize}},
                    "limit": {{pageSize}},
                    "query": "in:{{mailbox.ToString().ToLower()}}",
                    "types": "message",
                    "recip": "0",
                    "needExp": 1
                }
                """,
                "urn:zimbraMail");
            var resp = await _zimbra.SendRequest(request);

            if (resp.IsFault())
            {
                var message = resp.GetFaultMessage().First().Value;
                throw new McpException(message);
            }
            else
            {
                var res = JsonSerializer.Deserialize<Dictionary<string, ZimbraSearchResponse>>(resp.GetResponse(), MailModelContext.Default.DictionaryStringZimbraSearchResponse);
                return res.First().Value;
            }
        }

        public async Task<ZimbraGetMsgResponse> GetSingleMail(int mailId, bool markRead)
        {
            JsonRequest request = _zimbra.GenRequest("json", userToken) as JsonRequest;
            request.AddRequest("GetMsgRequest", $$"""
                   {
                  "m": {
                    "id": "{{mailId}}",
                    "html": 1,
                    "read": {{(markRead ? 1 : 0)}},
                    "needExp": 1,
                    "header": [
                      {
                        "n": "List-ID"
                      },
                      {
                        "n": "X-Zimbra-DL"
                      },
                      {
                        "n": "IN-REPLY-TO"
                      }
                    ],
                    "max": 250000
                  }
                }
                """,
                "urn:zimbraMail");
            var resp = await _zimbra.SendRequest(request);

            if (resp.IsFault())
            {
                var message = resp.GetFaultMessage().First().Value;
                throw new McpException(message);
            }
            else
            {
                var res = JsonSerializer.Deserialize<Dictionary<string, ZimbraGetMsgResponse>>(resp.GetResponse(), MailModelContext.Default.DictionaryStringZimbraGetMsgResponse);
                return res.First().Value;
            }
        }

        public async Task<ZimbraGetInfoResponse> GetSystemInfo()
        {
            JsonRequest request = _zimbra.GenRequest("json", userToken) as JsonRequest;
            request.AddRequest("GetInfoRequest", $$"""
                   {}
                """,
                "urn:zimbraAccount");
            var resp = await _zimbra.SendRequest(request);

            if (resp.IsFault())
            {
                var message = resp.GetFaultMessage().First().Value;
                throw new McpException(message);
            }
            else
            {
                var res = JsonSerializer.Deserialize<Dictionary<string, ZimbraGetInfoResponse>>(resp.GetResponse(), MailModelContext.Default.DictionaryStringZimbraGetInfoResponse);
                return res.First().Value;
            }
        }

        public async Task<ZimbraSendMsgResponse> SendMail(List<ZimbraMailParticipant> participants, string title, string content)
        {
            JsonRequest request = _zimbra.GenRequest("json", userToken) as JsonRequest;
            request.AddRequest("SendMsgRequest", $$"""
                  {
                  "m": {
                    "e": {{JsonSerializer.Serialize(participants, MailModelContext.Default.ListZimbraMailParticipant)}},
                    "su": {
                      "_content": {{JsonSerializer.Serialize(title, MailModelContext.Default.String)}}
                    },
                    "mp": [
                      {
                        "ct": "text/plain",
                        "content": {
                          "_content": {{JsonSerializer.Serialize(content, MailModelContext.Default.String)}}
                        }
                      }
                    ]
                  }
                }
                """,
                "urn:zimbraMail");
            var resp = await _zimbra.SendRequest(request);

            if (resp.IsFault())
            {
                var message = resp.GetFaultMessage().First().Value;
                throw new McpException(message);
            }
            else
            {
                var res = JsonSerializer.Deserialize<Dictionary<string, ZimbraSendMsgResponse>>(resp.GetResponse(), MailModelContext.Default.DictionaryStringZimbraSendMsgResponse);
                return res.First().Value;
            }
        }

    }
}
