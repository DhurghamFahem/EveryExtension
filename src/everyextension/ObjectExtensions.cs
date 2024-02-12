using System.Reflection;
using System.Text.Json;

namespace EveryExtension;

public static class ObjectExtensions
{
    public static bool IsNull(this object obj)
        => obj == null;

    public static bool IsNotNull(this object obj)
        => obj != null;

    public static string ToStringOrDefault(this object obj, string defaultValue = "")
        => obj?.ToString() ?? defaultValue;

    public static bool In<T>(this T obj, params T[] collection)
        => collection.Contains(obj);

    public static string ToJson(this object obj)
        => JsonSerializer.Serialize(obj);

    public static T? Copy<T>(this T? obj)
    {
        if (obj == null)
            return default;
        return JsonSerializer.Deserialize<T?>(obj.ToJson());
    }

    public static Dictionary<string, object?> ToDictionary(this object obj)
        => obj.GetType()
                   .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                   .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj));

    public static string GetTypeDescription(this object obj)
    {
        var type = obj.GetType();
        var typeName = type.Name;
        if (type.IsGenericType)
        {
            var genericArguments = type.GetGenericArguments().Select(arg => arg.Name);
            typeName = $"{typeName}<{string.Join(", ", genericArguments)}>";
        }
        return typeName;
    }

    public static T? SafeCast<T>(this object obj) where T : class
        => obj as T;

    public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
        return obj;
    }

    public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
    {
        if (obj != null)
            action(obj);
    }

    public static T DefaultValueIfNull<T>(this T obj, T defaultValue)
        => obj ?? defaultValue;

    public static object? InvokeMethod(this object obj, string methodName, params object[] parameters)
    {
        var method = obj.GetType().GetMethod(methodName);
        if (method == null)
            return obj;
        return method?.Invoke(obj, parameters);
    }

    public static bool IsDefault<T>(this T obj)
        => EqualityComparer<T>.Default.Equals(obj, default);

    public static void CopyProperties<T>(this T source, T destination)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            if (property.CanRead && property.CanWrite)
            {
                var value = property.GetValue(source);
                property.SetValue(destination, value);
            }
        }
    }

    public static IEnumerable<T> WrapInEnumerable<T>(this T obj)
    {
        yield return obj;
    }

    public static List<T> AsList<T>(this T obj)
    {
        if (obj is IEnumerable<T> enumerable)
            return enumerable.ToList();
        return [obj];
    }

    public static bool IsPrimitive(this object obj)
        => obj != null && obj.GetType().IsPrimitive;
}
