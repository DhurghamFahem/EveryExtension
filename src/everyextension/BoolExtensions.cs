namespace EveryExtension;

/// <summary>
/// Extension methods for bool type.
/// </summary>
public static class BoolExtensions
{
    /// <summary>
    /// Converts a boolean value to a string representation of "Yes" if the boolean is true,
    /// and "No" if the boolean is false.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>A string representing "Yes" or "No" based on the boolean value.</returns>
    public static string ToYesNoString(this bool value)
        => value ? "Yes" : "No";

    /// <summary>
    /// Converts a boolean value to a binary string representation, where true is "1" and false is "0".
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>A string representing "1" for true or "0" for false.</returns>
    public static string ToBinaryString(this bool value)
        => value ? "1" : "0";

    /// <summary>
    /// Converts a boolean value to a bit representation, where true is 1 and false is 0.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>An integer representing 1 for true or 0 for false.</returns>
    public static int ToBit(this bool value)
        => value ? 1 : 0;

    /// <summary>
    /// Toggles the boolean value, changing true to false and false to true.
    /// </summary>
    /// <param name="value">The boolean value to toggle.</param>
    /// <returns>The toggled boolean value.</returns>
    public static bool Toggle(this bool value)
        => !value;

    /// <summary>
    /// Toggles the boolean value only if a specified condition is true.
    /// </summary>
    /// <param name="value">The boolean value to toggle.</param>
    /// <param name="condition">The condition that determines whether to toggle the value.</param>
    /// <returns>The toggled boolean value if the condition is true; otherwise, the original value.</returns>
    public static bool ToggleIf(this bool value, bool condition)
        => condition ? !value : value;

    /// <summary>
    /// Executes an action if the boolean value is true.
    /// </summary>
    /// <param name="value">The boolean value to check.</param>
    /// <param name="action">The action to execute if the boolean value is true.</param>
    public static void IfTrue(this bool value, Action action)
    {
        if (value)
            action();
    }

    /// <summary>
    /// Executes different actions based on whether the boolean value is true or false.
    /// </summary>
    /// <param name="value">The boolean value to check.</param>
    /// <param name="trueAction">The action to execute if the boolean value is true.</param>
    /// <param name="falseAction">The action to execute if the boolean value is false.</param>
    public static void IfTrue(this bool value, Action trueAction, Action falseAction)
    {
        if (value)
            trueAction();
        else falseAction();
    }

    /// <summary>
    /// Invokes different functions based on whether the boolean value is true or false and returns the result.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="value">The boolean value to check.</param>
    /// <param name="trueFunc">The function to invoke if the boolean value is true.</param>
    /// <param name="falseFunc">The function to invoke if the boolean value is false.</param>
    /// <returns>The result of the invoked function.</returns>
    public static T IfTrue<T>(this bool value, Func<T> trueFunc, Func<T> falseFunc)
    {
        return value ? trueFunc() : falseFunc();
    }

    /// <summary>
    /// Performs a logical AND operation between the original boolean value and an array of boolean values.
    /// </summary>
    /// <param name="value">The original boolean value.</param>
    /// <param name="values">An array of boolean values to perform the AND operation with.</param>
    /// <returns>The result of the logical AND operation.</returns>
    public static bool And(this bool value, params bool[] values)
    {
        foreach (var v in values)
            value = value && v;
        return value;
    }

    /// <summary>
    /// Performs a logical OR operation between the original boolean value and an array of boolean values.
    /// </summary>
    /// <param name="value">The original boolean value.</param>
    /// <param name="values">An array of boolean values to perform the OR operation with.</param>
    /// <returns>The result of the logical OR operation.</returns>
    public static bool Or(this bool value, params bool[] values)
    {
        foreach (var v in values)
            value = value || v;
        return value;
    }

    /// <summary>
    /// Performs a logical XOR (exclusive OR) operation between two boolean values.
    /// </summary>
    /// <param name="value1">The first boolean value.</param>
    /// <param name="value2">The second boolean value.</param>
    /// <returns>The result of the logical XOR operation.</returns>
    public static bool Xor(this bool value1, bool value2)
        => value1 ^ value2;

    /// <summary>
    /// Converts a boolean value to a custom string representation, using specified strings for true and false values.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <param name="trueString">The string representation for true.</param>
    /// <param name="falseString">The string representation for false.</param>
    /// <returns>The custom string representation based on the boolean value.</returns>
    public static string ToString(this bool value, string trueString, string falseString)
        => value ? trueString : falseString;

    /// <summary>
    /// Checks if a nullable boolean value is either null or false.
    /// </summary>
    /// <param name="value">The nullable boolean value to check.</param>
    /// <returns>True if the value is null or false; otherwise, false.</returns>
    public static bool IsNullOrFalse(this bool? value)
        => value == null || value == false;

    /// <summary>
    /// Converts a boolean value to an emoji representation, using "✅" for true and "❌" for false.
    /// </summary>
    /// <param name="value">The boolean value to convert.</param>
    /// <returns>The emoji representation based on the boolean value.</returns>
    public static string ToEmoji(this bool value)
        => value ? "✅" : "❌";

    /// <summary>
    /// Toggles the boolean value only if it is initially true.
    /// </summary>
    /// <param name="value">The boolean value to toggle.</param>
    /// <returns>The toggled boolean value if it is initially true; otherwise, the original value.</returns>
    public static bool ToggleIfTrue(this bool value)
        => value ? !value : value;

    /// <summary>
    /// Toggles the boolean value only if it is initially false.
    /// </summary>
    /// <param name="value">The boolean value to toggle.</param>
    /// <returns>The toggled boolean value if it is initially false; otherwise, the original value.</returns>
    public static bool ToggleIfFalse(this bool value)
        => !value ? !value : value;

    /// <summary>
    /// Checks if the boolean value is the same as another boolean value.
    /// </summary>
    /// <param name="value">The boolean value to compare.</param>
    /// <param name="other">The other boolean value to compare with.</param>
    /// <returns>True if the values are the same; otherwise, false.</returns>
    public static bool IsSameAs(this bool value, bool other)
        => value == other;

    /// <summary>
    /// Checks if two boolean values are distinct (not equal).
    /// </summary>
    /// <param name="value1">The first boolean value to compare.</param>
    /// <param name="value2">The second boolean value to compare.</param>
    /// <returns>True if the values are distinct; otherwise, false.</returns>
    public static bool IsDistinct(this bool value1, bool value2)
        => value1 != value2;
}
