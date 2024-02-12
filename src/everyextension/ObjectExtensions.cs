using System.Reflection;
using System.Text.Json;

namespace EveryExtension;

/// <summary>
/// Extension methods for working with objects.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Checks if the object is null.
    /// </summary>
    /// <param name="obj">The object to check for null.</param>
    /// <returns>True if the object is null; otherwise, false.</returns>
    public static bool IsNull(this object obj)
        => obj == null;

    /// <summary>
    /// Checks if the object is not null.
    /// </summary>
    /// <param name="obj">The object to check for non-null.</param>
    /// <returns>True if the object is not null; otherwise, false.</returns>
    public static bool IsNotNull(this object obj)
        => obj != null;

    /// <summary>
    /// Converts the object to a string or returns a default value if null.
    /// </summary>
    /// <param name="defaultValue">Default value to return if the object is null.</param>
    /// <returns>String representation of the object or the default value if null.</returns>
    public static string ToStringOrDefault(this object obj, string defaultValue = "")
        => obj?.ToString() ?? defaultValue;

    /// <summary>
    /// Checks if the object is in a collection.
    /// </summary>
    /// <typeparam name="T">Type of the object and the collection elements.</typeparam>
    /// <param name="collection">Collection to check against.</param>
    /// <returns>True if the object is in the collection; otherwise, false.</returns>
    public static bool In<T>(this T obj, params T[] collection)
        => collection.Contains(obj);

    /// <summary>
    /// Converts the object to JSON.
    /// </summary>
    /// <returns>JSON representation of the object.</returns>
    public static string ToJson(this object obj)
        => JsonSerializer.Serialize(obj);

    /// <summary>
    /// Creates a deep copy of a nullable object using JSON serialization.
    /// </summary>
    /// <typeparam name="T">Type of the object.</typeparam>
    /// <param name="obj">The nullable object to copy.</param>
    /// <returns>A deep copy of the nullable object.</returns>
    public static T? Copy<T>(this T? obj)
    {
        if (obj == null)
            return default;
        return JsonSerializer.Deserialize<T?>(obj.ToJson());
    }

    /// <summary>
    /// Converts an object to a dictionary of property names and values.
    /// </summary>
    /// <param name="obj">The object to convert to a dictionary.</param>
    /// <returns>A dictionary of property names and values.</returns>
    public static Dictionary<string, object?> ToDictionary(this object obj)
        => obj.GetType()
               .GetProperties(BindingFlags.Instance | BindingFlags.Public)
               .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj));

    /// <summary>
    /// Gets a description of the object's type, including generic arguments.
    /// </summary>
    /// <param name="obj">The object whose type description is needed.</param>
    /// <returns>A string representing the object's type description.</returns>
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

    /// <summary>
    /// Safely casts an object to a specified type.
    /// </summary>
    /// <typeparam name="T">The type to cast the object to.</typeparam>
    /// <param name="obj">The object to cast.</param>
    /// <returns>The object cast to the specified type, or null if the cast fails.</returns>
    public static T? SafeCast<T>(this object obj) where T : class
        => obj as T;

    /// <summary>
    /// Throws an exception if the object is null.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to check for null.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <returns>The non-null object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the object is null.</exception>
    public static T ThrowIfNull<T>(this T obj, string paramName) where T : class
    {
        if (obj == null)
            throw new ArgumentNullException(paramName);
        return obj;
    }

    /// <summary>
    /// Performs an action if the object is not null.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object on which to perform the action.</param>
    /// <param name="action">The action to perform.</param>
    public static void IfNotNull<T>(this T obj, Action<T> action) where T : class
    {
        if (obj != null)
            action(obj);
    }

    /// <summary>
    /// Returns a default value if the object is null.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to check for null.</param>
    /// <param name="defaultValue">The default value to return if the object is null.</param>
    /// <returns>The object or the default value if the object is null.</returns>
    public static T DefaultValueIfNull<T>(this T obj, T defaultValue)
        => obj ?? defaultValue;

    /// <summary>
    /// Invokes a method on the object by name with specified parameters.
    /// </summary>
    /// <param name="obj">The object on which to invoke the method.</param>
    /// <param name="methodName">The name of the method to invoke.</param>
    /// <param name="parameters">The parameters to pass to the method.</param>
    /// <returns>The result of invoking the method, or the original object if the method is not found.</returns>
    public static object? InvokeMethod(this object obj, string methodName, params object[] parameters)
    {
        var method = obj.GetType().GetMethod(methodName);
        if (method == null)
            return obj;
        return method?.Invoke(obj, parameters);
    }

    /// <summary>
    /// Checks if the object has the default value.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to check.</param>
    /// <returns>True if the object has the default value; otherwise, false.</returns>
    public static bool IsDefault<T>(this T obj)
        => EqualityComparer<T>.Default.Equals(obj, default);

    /// <summary>
    /// Copies properties from one object to another of the same type.
    /// </summary>
    /// <typeparam name="T">The type of the objects.</typeparam>
    /// <param name="source">The source object from which to copy properties.</param>
    /// <param name="destination">The destination object to which to copy properties.</param>
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

    /// <summary>
    /// Wraps an object in an enumerable.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to wrap in an enumerable.</param>
    /// <returns>An enumerable containing the object.</returns>
    public static IEnumerable<T> WrapInEnumerable<T>(this T obj)
    {
        yield return obj;
    }

    /// <summary>
    /// Converts an object to a list.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="obj">The object to convert to a list.</param>
    /// <returns>A list containing the object.</returns>
    public static List<T> AsList<T>(this T obj)
    {
        if (obj is IEnumerable<T> enumerable)
            return enumerable.ToList();
        return new List<T> { obj };
    }

    /// <summary>
    /// Checks if the object is a primitive type.
    /// </summary>
    /// <param name="obj">The object to check.</param>
    /// <returns>True if the object is a primitive type; otherwise, false.</returns>
    public static bool IsPrimitive(this object obj)
        => obj != null && obj.GetType().IsPrimitive;

    /// <summary>
    /// Converts a non-nullable object to a nullable object.
    /// </summary>
    /// <param name="value">The non-nullable object to convert.</param>
    /// <returns>The nullable version of the object.</returns>
    public static object? AsNullable(this object value)
        => value;
}
