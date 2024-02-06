using System.ComponentModel;

namespace EveryExtension;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var field = value.GetType().GetField(value.ToString())!;
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute))!;
        return attribute == null ? value.ToString() : attribute.Description;
    }

    public static bool HasFlag<TEnum>(this Enum value, TEnum flag) where TEnum : Enum
    {
        var flagValue = Convert.ToInt64(flag);
        var enumValue = Convert.ToInt64(value);
        return (enumValue & flagValue) == flagValue;
    }

    public static TAttribute GetEnumAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        return (TAttribute)Attribute.GetCustomAttribute(fieldInfo!, typeof(TAttribute))!;
    }
}
