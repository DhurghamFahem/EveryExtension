using System.ComponentModel;

namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Enums.
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    /// Gets the description attribute value of an Enum.
    /// </summary>
    /// <param name="value">The Enum value.</param>
    /// <returns>The description attribute value if present; otherwise, the Enum's string representation.</returns>
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString())!;
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
        return attribute == null ? value.ToString() : attribute.Description;
    }

    /// <summary>
    /// Checks if an Enum has a specified flag.
    /// </summary>
    /// <typeparam name="TEnum">The Enum type.</typeparam>
    /// <param name="value">The Enum value.</param>
    /// <param name="flag">The flag to check.</param>
    /// <returns>True if the Enum has the specified flag; otherwise, false.</returns>
    public static bool HasFlag<TEnum>(this Enum value, TEnum flag) where TEnum : Enum
    {
        var flagValue = Convert.ToInt64(flag);
        var enumValue = Convert.ToInt64(value);
        return (enumValue & flagValue) == flagValue;
    }

    /// <summary>
    /// Gets a specified attribute from an Enum.
    /// </summary>
    /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
    /// <param name="value">The Enum value.</param>
    /// <returns>The specified attribute if present; otherwise, null.</returns>
    public static TAttribute GetEnumAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        return (TAttribute)Attribute.GetCustomAttribute(fieldInfo!, typeof(TAttribute))!;
    }
}
