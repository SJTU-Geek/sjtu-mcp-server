using ModelContextProtocol.Protocol;
using ModelContextProtocol.Server;
using SJTUGeek.MCP.Server.Modules;
using System.ComponentModel;

namespace SJTUGeek.MCP.Server.Tools.SjtuJw;

[McpServerToolType]
public class SjtuJwTool
{
    private readonly ILogger<SjtuJwTool> _logger;
    private readonly SjtuJwService _jw;

    public SjtuJwTool(ILogger<SjtuJwTool> logger, SjtuJwService jw)
    {
        _logger = logger;
        _jw = jw;
    }

    [McpServerTool(Name = "personal_course_table"), Description("Get class schedules for a given semester.")]
    public async Task<object> ToolPersonalCourseTable(
        [Description("The specified semester, defaults to the current semester if left blank.")]
        string? semester = null
    )
    {
        await _jw.Login();
        var courses = await _jw.GetPersonalCourseTable(semester);
        if (courses.KbList.Length == 0)
            return new CallToolResponse() { IsError = false, Content = new List<Content>() { new Content() { Text = "指定的学期没有课程！" } } };
        var res = RenderPersonalCourseTable(courses);
        return res;
    }

    [McpServerTool(Name = "personal_course_score"), Description("Get course scores for a given semester.")]
    public async Task<object> ToolCourseScoreList(
        [Description("The specified semester, defaults to the current semester if left blank.")]
        string? semester = null
    )
    {
        await _jw.Login();
        var scores = await _jw.GetCourseScoreList(semester);
        if (scores.Items.Length == 0)
            return new CallToolResponse() { IsError = false, Content = new List<Content>() { new Content() { Text = "找不到数据，可能是成绩还没有出哦~" } } };
        var res = RenderCourseScoreList(scores);
        return res;
    }

    [McpServerTool(Name = "personal_gpa_and_ranking"), Description("Get personal GPA and rankings.")]
    public async Task<object> ToolGpaAndRanking(
        [Description("Starting semester of statistics, defaults to the current semester if left blank.")]
        string? start_semester = null,
        [Description("Ending semester of statistics, defaults to the current semester if left blank.")]
        string? end_semester = null,
        [Description("Types of courses included, 'core' or 'all'.")]
        string? type = "core"
    )
    {
        await _jw.Login();
        var tj = await _jw.RequestGpaTj(start_semester, end_semester, type);
        if (!tj.StartsWith("统计成功"))
            return new CallToolResponse() { IsError = true, Content = new List<Content>() { new Content() { Text = "统计失败！" } } };
        var stat = await _jw.GetGpaTjResult();
        if (stat.Items.Length == 0)
            return new CallToolResponse() { IsError = true, Content = new List<Content>() { new Content() { Text = "找不到数据，请检查统计范围！" } } };
        var res = RenderGrades(stat.Items[0]);
        return res;
    }

    [McpServerTool(Name = "personal_exam_info"), Description("Obtain exam time and location information.")]
    public async Task<object> ToolExamInfo(
    [Description("The specified semester, defaults to the current semester if left blank.")]
        string? semester = null
    )
    {
        await _jw.Login();
        var exams = await _jw.GetExamInfo(semester);
        if (exams.Items.Length == 0)
            return new CallToolResponse() { IsError = false, Content = new List<Content>() { new Content() { Text = "找不到数据，可能是考试安排还没有出哦~" } } };
        var res = RenderExamInfoList(exams);
        return res;
    }

    public string RenderPersonalCourse(KbList c)
    {
        var res =
        $"- {c.Kcmc}（{c.Kch}）" + "\n" +
        $"  周数：{c.Zcd}" + "\n" +
        $"  校区：{c.Xqmc}" + "\n" +
        $"  上课时间：{c.Xqjmc} {c.Jc}" + "\n" +
        $"  上课地点：{c.Cdmc}" + "\n" +
        $"  教师：{c.Xm}" + "\n" +
        $"  教学班：{c.Jxbmc}" + "\n" +
        $"  选课备注：{(c.Xkbz.Trim() == "" ? "无" : c.Xkbz.Trim())}" + "\n" +
        $"  学分：{c.Xf}" + "\n" +
        $"  课程标记：{c.Kcbj}" + "\n" +
        $"  是否专业核心课程：{c.Zyhxkcbj}" + "\n" +
        "";
        return res;
    }

    public string RenderPersonalCourseTable(JwPersonalCourseList list)
    {
        return string.Join('\n', list.KbList.Select(x => RenderPersonalCourse(x)));
    }

    public string RenderSingleCourseScore(List<JwCourseScoreItem> c)
    {
        var res =
        $"- {c[0].Kcmc}（课程号：{c[0].Kch}；学分：{c[0].Xf}）" + "\n" +
        string.Join('\n', c.Select(x => $"  - {x.Xmblmc}：{x.Xmcj}"));
        return res;
    }

    public string RenderCourseScoreList(JwCourseScoreList list)
    {
        var groups = list.Items.GroupBy(x => x.Kch, y => y);
        return string.Join('\n', groups.Select(x => RenderSingleCourseScore(x.ToList())));
    }

    public string RenderGrades(JwGpaStatistic stat)
    {
        var res =
        $"总分：{stat.Zf}" + "\n" +
        $"门数：{stat.Ms}" + "\n" +
        $"不及格门数：{stat.Bjgms}" + "\n" +
        $"总学分：{stat.Zxf}" + "\n" +
        $"获得学分：{stat.Hdxf}" + "\n" +
        $"不及格学分：{stat.Bjgxf}" + "\n" +
        $"通过率：{stat.Tgl}" + "\n" +
        $"学积分：{stat.Xjf}" + "\n" +
        $"学积分排名：{stat.Xjfpm}" + "\n" +
        $"绩点(gpa)：{stat.Gpa}" + "\n" +
        $"绩点排名：{stat.Gpapm}" + "\n" +
        $"全部课程不及格门次：{stat.Bjgmc}" + "\n" +
        "";
        return res;
    }

    public string RenderSingleExamInfo(JwExamInfoItem item)
    {
        var res =
        $"- {item.Kcmc}（课程号：{item.Kch}）" + "\n" +
        $"  考试时间：{item.Kssj}" + "\n" +
        $"  考试地点：{item.Cdmc}" + "\n" +
        $"  考试方式：{item.Ksfs}";
        return res;
    }

    public string RenderExamInfoList(JwExamInfoResult list)
    {
        return string.Join('\n', list.Items.Select(x => RenderSingleExamInfo(x)));
    }
}

