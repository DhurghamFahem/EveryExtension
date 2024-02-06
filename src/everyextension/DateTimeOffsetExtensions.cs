namespace EveryExtension;

public static class DateTimeOffsetExtensions
{
    public static bool IsLeapYear(this DateTimeOffset date)
    {
        var year = date.Year;
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    public static long ToUnixTimestamp(this DateTimeOffset date)
    {
        return (long)date.Subtract(new DateTimeOffset(new(1970, 1, 1))).TotalSeconds;
    }

    public static int Age(this DateTimeOffset birthDate)
    {
        var today = DateTimeOffset.UtcNow;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    public static DateTimeOffset ToDaysEnd(this DateTimeOffset dt)
        => dt.AddDays(1).AddTicks(-1);

    public static DateTimeOffset ToDaysStart(this DateTimeOffset dt)
       => new(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, dt.Offset);

    public static bool IsWithinRange(this DateTimeOffset date, DateTimeOffset startDate, DateTimeOffset endDate)
        => date >= startDate && date <= endDate;

    public static bool IsToday(this DateTimeOffset date)
        => date.Date == DateTimeOffset.UtcNow.Date;

    public static DateTimeOffset AddWeeks(this DateTimeOffset date, int weeks)
        => date.AddDays(weeks * 7);

    public static DateTimeOffset SubtractWeeks(this DateTimeOffset date, int weeks)
        => date.AddDays(-weeks * 7);

    public static bool IsFuture(this DateTimeOffset date)
        => date > DateTimeOffset.Now;

    public static string ToShortMonthString(this DateTimeOffset date)
        => date.ToString("MMM");

    public static bool IsSameDay(this DateTimeOffset date1, DateTimeOffset date2)
        => date1.Date == date2.Date;

    public static DateTimeOffset StartOfMonth(this DateTimeOffset date)
        => new(new(date.Year, date.Month, 1));

    public static DateTimeOffset EndOfMonth(this DateTimeOffset date)
    {
        var endOfMonth = new DateTimeOffset(new(date.Year, date.Month, date.DaysInMonth()));
        return endOfMonth.ToDaysEnd();
    }

    public static DateTimeOffset StartOfYear(this DateTimeOffset date)
       => new(new(date.Year, 1, 1));

    public static DateTimeOffset EndOfYear(this DateTimeOffset date)
    {
        var endOfYear = new DateTimeOffset(new(date.Year, 12, date.DaysInMonth()));
        return endOfYear.ToDaysEnd();
    }

    public static DateTimeOffset RoundToNearestMinute(this DateTimeOffset date)
        => new(new(date.Ticks - (date.Ticks % TimeSpan.TicksPerMinute), date.DateTime.Kind));

    public static string ElapsedTimeString(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        if (elapsed.TotalMinutes < 1)
            return "Just now";
        var elapsedChar = (int)elapsed.TotalMinutes != 1 ? "s" : "";
        if (elapsed.TotalHours < 1)
            return $"{(int)elapsed.TotalMinutes} minute{elapsedChar} ago";
        if (elapsed.TotalDays < 1)
            return $"{(int)elapsed.TotalHours} hour{elapsedChar} ago";
        return $"{(int)elapsed.TotalDays} day{elapsedChar} ago";
    }

    public static double ElapsedMinutes(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalMinutes;
    }

    public static double ElapsedHours(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalHours;
    }

    public static double ElapsedDays(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalDays;
    }

    public static bool IsMorning(this DateTimeOffset time)
        => time.Hour >= 0 && time.Hour < 12;

    public static bool IsAfternoon(this DateTimeOffset time)
        => time.Hour >= 12 && time.Hour < 18;

    public static bool IsEvening(this DateTimeOffset time)
        => time.Hour >= 18 && time.Hour < 24;

    public static int DaysInMonth(this DateTimeOffset date)
        => DateTime.DaysInMonth(date.Year, date.Month);

    public static bool IsLeapDay(this DateTimeOffset date)
        => date.Month == 2 && date.Day == 29;

    public static bool IsYesterday(this DateTimeOffset date)
        => date.Date == DateTimeOffset.UtcNow.AddDays(-1);

    public static bool IsSameYear(this DateTimeOffset date1, DateTimeOffset date2)
        => date1.Year == date2.Year;

    public static bool? IsNull(this DateTimeOffset? value)
        => value == null;

    public static bool? IsNotNull(this DateTimeOffset? value)
        => value != null;

    public static DateTimeOffset? AsNullable(this DateTimeOffset value)
        => value;
}
