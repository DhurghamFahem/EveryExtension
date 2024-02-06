using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Text;

namespace EveryExtension;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);

    public static bool IsNullOrEmptyOrWhiteSpace(this string value)
        => string.IsNullOrEmpty(value) || value.IsNullOrWhiteSpace();

    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;
        return value[..maxLength] + "...";
    }

    public static bool IsEmail(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return false;
        return IPAddress.TryParse(value, out _);
    }

    public static string ToTitleCase(this string value)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
    }

    public static bool IsNumeric(this string value)
    {
        return decimal.TryParse(value, out _);
    }

    public static string RemoveWhitespace(this string value)
    {
        return new string(value.ToCharArray()
            .Where(c => !c.IsWhiteSpace())
            .ToArray());
    }

    public static bool IsUrl(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Uri.TryCreate(value, UriKind.Absolute, out _);
    }

    public static bool IsJson(this string value)
    {
        try
        {
            JToken.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static int WordCount(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return 0;
        return value.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
    }

    public static bool IsAlphaNumeric(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        return value.All(c => c.IsAlphanumeric());
    }

    public static string Base64Encode(this string value)
    {
        var bytes = Encoding.UTF8.GetBytes(value);
        return Convert.ToBase64String(bytes);
    }

    public static string Base64Decode(this string value)
    {
        var bytes = Convert.FromBase64String(value);
        return Encoding.UTF8.GetString(bytes);
    }

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

    public static string ToCamelCase(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return value;
        return value[0].ToLowerCase() + value.Substring(1);
    }

    public static string ToSnakeCase(this string value)
    {
        if (value.IsNullOrWhiteSpace())
            return value;
        return string.Concat(value.Select((x, i) =>
            i > 0 && x.IsUpperCase() ? "_" + x.ToString() : x.ToString())).ToLower();
    }

    public static string SubstringBetween(this string value, string startMarker, string endMarker)
    {
        var startIndex = value.IndexOf(startMarker);
        var endIndex = value.LastIndexOf(endMarker);
        if (startIndex < 0 || endIndex < 0 || startIndex >= endIndex)
            return string.Empty;
        return value.Substring(startIndex + startMarker.Length, endIndex - startIndex - startMarker.Length);
    }

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

    public static string SwapCase(this string value)
        => new(value.Select(c => c.IsLetter() ? (c.IsUpperCase() ? c.ToLowerCase() : c.ToUpperCase()) : c).ToArray());
}
