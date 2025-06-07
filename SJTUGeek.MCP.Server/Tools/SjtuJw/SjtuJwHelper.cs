using System.Text.RegularExpressions;

namespace SJTUGeek.MCP.Server.Tools.SjtuJw
{
    public static class SjtuJwHelper
    {
        public static (string xn, string xq) GetCurrentXnXq()
        {
            DateTime now = DateTime.Now;
            int mm = now.Month;
            int yy = now.Year;
            string xq, xn;

            if (mm >= 9 || mm <= 2)
            {
                xq = "3"; // 第一学期
                xn = mm >= 9 ? yy.ToString() : (yy - 1).ToString();
            }
            else if (mm >= 3 && mm <= 7)
            {
                xq = "12"; // 第二学期
                xn = (yy - 1).ToString();
            }
            else
            {
                xq = "16"; // 第三学期
                xn = (yy - 1).ToString();
            }

            return (xn, xq);
        }

        public static (string xn, string xq) ParseXnXq(string semester)
        {
            var regex = new Regex(@"\d+");
            var matches = regex.Matches(semester);
            var integers = matches.Cast<Match>().Select(m => m.Value).ToList();

            if (integers.Count == 0)
                throw new Exception("学期学年格式错误！请使用类似「2024-2025学年第一学期」的格式");

            if (!int.TryParse(integers[0], out int xn) || xn < 2000)
                throw new Exception("学期学年格式错误！请使用类似「2024-2025学年第一学期」的格式");

            bool parseChinese = false;
            int xq = 0;

            if (integers.Count >= 2)
            {
                if (!int.TryParse(integers[1], out int t))
                    throw new Exception("学期学年格式错误！请使用类似「2024-2025学年第一学期」的格式");

                if (t > 2000)
                {
                    if (integers.Count >= 3)
                    {
                        if (!int.TryParse(integers[2], out xq))
                            throw new Exception("学期学年格式错误！请使用类似「2024-2025学年第一学期」的格式");
                    }
                    else
                    {
                        parseChinese = true;
                    }
                }
                else
                {
                    xq = t;
                }
            }
            else
            {
                parseChinese = true;
            }

            if (parseChinese)
            {
                if (semester.Contains("一")) xq = 1;
                else if (semester.Contains("二")) xq = 2;
                else if (semester.Contains("三")) xq = 3;
            }

            if (xq < 1 || xq > 3)
                throw new Exception("学期学年格式错误！请使用类似「2024-2025学年第一学期」的格式");

            int[] xqMap = { 3, 12, 16 };
            return (xn.ToString(), xqMap[xq - 1].ToString());
        }
    }
}
