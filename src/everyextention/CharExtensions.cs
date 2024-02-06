namespace EveryExtention;

public static class CharExtensions
{
    public static bool IsDigit(this char c)
        => char.IsDigit(c);

    public static bool IsLetter(this char c)
        => char.IsLetter(c);

    public static bool IsWhiteSpace(this char c)
        => char.IsWhiteSpace(c);

    public static char ToUpperCase(this char c)
        => char.ToUpper(c);

    public static char ToLowerCase(this char c)
        => char.ToLower(c);

    public static bool IsPunctuation(this char c)
        => char.IsPunctuation(c);

    public static bool IsVowel(this char c)
    {
        var lowerC = c.ToLowerCase();
        return lowerC == 'a' || lowerC == 'e' || lowerC == 'i' || lowerC == 'o' || lowerC == 'u';
    }

    public static bool IsConsonant(this char c)
        => c.IsLetter() && !c.IsVowel();

    public static bool IsUpperCase(this char c)
        => char.IsUpper(c);

    public static bool IsLowerCase(this char c)
        => char.IsLower(c);

    public static bool IsHexDigit(this char c)
        => "0123456789ABCDEFabcdef".Contains(c);

    public static int ToDigit(this char c)
    {
        if (c.IsDigit())
            return int.Parse(c.ToString());
        throw new ArgumentException("The character is not a digit.");
    }

    public static string Repeat(this char c, int count)
        => new(c, count);

    public static bool IsMathOperator(this char c)
        => "+-*/".Contains(c);

    public static bool IsControlCharacter(this char c)
        => char.IsControl(c);

    public static bool IsHighSurrogate(this char c)
        => char.IsHighSurrogate(c);

    public static bool IsLowSurrogate(this char c)
        => char.IsLowSurrogate(c);

    public static bool IsSeparator(this char c)
        => char.IsSeparator(c);

    public static bool IsAlphanumeric(this char c)
        => char.IsLetterOrDigit(c);

    public static int ToAlphabeticalIndex(this char c)
    {
        if (c.IsLetter())
            return c.ToUpperCase() - 'A' + 1;
        throw new ArgumentException("The character is not a letter.");
    }

    public static bool IsSymbol(this char c)
        => char.IsSymbol(c);

    public static bool IsSurrogatePair(this char c)
        => char.IsSurrogatePair(c.ToString(), 0);
}
