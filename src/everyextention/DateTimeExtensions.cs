namespace EveryExtention;

public static class DateTimeExtensions
{
    public static bool IsLeapYear(this DateTime date)
    {
        var year = date.Year;
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public static long ToUnixTimestamp(this DateTime date)
    {
        return (long)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    public static int Age(this DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    public static DateTime ToDaysEnd(this DateTime dt)
        => dt.AddDays(1).AddTicks(-1);

    public static DateTime ToDaysStart(this DateTime dt)
       => new(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);

    public static bool IsWithinRange(this DateTime date, DateTime startDate, DateTime endDate)
        => date >= startDate && date <= endDate;

    public static bool IsToday(this DateTime date)
        => date.Date == DateTime.Today;

    public static DateTime AddWeeks(this DateTime date, int weeks)
        => date.AddDays(weeks * 7);

    public static DateTime SubtractWeeks(this DateTime date, int weeks)
        => date.AddDays(-weeks * 7);

    public static bool IsFuture(this DateTime date)
        => date > DateTime.Now;

    public static string ToShortMonthString(this DateTime date)
        => date.ToString("MMM");

    public static bool IsSameDay(this DateTime date1, DateTime date2)
        => date1.Date == date2.Date;

    public static DateTime StartOfMonth(this DateTime date)
        => new(date.Year, date.Month, 1);

    public static DateTime EndOfMonth(this DateTime date)
    {
        var endOfMonth = new DateTime(date.Year, date.Month, date.DaysInMonth());
        return endOfMonth.ToDaysEnd();
    }

    public static DateTime StartOfYear(this DateTime date)
       => new(date.Year, 1, 1);

    public static DateTime EndOfYear(this DateTime date)
    {
        var endOfYear = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12));
        return endOfYear.ToDaysEnd();
    }

    public static DateTime RoundToNearestMinute(this DateTime date)
        => new(date.Ticks - (date.Ticks % TimeSpan.TicksPerMinute), date.Kind);

    public static string ElapsedTimeString(this DateTime date)
    {
        var elapsed = DateTime.Now - date;
        if (elapsed.TotalMinutes < 1)
            return "Just now";
        var elapsedChar = (int)elapsed.TotalMinutes != 1 ? "s" : "";
        if (elapsed.TotalHours < 1)
            return $"{(int)elapsed.TotalMinutes} minute{elapsedChar} ago";
        if (elapsed.TotalDays < 1)
            return $"{(int)elapsed.TotalHours} hour{elapsedChar} ago";
        return $"{(int)elapsed.TotalDays} day{elapsedChar} ago";
    }

    public static bool IsMorning(this DateTime time)
        => time.Hour >= 0 && time.Hour < 12;

    public static bool IsAfternoon(this DateTime time)
        => time.Hour >= 12 && time.Hour < 18;

    public static bool IsEvening(this DateTime time)
        => time.Hour >= 18 && time.Hour < 24;

    public static int DaysInMonth(this DateTime date)
        => DateTime.DaysInMonth(date.Year, date.Month);

    public static bool IsLeapDay(this DateTime date)
        => date.Month == 2 && date.Day == 29;

    public static bool IsYesterday(this DateTime date)
        => date.Date == DateTime.Today.AddDays(-1);

    public static bool IsSameYear(this DateTime date1, DateTime date2)
        => date1.Year == date2.Year;

    public static bool? IsNull(this DateTime? value)
        => value == null;

    public static bool? IsNotNull(this DateTime? value)
        => value != null;

    public static DateTime? AsNullable(this DateTime value)
        => value;
}