namespace EveryExtension;

public static class DateTimeOffsetExtensions
{
    public static DateTimeOffset ToDaysEnd(this DateTimeOffset dt)
       => dt.AddDays(1).AddTicks(-1);

    public static DateTimeOffset ToDaysStart(this DateTimeOffset dt)
       => new(dt.Year, dt.Month, dt.Day, 0, 0, 0, 0, dt.Offset);
}
