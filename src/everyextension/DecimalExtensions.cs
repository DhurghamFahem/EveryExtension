namespace EveryExtension;

public static class DecimalExtensions
{
    public static decimal PercentageOf(this decimal value, decimal percentage)
        => value * (percentage / 100);

    public static decimal RoundTo(this decimal value, int decimalPlaces)
        => Math.Round(value, decimalPlaces);

    public static bool IsWholeNumber(this decimal value)
        => value % 1 == 0;

    public static string ToMoneyString(this decimal value)
        => value.ToString("C");

    public static decimal Abs(this decimal value)
        => Math.Abs(value);

    public static decimal SquareRoot(this decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Cannot calculate square root of a negative number");
        return (decimal)Math.Sqrt((double)value);
    }

    public static decimal PercentageChange(this decimal oldValue, decimal newValue)
    {
        if (oldValue == 0)
            throw new ArgumentException("Cannot calculate percentage change with a denominator of zero");
        return ((newValue - oldValue) / oldValue.Abs()) * 100;
    }

    public static string ToRoundedString(this decimal value, int decimalPlaces)
        => value.ToString($"F{decimalPlaces}");

    public static decimal Increment(this decimal value, decimal step)
        => value + step;

    public static decimal Decrement(this decimal value, decimal step)
        => value - step;

    public static bool IsPositive(this decimal value)
        => value > 0;

    public static bool IsNegative(this decimal value)
        => value < 0;

    public static bool IsZero(this decimal value)
        => value == 0;

    public static string ToPercentageString(this decimal value)
        => $"{value * 100:F2}%";

    public static decimal Ceiling(this decimal value)
        => Math.Ceiling(value);

    public static decimal Floor(this decimal value)
        => Math.Floor(value);

    public static bool IsBetween(this decimal value, decimal minValue, decimal maxValue)
        => value >= minValue && value <= maxValue;

    public static string ToWords(this decimal value)
    {
        //TODO
        return "Not Implemented";
    }

    public static decimal PercentageOfTotal(this decimal value, decimal total)
    {
        if (total == 0)
            throw new ArgumentException("Cannot calculate percentage of total with a denominator of zero");

        return (value / total) * 100;
    }

    public static bool IsEven(this decimal value)
        => value % 2 == 0;

    public static bool IsOdd(this decimal value)
        => value % 2 != 0;

    public static bool IsPrime(this decimal value)
    {
        if (value <= 1)
            return false;

        for (int i = 2; i <= value.SquareRoot(); i++)
        {
            if (value % i == 0)
                return false;
        }

        return true;
    }

    public static decimal Square(this decimal value)
        => value * value;

    public static decimal Reverse(this decimal value)
    {
        var digits = value.ToString().ToCharArray();
        Array.Reverse(digits);
        return decimal.Parse(new string(digits));
    }

    public static decimal ToPowerOf(this decimal value, int power)
        => (decimal)Math.Pow((double)value, power);

    public static bool IsMultipleOf(this decimal value, decimal multiple)
    {
        if (multiple == 0)
            throw new ArgumentException("Cannot check for multiples with a denominator of zero");
        return value % multiple == 0;
    }

    public static string ToOrdinal(this decimal value)
    {
        string suffix;
        if (value % 100 >= 11 && value % 100 <= 13)
            suffix = "th";
        else
            suffix = (value % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        return $"{value}{suffix}";
    }

    public static int DigitCount(this decimal value)
        => value.ToString().Replace(".", "").Length;

    public static string ToCurrencyWords(this decimal value)
    {
        //TODO
        return "Not Implemented";
    }

    public static decimal RandomInRange(this decimal minValue, decimal maxValue)
    {
        var random = new Random();
        return (decimal)random.NextDouble() * (maxValue - minValue) + minValue;
    }

    public static bool IsPerfectSquare(this decimal value)
    {
        var sqrt = (int)value.SquareRoot();
        return value == sqrt * sqrt;
    }

    public static int[] GetDigits(this decimal value)
        => value.ToString().Where(c => c.IsDigit()).Select(c => int.Parse(c.ToString())).ToArray();

    public static decimal Factorial(this decimal value)
    {
        if (value < 0 || value % 1 != 0)
            throw new ArgumentException("Factorial is only defined for non-negative integers");

        var result = 1m;
        for (var i = 2m; i <= value; i++)
            result *= i;
        return result;
    }

    public static decimal Cubed(this decimal value)
        => value * value * value;

    public static decimal Fibonacci(this decimal n)
    {
        if (n < 0 || n % 1 != 0)
            throw new ArgumentException("Fibonacci is only defined for non-negative integers");
        decimal a = 0, b = 1;
        for (var i = 0m; i < n; i++)
        {
            var temp = a;
            a = b;
            b = temp + b;
        }
        return a;
    }

    public static decimal Logarithm(this decimal value, double baseValue)
        => (decimal)(Math.Log((double)value) / Math.Log(baseValue));

    public static decimal RoundToSignificantFigures(this decimal value, int significantFigures)
    {
        if (value == 0)
            return 0;
        var scale = (decimal)Math.Pow(10, significantFigures - 1);
        return Math.Round(value * scale) / scale;
    }

    public static bool IsDivisibleBy(this decimal value, decimal divisor)
    {
        if (divisor == 0)
            throw new ArgumentException("Cannot check divisibility with a divisor of zero");
        return value % divisor == 0;
    }

    public static decimal Clamp(this decimal value, decimal minValue, decimal maxValue)
        => Math.Max(minValue, Math.Min(maxValue, value));

    public static string ToHexadecimal(this decimal value)
        => Convert.ToString((long)value, 16).ToUpper();

    public static decimal Root(this decimal value, int n)
        => (decimal)Math.Pow((double)value, 1.0 / n);
}
