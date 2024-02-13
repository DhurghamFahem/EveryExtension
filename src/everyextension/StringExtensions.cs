using System.Globalization;
using System.Net;
using System.Text;

namespace EveryExtension;

/// <summary>
/// A static class containing extension methods for string manipulation and validation.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Determines whether a string is null, empty, or consists only of white-space characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is null, empty, or consists only of white-space characters; otherwise, false.</returns>
    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Determines whether a string is null, empty, or consists only of white-space characters, including an additional check for an empty string.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is null, empty, or consists only of white-space characters; otherwise, false.</returns>
    public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        => string.IsNullOrEmpty(value) || value.IsNullOrWhiteSpace();

    /// <summary>
    /// Truncates a string to the specified maximum length and appends ellipsis if necessary.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum length of the truncated string.</param>
    /// <returns>The truncated string.</returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;
        return value[..maxLength] + "...";
    }

    /// <summary>
    /// Determines whether a string is in email format.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is in email format; otherwise, false.</returns>
    public static bool IsEmail(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return false;
        return IPAddress.TryParse(value, out _);
    }

    /// <summary>
    /// Converts the first character of a string to title case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string with the first character in title case.</returns>
    public static string ToTitleCase(this string value)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
    }

    /// <summary>
    /// Determines whether a string represents a numeric value.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string represents a numeric value; otherwise, false.</returns>
    public static bool IsNumeric(this string value)
    {
        return decimal.TryParse(value, out _);
    }

    /// <summary>
    /// Removes white-space characters from a string.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string with white-space characters removed.</returns>
    public static string RemoveWhitespace(this string value)
    {
        return new string(value.ToCharArray()
            .Where(c => !c.IsWhiteSpace())
            .ToArray());
    }

    /// <summary>
    /// Determines whether a string is a valid URL.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is a valid URL; otherwise, false.</returns>
    public static bool IsUrl(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;
        return Uri.TryCreate(value, UriKind.Absolute, out _);
    }

    /// <summary>
    /// Counts the number of words in a string.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The number of words in the string.</returns>
    public static int WordCount(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;
        return value.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    /// <summary>
    /// Determines whether a string is alphanumeric.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is alphanumeric; otherwise, false.</returns>
    public static bool IsAlphaNumeric(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return value.All(c => c.IsAlphanumeric());
    }

    /// <summary>
    /// Encodes a string to Base64.
    /// </summary>
    /// <param name="value">The string to encode.</param>
    /// <returns>The Base64-encoded string.</returns>
    public static string Base64Encode(this string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Decodes a Base64-encoded string.
    /// </summary>
    /// <param name="value">The Base64-encoded string to decode.</param>
    /// <returns>The decoded string.</returns>
    public static string Base64Decode(this string value)
    {
        var bytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(bytes);
    }

    /// <summary>
    /// Removes diacritics (accents) from a string.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <returns>The string without diacritics.</returns>
    public static string RemoveDiacritics(this string value)
    {
        var normalizedString = value.Normalize(NormalizationForm.FormD);
        var stringBuilder = new StringBuilder();
        foreach (char c in normalizedString)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(c);
        }
        return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
    }

    /// <summary>
    /// Determines whether a string is an anagram of another string.
    /// </summary>
    /// <param name="value">The first string.</param>
    /// <param name="other">The second string to compare for anagram.</param>
    /// <returns>true if the strings are anagrams; otherwise, false.</returns>
    public static bool IsAnagramOf(this string value, string other)
    {
        if (value.Length != other.Length)
            return false;
        var valueChars = value.ToCharArray();
        var otherChars = other.ToCharArray();
        Array.Sort(valueChars);
        Array.Sort(otherChars);
        return valueChars.SequenceEqual(otherChars);
    }

    /// <summary>
    /// Converts the first character of a string to camel case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string with the first character in camel case.</returns>
    public static string ToCamelCase(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return value;
        return value[0].ToLowerCase() + value.Substring(1);
    }

    /// <summary>
    /// Converts a string to snake case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The string in snake case.</returns>
    public static string ToSnakeCase(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return value;
        return string.Concat(value.Select((x, i) =>
            i > 0 && x.IsUpperCase() ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    /// <summary>
    /// Extracts a substring from between two markers within a string.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <param name="startMarker">The starting marker.</param>
    /// <param name="endMarker">The ending marker.</param>
    /// <returns>The substring between the markers.</returns>
    public static string SubstringBetween(this string value, string startMarker, string endMarker)
    {
        var startIndex = value.IndexOf(startMarker);
        var endIndex = value.LastIndexOf(endMarker);
        if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex)
            return string.Empty;
        return value.Substring(startIndex + startMarker.Length, endIndex - startIndex - startMarker.Length);
    }

    /// <summary>
    /// Counts the occurrences of a substring within a string.
    /// </summary>
    /// <param name="value">The string to search.</param>
    /// <param name="substring">The substring to count.</param>
    /// <returns>The number of occurrences of the substring.</returns>
    public static int CountOccurrences(this string value, string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring))
            return 0;
        var count = 0;
        var index = 0;
        while ((index = value.IndexOf(substring, index, StringComparison.Ordinal)) != -1)
        {
            index += substring.Length;
            count++;
        }
        return count;
    }

    /// <summary>
    /// Swaps the case of letters in a string.
    /// </summary>
    /// <param name="value">The string to swap the case of.</param>
    /// <returns>The string with case-swapped letters.</returns>
    public static string SwapCase(this string value)
        => new(value.Select(c => c.IsLetter() ? (c.IsUpperCase() ? c.ToLowerCase() : c.ToUpperCase()) : c).ToArray());

    /// <summary>
    /// Converts the string representation of an enum to its corresponding enum value, or returns a default value if parsing fails.
    /// </summary>
    /// <typeparam name="T">The type of the enum.</typeparam>
    /// <param name="value">The string representation of the enum.</param>
    /// <param name="defaultValue">The default value to return if parsing fails.</param>
    /// <returns>The parsed enum value or the default value if parsing fails.</returns>
    public static T ToEnumOrDefault<T>(this string value, T defaultValue) where T : struct
    {
        if (Enum.TryParse(value, true, out T result))
            return result;
        return defaultValue;
    }

    /// <summary>
    /// Determines whether a string is a valid Guid string.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string is a valid Guid string; otherwise, false.</returns>
    public static bool IsGuidString(this string value)
        => Guid.TryParse(value, out _);

    /// <summary>
    /// Converts a string to a Guid.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The Guid representation of the string.</returns>
    public static Guid ToGuid(this string value)
      => value.IsGuidString() ? Guid.Parse(value) : throw new ArgumentException("The value is not a valid Guid string.");

    /// <summary>
    /// Determines whether a string is a valid IP address.
    /// </summary>
    /// <param name="ipAddressString">The string to check.</param>
    /// <returns>true if the string is a valid IP address; otherwise, false.</returns>
    public static bool IsValidIPAddress(this string ipAddressString)
        => IPAddress.TryParse(ipAddressString, out _);

    /// <summary>
    /// Determines whether a string is a valid IPv4 address.
    /// </summary>
    /// <param name="ipAddressString">The string to check.</param>
    /// <returns>true if the string is a valid IPv4 address; otherwise, false.</returns>
    public static bool IsValidIPv4(this string ipAddressString)
        => IPAddress.TryParse(ipAddressString, out IPAddress? ipAddress) && ipAddress != null && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;

    /// <summary>
    /// Determines whether a string is a valid IPv6 address.
    /// </summary>
    /// <param name="ipAddressString">The string to check.</param>
    /// <returns>true if the string is a valid IPv6 address; otherwise, false.</returns>
    public static bool IsValidIPv6(this string ipAddressString)
        => IPAddress.TryParse(ipAddressString, out IPAddress? ipAddress) && ipAddress != null && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6;
}
