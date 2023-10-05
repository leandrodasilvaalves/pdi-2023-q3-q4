namespace Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime SetMillisecond(this DateTime self, int milesecond = 0)
        {
            return new DateTime(self.Year, self.Month, self.Day, self.Hour, self.Minute, self.Second, milesecond);
        }
    }
}