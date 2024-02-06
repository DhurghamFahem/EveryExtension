using System.Globalization;
using System.Numerics;

namespace EveryExtension;

public static class NumberExtensions
{
    public static TNumber PercentageOf<TNumber>(this TNumber value, TNumber percentage) where TNumber : INumber<TNumber>
         => value * (percentage / TNumber.CreateChecked(100));

    public static string ToMoneyString<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value.ToString("C", CultureInfo.CurrentCulture.NumberFormat)!;

    public static TNumber Abs<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value >= TNumber.CreateChecked(0) ? value : TNumber.CreateChecked(-1) * value;

    public static TNumber SquareRoot<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        if (value < TNumber.CreateChecked(0))
            throw new ArgumentException("Cannot calculate square root of a negative number");
        return TNumber.CreateChecked(Math.Sqrt(double.CreateSaturating(value)));
    }

    public static TNumber RoundTo<TNumber>(this TNumber value, int decimalPlaces) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Round(double.CreateSaturating(value), decimalPlaces));

    public static bool IsWholeNumber<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value % TNumber.CreateChecked(1) == TNumber.CreateChecked(0);

    public static TNumber PercentageChange<TNumber>(this TNumber oldValue, TNumber newValue) where TNumber : INumber<TNumber>
    {
        if (oldValue == TNumber.CreateChecked(0))
            throw new ArgumentException("Cannot calculate percentage change with a denominator of zero");
        return ((newValue - oldValue) / oldValue.Abs()) * TNumber.CreateChecked(100);
    }

    public static string ToRoundedString<TNumber>(this TNumber value, int decimalPlaces) where TNumber : INumber<TNumber>
        => value.ToString($"F{decimalPlaces}", CultureInfo.CurrentCulture.NumberFormat);

    public static TNumber Increment<TNumber>(this TNumber value, TNumber step) where TNumber : INumber<TNumber>
        => value + step;

    public static TNumber Decrement<TNumber>(this TNumber value, TNumber step) where TNumber : INumber<TNumber>
        => value - step;

    public static bool IsPositive<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value > TNumber.CreateChecked(0);

    public static bool IsNegative<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value < TNumber.CreateChecked(0);

    public static bool IsZero<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value == TNumber.CreateChecked(0);

    public static string ToPercentageString<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => $"{value * TNumber.CreateChecked(100):F2}%";

    public static TNumber Ceiling<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Ceiling(double.CreateChecked(value)));

    public static TNumber Floor<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Floor(double.CreateChecked(value)));

    public static bool IsBetween<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue) where TNumber : INumber<TNumber>
        => value >= minValue && value <= maxValue;

    public static TNumber PercentageOfTotal<TNumber>(this TNumber value, TNumber total) where TNumber : INumber<TNumber>
    {
        if (total == TNumber.CreateChecked(0))
            throw new ArgumentException("Cannot calculate percentage of total with a denominator of zero");
        return (value / total) * TNumber.CreateChecked(100);
    }

    public static bool IsEven<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
       => value % TNumber.CreateChecked(2) == TNumber.CreateChecked(0);

    public static bool IsOdd<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value % TNumber.CreateChecked(2) != TNumber.CreateChecked(0);

    public static bool IsPrime<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        if (value <= TNumber.CreateChecked(1))
            return false;
        for (int i = 2; i <= int.CreateChecked(value.SquareRoot()); i++)
        {
            if (value % TNumber.CreateChecked(i) == TNumber.CreateChecked(0))
                return false;
        }
        return true;
    }

    public static TNumber Square<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value * value;

    public static TNumber Reverse<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        var digits = value.ToString()!.ToCharArray();
        Array.Reverse(digits);
        return TNumber.CreateChecked(decimal.Parse(new string(digits)));
    }

    public static TNumber ToPowerOf<TNumber>(this TNumber value, int power) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Pow(double.CreateChecked(value), power));

    public static bool IsMultipleOf<TNumber>(this TNumber value, TNumber multiple) where TNumber : INumber<TNumber>
    {
        if (multiple == TNumber.CreateChecked(0))
            throw new ArgumentException("Cannot check for multiples with a denominator of zero");
        return value % multiple == TNumber.CreateChecked(0);
    }

    public static string ToOrdinal<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        string suffix;
        if (value % TNumber.CreateChecked(100) >= TNumber.CreateChecked(11) && value % TNumber.CreateChecked(100) <= TNumber.CreateChecked(13))
            suffix = "th";
        else
            suffix = (value % TNumber.CreateChecked(10)) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        return $"{value}{suffix}";
    }

    public static int DigitCount<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value.ToString()!.Replace(".", "").Length;

    public static TNumber RandomInRange<TNumber>(this TNumber minValue, TNumber maxValue) where TNumber : INumber<TNumber>
    {
        var random = new Random();
        return TNumber.CreateChecked(random.NextDouble()) * (maxValue - minValue) + minValue;
    }

    public static bool IsPerfectSquare<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        var sqrt = int.CreateChecked(value.SquareRoot());
        return value == TNumber.CreateChecked(sqrt * sqrt);
    }

    public static int[] GetDigits<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value.ToString()!.Where(c => c.IsDigit()).Select(c => int.Parse(c.ToString())).ToArray();

    public static TNumber Factorial<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        if (value < TNumber.CreateChecked(0) || value % TNumber.CreateChecked(1) != TNumber.CreateChecked(0))
            throw new ArgumentException("Factorial is only defined for non-negative integers");

        var result = TNumber.CreateChecked(1);
        for (var i = TNumber.CreateChecked(2); i <= value; i++)
            result *= i;
        return result;
    }

    public static TNumber Cubed<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
        => value * value * value;

    public static TNumber Fibonacci<TNumber>(this TNumber value) where TNumber : INumber<TNumber>
    {
        if (value < TNumber.CreateChecked(0) || value % TNumber.CreateChecked(1) != TNumber.CreateChecked(0))
            throw new ArgumentException("Fibonacci is only defined for non-negative integers");
        TNumber a = TNumber.CreateChecked(0), b = TNumber.CreateChecked(1);
        for (var i = TNumber.CreateChecked(0); i < value; i++)
        {
            var temp = a;
            a = b;
            b = temp + b;
        }
        return a;
    }

    public static TNumber Logarithm<TNumber>(this TNumber value, double baseValue) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked((Math.Log(double.CreateChecked(value)) / Math.Log(baseValue)));

    public static TNumber RoundToSignificantFigures<TNumber>(this TNumber value, int significantFigures) where TNumber : INumber<TNumber>
    {
        if (value == TNumber.CreateChecked(0))
            return TNumber.CreateChecked(0);
        var scale = (decimal)Math.Pow(10, significantFigures - 1);
        return TNumber.CreateChecked(Math.Round(double.CreateChecked(value) * (double)scale) / (double)scale);
    }

    public static bool IsDivisibleBy<TNumber>(this TNumber value, TNumber divisor) where TNumber : INumber<TNumber>
    {
        if (divisor == TNumber.CreateChecked(0))
            throw new ArgumentException("Cannot check divisibility with a divisor of zero");
        return value % divisor == TNumber.CreateChecked(0);
    }

    public static TNumber Clamp<TNumber>(this TNumber value, TNumber minValue, TNumber maxValue) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Max(double.CreateChecked(minValue), Math.Min(double.CreateChecked(maxValue), double.CreateChecked(value))));

    public static TNumber Root<TNumber>(this TNumber value, int n) where TNumber : INumber<TNumber>
        => TNumber.CreateChecked(Math.Pow(double.CreateChecked(value), 1.0 / n));
}
