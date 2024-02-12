namespace EveryExtension;

/// <summary>
/// Extension methods for DateTime type, providing various date and time-related functionalities.
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Checks if the year of the date is a leap year.
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <returns>True if the year is a leap year; otherwise, false.</returns>
    public static bool IsLeapYear(this DateTime date)
    {
        var year = date.Year;
        return (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
    }

    /// <summary>
    /// Converts the DateTime to a Unix timestamp (seconds since 1970-01-01).
    /// </summary>
    /// <param name="date">The DateTime object to convert.</param>
    /// <returns>The Unix timestamp representation of the DateTime.</returns>
    public static long ToUnixTimestamp(this DateTime date)
    {
        return (long)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    /// <summary>
    /// Calculates the age based on the birthdate.
    /// </summary>
    /// <param name="birthDate">The birthdate to calculate the age from.</param>
    /// <returns>The age as an integer.</returns>
    public static int Age(this DateTime birthDate)
    {
        var today = DateTime.Today;
        int age = today.Year - birthDate.Year;

        if (birthDate.Date > today.AddYears(-age))
            age--;

        return age;
    }

    /// <summary>
    /// Converts the DateTime to the end of the day.
    /// </summary>
    /// <param name="dt">The DateTime object.</param>
    /// <returns>The DateTime set to the end of the day.</returns>
    public static DateTime ToDaysEnd(this DateTime dt)
        => dt.AddDays(1).AddTicks(-1);

    /// <summary>
    /// Converts the DateTime to the start of the day.
    /// </summary>
    /// <param name="dt">The DateTime object.</param>
    /// <returns>The DateTime set to the start of the day.</returns>
    public static DateTime ToDaysStart(this DateTime dt)
        => new(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0);

    /// <summary>
    /// Checks if the DateTime is within a specified date range.
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <param name="startDate">The start date of the range.</param>
    /// <param name="endDate">The end date of the range.</param>
    /// <returns>True if the DateTime is within the specified range; otherwise, false.</returns>
    public static bool IsWithinRange(this DateTime date, DateTime startDate, DateTime endDate)
        => date >= startDate && date <= endDate;

    /// <summary>
    /// Checks if the DateTime is today.
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <returns>True if the DateTime is today; otherwise, false.</returns>
    public static bool IsToday(this DateTime date)
        => date.Date == DateTime.Today;

    /// <summary>
    /// Adds a specified number of weeks to the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to add weeks to.</param>
    /// <param name="weeks">The number of weeks to add.</param>
    /// <returns>The DateTime with the added weeks.</returns>
    public static DateTime AddWeeks(this DateTime date, int weeks)
        => date.AddDays(weeks * 7);

    /// <summary>
    /// Subtracts a specified number of weeks from the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to subtract weeks from.</param>
    /// <param name="weeks">The number of weeks to subtract.</param>
    /// <returns>The DateTime with the subtracted weeks.</returns>
    public static DateTime SubtractWeeks(this DateTime date, int weeks)
        => date.AddDays(-weeks * 7);

    /// <summary>
    /// Checks if the DateTime is in the future.
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <returns>True if the DateTime is in the future; otherwise, false.</returns>
    public static bool IsFuture(this DateTime date)
        => date > DateTime.Now;

    /// <summary>
    /// Converts the DateTime to a short month string (e.g., "Jan").
    /// </summary>
    /// <param name="date">The DateTime object to convert.</param>
    /// <returns>The short month string.</returns>
    public static string ToShortMonthString(this DateTime date)
        => date.ToString("MMM");

    /// <summary>
    /// Checks if two DateTime objects represent the same day.
    /// </summary>
    /// <param name="date1">The first DateTime object.</param>
    /// <param name="date2">The second DateTime object.</param>
    /// <returns>True if the DateTime objects represent the same day; otherwise, false.</returns>
    public static bool IsSameDay(this DateTime date1, DateTime date2)
        => date1.Date == date2.Date;

    /// <summary>
    /// Gets the start of the month for a given DateTime.
    /// </summary>
    /// <param name="date">The DateTime object.</param>
    /// <returns>The DateTime set to the start of the month.</returns>
    public static DateTime StartOfMonth(this DateTime date)
        => new(date.Year, date.Month, 1);

    /// <summary>
    /// Gets the end of the month for a given DateTime.
    /// </summary>
    /// <param name="date">The DateTime object.</param>
    /// <returns>The DateTime set to the end of the month.</returns>
    public static DateTime EndOfMonth(this DateTime date)
    {
        var endOfMonth = new DateTime(date.Year, date.Month, date.DaysInMonth());
        return endOfMonth.ToDaysEnd();
    }

    /// <summary>
    /// Gets the start of the year for a given DateTime.
    /// </summary>
    /// <param name="date">The DateTime object.</param>
    /// <returns>The DateTime set to the start of the year.</returns>
    public static DateTime StartOfYear(this DateTime date)
       => new(date.Year, 1, 1);

    /// <summary>
    /// Gets the end of the year for a given DateTime.
    /// </summary>
    /// <param name="date">The DateTime object.</param>
    /// <returns>The DateTime set to the end of the year.</returns>
    public static DateTime EndOfYear(this DateTime date)
    {
        var endOfYear = new DateTime(date.Year, 12, DateTime.DaysInMonth(date.Year, 12));
        return endOfYear.ToDaysEnd();
    }

    /// <summary>
    /// Rounds the DateTime to the nearest minute.
    /// </summary>
    /// <param name="date">The DateTime object to round.</param>
    /// <returns>The DateTime rounded to the nearest minute.</returns>
    public static DateTime RoundToNearestMinute(this DateTime date)
        => new(date.Ticks - (date.Ticks % TimeSpan.TicksPerMinute), date.Kind);

    /// <summary>
    /// Gets a human-readable elapsed time string from the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to calculate the elapsed time from.</param>
    /// <returns>The elapsed time string.</returns>
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

    /// <summary>
    /// Gets the elapsed minutes from the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to calculate the elapsed minutes from.</param>
    /// <returns>The elapsed minutes.</returns>
    public static double ElapsedMinutes(this DateTime date)
    {
        var elapsed = DateTime.Now - date;
        return elapsed.TotalMinutes;
    }

    /// <summary>
    /// Gets the elapsed hours from the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to calculate the elapsed hours from.</param>
    /// <returns>The elapsed hours.</returns>
    public static double ElapsedHours(this DateTime date)
    {
        var elapsed = DateTime.Now - date;
        return elapsed.TotalHours;
    }

    /// <summary>
    /// Gets the elapsed days from the DateTime.
    /// </summary>
    /// <param name="date">The DateTime object to calculate the elapsed days from.</param>
    /// <returns>The elapsed days.</returns>
    public static double ElapsedDays(this DateTime date)
    {
        var elapsed = DateTime.Now - date;
        return elapsed.TotalDays;
    }

    /// <summary>
    /// Checks if the time is in the morning.
    /// </summary>
    /// <param name="time">The DateTime object representing a time.</param>
    /// <returns>True if the time is in the morning; otherwise, false.</returns>
    public static bool IsMorning(this DateTime time)
        => time.Hour >= 0 && time.Hour < 12;

    /// <summary>
    /// Checks if the time is in the afternoon.
    /// </summary>
    /// <param name="time">The DateTime object representing a time.</param>
    /// <returns>True if the time is in the afternoon; otherwise, false.</returns>
    public static bool IsAfternoon(this DateTime time)
        => time.Hour >= 12 && time.Hour < 18;

    /// <summary>
    /// Checks if the time is in the evening.
    /// </summary>
    /// <param name="time">The DateTime object representing a time.</param>
    /// <returns>True if the time is in the evening; otherwise, false.</returns>
    public static bool IsEvening(this DateTime time)
        => time.Hour >= 18 && time.Hour < 24;

    /// <summary>
    /// Gets the number of days in the month for a given DateTime.
    /// </summary>
    /// <param name="date">The DateTime object.</param>
    /// <returns>The number of days in the month.</returns>
    public static int DaysInMonth(this DateTime date)
        => DateTime.DaysInMonth(date.Year, date.Month);

    /// <summary>
    /// Checks if the DateTime represents a leap day (February 29th).
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <returns>True if the DateTime represents a leap day; otherwise, false.</returns>
    public static bool IsLeapDay(this DateTime date)
        => date.Month == 2 && date.Day == 29;

    /// <summary>
    /// Checks if the DateTime is yesterday.
    /// </summary>
    /// <param name="date">The DateTime object to check.</param>
    /// <returns>True if the DateTime is yesterday; otherwise, false.</returns>
    public static bool IsYesterday(this DateTime date)
        => date.Date == DateTime.Today.AddDays(-1);

    /// <summary>
    /// Checks if two DateTime objects represent the same year.
    /// </summary>
    /// <param name="date1">The first DateTime object.</param>
    /// <param name="date2">The second DateTime object.</param>
    /// <returns>True if the DateTime objects represent the same year; otherwise, false.</returns>
    public static bool IsSameYear(this DateTime date1, DateTime date2)
        => date1.Year == date2.Year;
}
