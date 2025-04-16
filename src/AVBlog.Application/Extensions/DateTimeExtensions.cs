namespace AVBlog.Application.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateOnly? ToDateOnly(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return null;
            }
            var dt = dateTime.Value;
            return new DateOnly(dt.Year, dt.Month, dt.Day);
        }

        public static DateTime? ToDateTime(this DateOnly? dateOnly)
        {
            if (dateOnly == null)
            {
                return null;
            }
            var dt = dateOnly.Value;
            return new DateTime(dt.Year, dt.Month, dt.Day);
        }
    }
}
