namespace EveryExtension;

/// <summary>
/// Extension methods for DateTimeOffset type, providing various date and time-related functionalities.
/// </summary>
public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Checks if the year of the date is a leap year.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <returns>True if the year is a leap year; otherwise, false.</returns>
    public static bool IsLeapYear(this DateTimeOffset date)
    {
        var year = date.Year;
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    /// <summary>
    /// Converts the DateTimeOffset to a Unix timestamp (seconds since 1970-01-01).
    /// </summary>
    /// <param name="date">The DateTimeOffset object to convert.</param>
    /// <returns>The Unix timestamp representation of the DateTimeOffset.</returns>
    public static long ToUnixTimestamp(this DateTimeOffset date)
    {
        return (long)(date.Subtract(new DateTimeOffset(new(1970, 1, 1)))).TotalSeconds;
    }

    /// <summary>
    /// Calculates the age based on the birthdate.
    /// </summary>
    /// <param name="birthDate">The birthdate to calculate the age from.</param>
    /// <returns>The age as an integer.</returns>
    public static int Age(this DateTimeOffset birthDate)
    {
        var today = DateTimeOffset.UtcNow;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    /// <summary>
    /// Converts the DateTimeOffset to the end of the day.
    /// </summary>
    /// <param name="dt">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the end of the day.</returns>
    public static DateTimeOffset ToDaysEnd(this DateTimeOffset dt)
        => dt.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Converts the DateTimeOffset to the start of the day.
    /// </summary>
    /// <param name="dt">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the start of the day.</returns>
    public static DateTimeOffset ToDaysStart(this DateTimeOffset dt)
        => new(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, dt.Offset);

    /// <summary>
    /// Checks if the DateTimeOffset is within a specified date range.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>True if the DateTimeOffset is within the specified range; otherwise, false.</returns>
    public static bool IsWithinRange(this DateTimeOffset date, DateTimeOffset startDate, DateTimeOffset endDate)
        => date >= startDate && date <= endDate;

    /// <summary>
    /// Checks if the DateTimeOffset is today.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <returns>True if the DateTimeOffset is today; otherwise, false.</returns>
    public static bool IsToday(this DateTimeOffset date)
        => date.Date == DateTimeOffset.UtcNow.Date;

    /// <summary>
    /// Adds a specified number of weeks to the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to add weeks to.</param>
    /// <param name="weeks">The number of weeks to add.</param>
    /// <returns>The DateTimeOffset with the added weeks.</returns>
    public static DateTimeOffset AddWeeks(this DateTimeOffset date, int weeks)
        => date.AddDays(weeks * 7);

    /// <summary>
    /// Subtracts a specified number of weeks from the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to subtract weeks from.</param>
    /// <param name="weeks">The number of weeks to subtract.</param>
    /// <returns>The DateTimeOffset with the subtracted weeks.</returns>
    public static DateTimeOffset SubtractWeeks(this DateTimeOffset date, int weeks)
        => date.AddDays(-weeks * 7);

    /// <summary>
    /// Checks if the DateTimeOffset is in the future.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <returns>True if the DateTimeOffset is in the future; otherwise, false.</returns>
    public static bool IsFuture(this DateTimeOffset date)
        => date > DateTimeOffset.Now;

    /// <summary>
    /// Converts the DateTimeOffset to a short month string (e.g., "Jan").
    /// </summary>
    /// <param name="date">The DateTimeOffset object to convert.</param>
    /// <returns>The short month string.</returns>
    public static string ToShortMonthString(this DateTimeOffset date)
        => date.ToString("MMM");

    /// <summary>
    /// Checks if two DateTimeOffset objects represent the same day.
    /// </summary>
    /// <param name="date1">The first DateTimeOffset object.</param>
    /// <param name="date2">The second DateTimeOffset object.</param>
    /// <returns>True if the DateTimeOffset objects represent the same day; otherwise, false.</returns>
    public static bool IsSameDay(this DateTimeOffset date1, DateTimeOffset date2)
        => date1.Date == date2.Date;

    /// <summary>
    /// Gets the start of the month for a given DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the start of the month.</returns>
    public static DateTimeOffset StartOfMonth(this DateTimeOffset date)
        => new(new(date.Year, date.Month, 1));

    /// <summary>
    /// Gets the end of the month for a given DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the end of the month.</returns>
    public static DateTimeOffset EndOfMonth(this DateTimeOffset date)
    {
        var endOfMonth = new DateTimeOffset(new(date.Year, date.Month, date.DaysInMonth()));
        return endOfMonth.ToDaysEnd();
    }

    /// <summary>
    /// Gets the start of the year for a given DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the start of the year.</returns>
    public static DateTimeOffset StartOfYear(this DateTimeOffset date)
       => new(new(date.Year, 1, 1));

    /// <summary>
    /// Gets the end of the year for a given DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object.</param>
    /// <returns>The DateTimeOffset set to the end of the year.</returns>
    public static DateTimeOffset EndOfYear(this DateTimeOffset date)
    {
        var endOfYear = new DateTimeOffset(new(date.Year, 12, date.DaysInMonth()));
        return endOfYear.ToDaysEnd();
    }

    /// <summary>
    /// Rounds the DateTimeOffset to the nearest minute.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to round.</param>
    /// <returns>The DateTimeOffset rounded to the nearest minute.</returns>
    public static DateTimeOffset RoundToNearestMinute(this DateTimeOffset date)
        => new(new(date.Ticks - (date.Ticks % TimeSpan.TicksPerMinute), date.DateTime.Kind));

    /// <summary>
    /// Gets a human-readable elapsed time string from the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to calculate the elapsed time from.</param>
    /// <returns>The elapsed time string.</returns>
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

    /// <summary>
    /// Gets the elapsed minutes from the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to calculate the elapsed minutes from.</param>
    /// <returns>The elapsed minutes.</returns>
    public static double ElapsedMinutes(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalMinutes;
    }

    /// <summary>
    /// Gets the elapsed hours from the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to calculate the elapsed hours from.</param>
    /// <returns>The elapsed hours.</returns>
    public static double ElapsedHours(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalHours;
    }

    /// <summary>
    /// Gets the elapsed days from the DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to calculate the elapsed days from.</param>
    /// <returns>The elapsed days.</returns>
    public static double ElapsedDays(this DateTimeOffset date)
    {
        var elapsed = DateTimeOffset.Now - date;
        return elapsed.TotalDays;
    }

    /// <summary>
    /// Checks if the time is in the morning.
    /// </summary>
    /// <param name="time">The DateTimeOffset object representing a time.</param>
    /// <returns>True if the time is in the morning; otherwise, false.</returns>
    public static bool IsMorning(this DateTimeOffset time)
        => time.Hour >= 0 && time.Hour < 12;

    /// <summary>
    /// Checks if the time is in the afternoon.
    /// </summary>
    /// <param name="time">The DateTimeOffset object representing a time.</param>
    /// <returns>True if the time is in the afternoon; otherwise, false.</returns>
    public static bool IsAfternoon(this DateTimeOffset time)
        => time.Hour >= 12 && time.Hour < 18;

    /// <summary>
    /// Checks if the time is in the evening.
    /// </summary>
    /// <param name="time">The DateTimeOffset object representing a time.</param>
    /// <returns>True if the time is in the evening; otherwise, false.</returns>
    public static bool IsEvening(this DateTimeOffset time)
        => time.Hour >= 18 && time.Hour < 24;

    /// <summary>
    /// Gets the number of days in the month for a given DateTimeOffset.
    /// </summary>
    /// <param name="date">The DateTimeOffset object.</param>
    /// <returns>The number of days in the month.</returns>
    public static int DaysInMonth(this DateTimeOffset date)
        => DateTime.DaysInMonth(date.Year, date.Month);

    /// <summary>
    /// Checks if the DateTimeOffset represents a leap day (February 29th).
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <returns>True if the DateTimeOffset represents a leap day; otherwise, false.</returns>
    public static bool IsLeapDay(this DateTimeOffset date)
        => date.Month == 2 && date.Day == 29;

    /// <summary>
    /// Checks if the DateTimeOffset is yesterday.
    /// </summary>
    /// <param name="date">The DateTimeOffset object to check.</param>
    /// <returns>True if the DateTimeOffset is yesterday; otherwise, false.</returns>
    public static bool IsYesterday(this DateTimeOffset date)
        => date.Date == DateTimeOffset.UtcNow.AddDays(-1);

    /// <summary>
    /// Checks if two DateTimeOffset objects represent the same year.
    /// </summary>
    /// <param name="date1">The first DateTimeOffset object.</param>
    /// <param name="date2">The second DateTimeOffset object.</param>
    /// <returns>True if the DateTimeOffset objects represent the same year; otherwise, false.</returns>
    public static bool IsSameYear(this DateTimeOffset date1, DateTimeOffset date2)
        => date1.Year == date2.Year;
}
