namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with TimeSpan.
/// </summary>
public static class TimeSpanExtensions
{
    /// <summary>
    /// Converts a TimeSpan to a human-readable string.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>A human-readable string representation of the TimeSpan.</returns>
    public static string ToReadableString(this TimeSpan timeSpan)
    {
        if (timeSpan.TotalDays >= 1)
            return $"{(int)timeSpan.TotalDays} days, {timeSpan.Hours} hours, {timeSpan.Minutes} minutes";
        if (timeSpan.TotalHours >= 1)
            return $"{timeSpan.Hours} hours, {timeSpan.Minutes} minutes, {timeSpan.Seconds} seconds";
        if (timeSpan.TotalMinutes >= 1)
            return $"{timeSpan.Minutes} minutes, {timeSpan.Seconds} seconds";
        return $"{timeSpan.Seconds} seconds";
    }

    /// <summary>
    /// Converts a TimeSpan to its total milliseconds.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>The total milliseconds of the TimeSpan.</returns>
    public static double ToMilliseconds(this TimeSpan timeSpan)
        => timeSpan.TotalMilliseconds;

    /// <summary>
    /// Converts a TimeSpan to a human-readable "time ago" string.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>A string indicating how long ago the TimeSpan occurred.</returns>
    public static string Ago(this TimeSpan timeSpan)
    {
        if (timeSpan.TotalSeconds < 60)
            return $"{(int)timeSpan.TotalSeconds} seconds ago";

        if (timeSpan.TotalMinutes < 60)
            return $"{(int)timeSpan.TotalMinutes} minutes ago";

        if (timeSpan.TotalHours < 24)
            return $"{(int)timeSpan.TotalHours} hours ago";

        return $"{(int)timeSpan.TotalDays} days ago";
    }

    /// <summary>
    /// Checks if the TimeSpan is negative.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to check.</param>
    /// <returns>True if the TimeSpan is negative; otherwise, false.</returns>
    public static bool IsNegative(this TimeSpan timeSpan)
        => timeSpan < TimeSpan.Zero;

    /// <summary>
    /// Negates the TimeSpan.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to negate.</param>
    /// <returns>The negated TimeSpan.</returns>
    public static TimeSpan Negate(this TimeSpan timeSpan)
        => TimeSpan.Zero - timeSpan;

    /// <summary>
    /// Rounds the TimeSpan to the nearest minute.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to round.</param>
    /// <returns>The TimeSpan rounded to the nearest minute.</returns>
    public static TimeSpan RoundToNearestMinute(this TimeSpan timeSpan)
    {
        var minutes = (int)Math.Round(timeSpan.TotalMinutes);
        return TimeSpan.FromMinutes(minutes);
    }

    /// <summary>
    /// Converts a TimeSpan to a friendly string format.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>A friendly string representation of the TimeSpan.</returns>
    public static string ToFriendlyString(this TimeSpan timeSpan)
    {
        var formatted = $"{(int)timeSpan.TotalDays}d {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
        return formatted.Replace("0d ", "").Replace("0h ", "").Replace("0m ", "").Replace("0s", "");
    }

    /// <summary>
    /// Checks if the TimeSpan is greater than another TimeSpan.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to compare.</param>
    /// <param name="other">The TimeSpan to compare against.</param>
    /// <returns>True if the TimeSpan is greater; otherwise, false.</returns>
    public static bool IsGreaterThan(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) > 0;

    /// <summary>
    /// Checks if the TimeSpan is less than another TimeSpan.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to compare.</param>
    /// <param name="other">The TimeSpan to compare against.</param>
    /// <returns>True if the TimeSpan is less; otherwise, false.</returns>
    public static bool IsLessThan(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) < 0;

    /// <summary>
    /// Checks if the TimeSpan is equal to another TimeSpan.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to compare.</param>
    /// <param name="other">The TimeSpan to compare against.</param>
    /// <returns>True if the TimeSpan is equal; otherwise, false.</returns>
    public static bool IsEquals(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) < 0;

    /// <summary>
    /// Converts the TimeSpan to a stopwatch-style string.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>A stopwatch-style string representation of the TimeSpan.</returns>
    public static string ToStopwatchString(this TimeSpan timeSpan)
        => timeSpan.ToString(@"hh\:mm\:ss");

    /// <summary>
    /// Formats the TimeSpan using a custom format string.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to format.</param>
    /// <param name="format">The custom format string.</param>
    /// <returns>The formatted TimeSpan string.</returns>
    public static string Format(this TimeSpan timeSpan, string format)
        => string.Format(format, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

    /// <summary>
    /// Multiplies the TimeSpan by a factor.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to multiply.</param>
    /// <param name="factor">The multiplication factor.</param>
    /// <returns>The multiplied TimeSpan.</returns>
    public static TimeSpan Multiply(this TimeSpan timeSpan, double factor)
        => TimeSpan.FromTicks((long)(timeSpan.Ticks * factor));

    /// <summary>
    /// Checks if the TimeSpan is a multiple of another TimeSpan.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to check.</param>
    /// <param name="other">The TimeSpan to check against.</param>
    /// <returns>True if the TimeSpan is a multiple; otherwise, false.</returns>
    public static bool IsMultipleOf(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.Ticks % other.Ticks == 0;

    /// <summary>
    /// Converts the TimeSpan to months.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>The TimeSpan converted to months.</returns>
    public static double ToMonths(this TimeSpan timeSpan)
        => (double)timeSpan.Days / 30.44;

    /// <summary>
    /// Converts the TimeSpan to years.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>The TimeSpan converted to years.</returns>
    public static double ToYears(this TimeSpan timeSpan)
        => (double)timeSpan.Days / 365.25;

    /// <summary>
    /// Converts the TimeSpan to a short string format.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan to convert.</param>
    /// <returns>A short string representation of the TimeSpan.</returns>
    public static string ToShortString(this TimeSpan timeSpan)
    {
        var components = new List<string>();
        if (timeSpan.Days > 0)
            components.Add($"{timeSpan.Days}d");
        if (timeSpan.Hours > 0)
            components.Add($"{timeSpan.Hours}h");
        if (timeSpan.Minutes > 0)
            components.Add($"{timeSpan.Minutes}m");
        if (timeSpan.Seconds > 0)
            components.Add($"{timeSpan.Seconds}s");
        return string.Join(" ", components);
    }
}
