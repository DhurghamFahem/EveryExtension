namespace EveryExtension;

public static class TimeSpanExtensions
{
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

    public static double ToMilliseconds(this TimeSpan timeSpan)
        => timeSpan.TotalMilliseconds;

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

    public static bool IsNegative(this TimeSpan timeSpan)
        => timeSpan < TimeSpan.Zero;

    public static TimeSpan Negate(this TimeSpan timeSpan)
        => TimeSpan.Zero - timeSpan;

    public static TimeSpan RoundToNearestMinute(this TimeSpan timeSpan)
    {
        var minutes = (int)Math.Round(timeSpan.TotalMinutes);
        return TimeSpan.FromMinutes(minutes);
    }

    public static string ToFriendlyString(this TimeSpan timeSpan)
    {
        var formatted = $"{(int)timeSpan.TotalDays}d {timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";
        return formatted.Replace("0d ", "").Replace("0h ", "").Replace("0m ", "").Replace("0s", "");
    }

    public static bool IsGreaterThan(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) > 0;

    public static bool IsLessThan(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) < 0;

    public static bool IsEquals(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.CompareTo(other) < 0;

    public static string ToStopwatchString(this TimeSpan timeSpan)
        => timeSpan.ToString(@"hh\:mm\:ss");

    public static string Format(this TimeSpan timeSpan, string format)
        => string.Format(format, timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

    public static TimeSpan Multiply(this TimeSpan timeSpan, double factor)
        => TimeSpan.FromTicks((long)(timeSpan.Ticks * factor));

    public static bool IsMultipleOf(this TimeSpan timeSpan, TimeSpan other)
        => timeSpan.Ticks % other.Ticks == 0;

    public static double ToMonths(this TimeSpan timeSpan)
        => (double)timeSpan.Days / 30.44;

    public static double ToYears(this TimeSpan timeSpan)
        => (double)timeSpan.Days / 365.25;

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
