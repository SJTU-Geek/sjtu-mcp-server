namespace SJTUGeek.MCP.Server.Tools.SjtuVenue
{
    public static class SjtuVenueHelper
    {
        public const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEArKZOdKQAL+iYzJ4Q5EQzwv/yvVPnfdNVKRgNG19HbCYM4qIzFPEOFv28SVFQh+xqAj8tAfjpMSTihFwt6BQuWfZXWYpAqf4jF4cU7ez/VHJyzsn8Cb7Lf/1KsLpuz+MbqufrA57AysnLAnRXHOwik+QnpsXZYjTcjgxQ0iLMe5iJyo06CKFxH1rmgYMwS4E89kNg1VtYrFKs1MajApfhu9hTEXnm/lP24TPdefRXbf+z84p1GLue2HRhZs3wECH1HJWZOsrdL/M+wigWldY0fHoiaKsjD9rK1NyaPtk4bIYuwPsfQu5RN4hkEPpTvdw1nKzOdo77zNa5ovCY0uNLZwIDAQAB";

        public const string AESKey = "0123456789abcdef";

        public static TimeOnly[] GetTimeIntervals(TimeOnly start, TimeOnly end, TimeSpan interval)
        {
            if (start > end)
            {
                throw new ArgumentException("开始时间不能晚于结束时间");
            }

            if (interval <= TimeSpan.Zero)
            {
                throw new ArgumentException("间隔必须为正数");
            }

            // 计算需要多少个间隔点
            double totalMinutes = (end - start).TotalMinutes;
            double intervalMinutes = interval.TotalMinutes;
            int count = (int)(totalMinutes / intervalMinutes);

            // 创建数组并填充
            TimeOnly[] result = new TimeOnly[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = start.AddMinutes(i * intervalMinutes);
            }

            return result;
        }
    }
}
