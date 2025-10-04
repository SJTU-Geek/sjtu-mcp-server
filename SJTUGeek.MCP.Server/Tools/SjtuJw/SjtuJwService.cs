using ModelContextProtocol;
using SJTUGeek.MCP.Server.Modules;
using System.Net;

namespace SJTUGeek.MCP.Server.Tools.SjtuJw
{
    public class SjtuJwService
    {
        private readonly HttpClient _client;

        public SjtuJwService(HttpClientFactory clientFactory)
        {
            _client = clientFactory.CreateClient();
        }

        public async Task<bool> Login()
        {
            var res = await _client.GetAsync("https://i.sjtu.edu.cn/jaccountlogin");
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
            if (!res.RequestMessage.RequestUri.OriginalString.StartsWith("https://i.sjtu.edu.cn/"))
            {
                throw new McpException("认证失败");
            }
            return true;
        }

        public async Task<JwPersonalCourseList> GetPersonalCourseTable(string? semester = null)
        {
            var xnxq = SjtuJwHelper.GetCurrentXnXq();
            if (semester != null)
            {
                xnxq = SjtuJwHelper.ParseXnXq(semester);
            }

            var forms = new Dictionary<string, string>();
            forms.Add("xnm", xnxq.xn);
            forms.Add("xqm", xnxq.xq);
            forms.Add("kzlx", "ck");
            forms.Add("xsdm", "");

            var req = new HttpRequestMessage(HttpMethod.Post, "https://i.sjtu.edu.cn/kbcx/xskbcx_cxXsgrkb.html?gnmkdm=N2151");
            req.Content = new FormUrlEncodedContent(forms);

            var res = await _client.SendAsync(req);
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            var json = await res.Content.ReadFromJsonAsync<JwPersonalCourseList>(JwModelContext.Default.JwPersonalCourseList);
            return json;
        }

        public async Task<JwCourseScoreList> GetCourseScoreList(string? semester = null)
        {
            var xnxq = SjtuJwHelper.GetCurrentXnXq();
            if (semester != null)
            {
                xnxq = SjtuJwHelper.ParseXnXq(semester);
            }

            var forms = new Dictionary<string, string>();
            forms.Add("xnm", xnxq.xn);
            forms.Add("xqm", xnxq.xq);
            forms.Add("_search", "false");
            forms.Add("queryModel.showCount", "200");
            forms.Add("queryModel.currentPage", "1");
            forms.Add("queryModel.sortName", "");
            forms.Add("queryModel.sortOrder", "asc");
            forms.Add("time", "3");

            var req = new HttpRequestMessage(HttpMethod.Post, "https://i.sjtu.edu.cn/cjcx/cjcx_cxXsKccjList.html?gnmkdm=N305007");
            req.Content = new FormUrlEncodedContent(forms);

            var res = await _client.SendAsync(req);
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            var json = await res.Content.ReadFromJsonAsync<JwCourseScoreList>(JwModelContext.Default.JwCourseScoreList);
            return json;
        }

        public async Task<string> RequestGpaTj(
            string? start_semester = null,
            string? end_semester = null,
            string? type = "core"
        )
        {
            var xnxq1 = SjtuJwHelper.GetCurrentXnXq();
            if (start_semester != null)
            {
                xnxq1 = SjtuJwHelper.ParseXnXq(start_semester);
            }
            var xnxq2 = SjtuJwHelper.GetCurrentXnXq();
            if (end_semester != null)
            {
                xnxq2 = SjtuJwHelper.ParseXnXq(end_semester);
            }

            var forms = new Dictionary<string, string>();
            forms.Add("qsXnxq", $"{xnxq1.xn}{xnxq1.xq}");
            forms.Add("zzXnxq", $"{xnxq2.xn}{xnxq2.xq}");
            forms.Add("tjgx", "0");
            forms.Add("alsfj", "");
            forms.Add("sspjfblws", "9");
            forms.Add("pjjdblws", "9");
            forms.Add("bjpjf", "缓考,缓考(重考),尚未修读,暂不记录,中期退课,重考报名");
            forms.Add("bjjd", "缓考,缓考(重考),尚未修读,暂不记录,中期退课,重考报名");
            forms.Add("kch_ids", "MARX1205,TH009,TH020,FCE62B4E084826EBE055F8163EE1DCCC");
            forms.Add("bcjkc_id", "");
            forms.Add("bcjkz_id", "");
            forms.Add("cjkz_id", "");
            forms.Add("cjxzm", "zhyccj");
            forms.Add("kcfw", type == "core" ? "hxkc" : "qbkc");
            forms.Add("tjfw", "njzy");
            forms.Add("xjzt", "1");

            var req = new HttpRequestMessage(HttpMethod.Post, "https://i.sjtu.edu.cn/cjpmtj/gpapmtj_tjGpapmtj.html?gnmkdm=N309131");
            req.Content = new FormUrlEncodedContent(forms);

            var res = await _client.SendAsync(req);
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            var text = await res.Content.ReadAsStringAsync();
            return text.Trim('"');
        }

        public async Task<JwGpaQueryResult> GetGpaTjResult()
        {
            var forms = new Dictionary<string, string>();
            forms.Add("tjfw", "njzy");
            forms.Add("queryModel.showCount", "200");
            forms.Add("queryModel.currentPage", "1");
            forms.Add("queryModel.sortName", "");
            forms.Add("queryModel.sortOrder", "asc");

            var req = new HttpRequestMessage(HttpMethod.Post, "https://i.sjtu.edu.cn/cjpmtj/gpapmtj_cxGpaxjfcxIndex.html?doType=query&gnmkdm=N309131");
            req.Content = new FormUrlEncodedContent(forms);

            var res = await _client.SendAsync(req);
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            var json = await res.Content.ReadFromJsonAsync<JwGpaQueryResult>(JwModelContext.Default.JwGpaQueryResult);
            return json;
        }

        public async Task<JwExamInfoResult> GetExamInfo(string? semester = null)
        {
            var xnxq = SjtuJwHelper.GetCurrentXnXq();
            if (semester != null)
            {
                xnxq = SjtuJwHelper.ParseXnXq(semester);
            }

            var forms = new Dictionary<string, string>();
            forms.Add("xnm", xnxq.xn);
            forms.Add("xqm", xnxq.xq);
            forms.Add("ksmcdmb_id", "");
            forms.Add("kch", "");
            forms.Add("kc", "");
            forms.Add("ksrq", "");
            forms.Add("kkbm_id", "");
            forms.Add("_search", "false");
            forms.Add("time", "5");
            forms.Add("queryModel.showCount", "200");
            forms.Add("queryModel.currentPage", "1");
            forms.Add("queryModel.sortName", "");
            forms.Add("queryModel.sortOrder", "asc");

            var req = new HttpRequestMessage(HttpMethod.Post, "https://i.sjtu.edu.cn/kwgl/kscx_cxXsksxxIndex.html?doType=query&gnmkdm=N358105");
            req.Content = new FormUrlEncodedContent(forms);

            var res = await _client.SendAsync(req);
            try
            {
                res.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw new McpException(e.Message);
            }
            var json = await res.Content.ReadFromJsonAsync<JwExamInfoResult>(JwModelContext.Default.JwExamInfoResult);
            return json;
        }
    }
}
