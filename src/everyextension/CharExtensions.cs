namespace EveryExtension;

/// <summary>
/// Extension methods for char type, providing various character-related functionalities.
/// </summary>
public static class CharExtensions
{
    /// <summary>
    /// Checks if the character is a digit.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a digit; otherwise, false.</returns>
    public static bool IsDigit(this char c)
        => char.IsDigit(c);

    /// <summary>
    /// Checks if the character is a letter.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a letter; otherwise, false.</returns>
    public static bool IsLetter(this char c)
        => char.IsLetter(c);

    /// <summary>
    /// Checks if the character is a white space.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is white space; otherwise, false.</returns>
    public static bool IsWhiteSpace(this char c)
        => char.IsWhiteSpace(c);

    /// <summary>
    /// Converts the character to upper case.
    /// </summary>
    /// <param name="c">The character to convert.</param>
    /// <returns>The uppercased character.</returns>
    public static char ToUpperCase(this char c)
        => char.ToUpper(c);

    /// <summary>
    /// Converts the character to lower case.
    /// </summary>
    /// <param name="c">The character to convert.</param>
    /// <returns>The lowercased character.</returns>
    public static char ToLowerCase(this char c)
        => char.ToLower(c);

    /// <summary>
    /// Checks if the character is a punctuation mark.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a punctuation mark; otherwise, false.</returns>
    public static bool IsPunctuation(this char c)
        => char.IsPunctuation(c);

    /// <summary>
    /// Checks if the character is a vowel.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a vowel; otherwise, false.</returns>
    public static bool IsVowel(this char c)
    {
        var lowerC = c.ToLowerCase();
        return lowerC == 'a' || lowerC == 'e' || lowerC == 'i' || lowerC == 'o' || lowerC == 'u';
    }

    /// <summary>
    /// Checks if the character is a consonant.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a consonant; otherwise, false.</returns>
    public static bool IsConsonant(this char c)
        => c.IsLetter() && !c.IsVowel();

    /// <summary>
    /// Checks if the character is upper case.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is upper case; otherwise, false.</returns>
    public static bool IsUpperCase(this char c)
        => char.IsUpper(c);

    /// <summary>
    /// Checks if the character is lower case.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is lower case; otherwise, false.</returns>
    public static bool IsLowerCase(this char c)
        => char.IsLower(c);

    /// <summary>
    /// Checks if the character is a hexadecimal digit.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a hexadecimal digit; otherwise, false.</returns>
    public static bool IsHexDigit(this char c)
        => "0123456789ABCDEFabcdef".Contains(c);

    /// <summary>
    /// Converts the character to its digit representation.
    /// </summary>
    /// <param name="c">The character to convert.</param>
    /// <returns>The digit representation of the character.</returns>
    /// <exception cref="ArgumentException">Thrown if the character is not a digit.</exception>
    public static int ToDigit(this char c)
    {
        if (c.IsDigit())
            return int.Parse(c.ToString());
        throw new ArgumentException("The character is not a digit.");
    }

    /// <summary>
    /// Repeats the character a specified number of times.
    /// </summary>
    /// <param name="c">The character to repeat.</param>
    /// <param name="count">The number of times to repeat the character.</param>
    /// <returns>A string consisting of the repeated character.</returns>
    public static string Repeat(this char c, int count)
        => new(c, count);

    /// <summary>
    /// Checks if the character is a mathematical operator.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a mathematical operator; otherwise, false.</returns>
    public static bool IsMathOperator(this char c)
        => "+-*/".Contains(c);

    /// <summary>
    /// Checks if the character is a control character.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a control character; otherwise, false.</returns>
    public static bool IsControlCharacter(this char c)
        => char.IsControl(c);

    /// <summary>
    /// Checks if the character is a high surrogate.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a high surrogate; otherwise, false.</returns>
    public static bool IsHighSurrogate(this char c)
        => char.IsHighSurrogate(c);

    /// <summary>
    /// Checks if the character is a low surrogate.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a low surrogate; otherwise, false.</returns>
    public static bool IsLowSurrogate(this char c)
        => char.IsLowSurrogate(c);

    /// <summary>
    /// Checks if the character is a separator character.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a separator character; otherwise, false.</returns>
    public static bool IsSeparator(this char c)
        => char.IsSeparator(c);

    /// <summary>
    /// Checks if the character is alphanumeric.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is alphanumeric; otherwise, false.</returns>
    public static bool IsAlphanumeric(this char c)
        => char.IsLetterOrDigit(c);

    /// <summary>
    /// Converts the character to its alphabetical index.
    /// </summary>
    /// <param name="c">The character to convert.</param>
    /// <returns>The alphabetical index of the character.</returns>
    /// <exception cref="ArgumentException">Thrown if the character is not a letter.</exception>
    public static int ToAlphabeticalIndex(this char c)
    {
        if (c.IsLetter())
            return c.ToUpperCase() - 'A' + 1;
        throw new ArgumentException("The character is not a letter.");
    }

    /// <summary>
    /// Checks if the character is a symbol.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a symbol; otherwise, false.</returns>
    public static bool IsSymbol(this char c)
        => char.IsSymbol(c);

    /// <summary>
    /// Checks if the character is a surrogate pair.
    /// </summary>
    /// <param name="c">The character to check.</param>
    /// <returns>True if the character is a surrogate pair; otherwise, false.</returns>
    public static bool IsSurrogatePair(this char c)
        => char.IsSurrogatePair(c.ToString(), 0);
}

