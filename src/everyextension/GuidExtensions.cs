namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Guids.
/// </summary>
public static class GuidExtensions
{
    /// <summary>
    /// Checks if the Guid is empty.
    /// </summary>
    /// <param name="guid">The Guid object.</param>
    /// <returns>True if the Guid is empty; otherwise, false.</returns>
    public static bool IsEmpty(this Guid guid)
        => guid == Guid.Empty;

    /// <summary>
    /// Converts the Guid to a Base64 string.
    /// </summary>
    /// <param name="guid">The Guid object.</param>
    /// <returns>The Base64 representation of the Guid.</returns>
    public static string ToBase64(this Guid guid)
    {
        var guidBytes = guid.ToByteArray();
        return Convert.ToBase64String(guidBytes);
    }

    /// <summary>
    /// Converts the Guid to a byte array.
    /// </summary>
    /// <param name="guid">The Guid object.</param>
    /// <returns>The byte array representation of the Guid.</returns>
    public static byte[] ToByteArray(this Guid guid)
        => guid.ToByteArray();

    /// <summary>
    /// Combines two Guids using bitwise XOR operation.
    /// </summary>
    /// <param name="guid1">The first Guid.</param>
    /// <param name="guid2">The second Guid.</param>
    /// <returns>The combined Guid using bitwise XOR operation.</returns>
    public static Guid CombineGuids(this Guid guid1, Guid guid2)
    {
        var guidBytes1 = guid1.ToByteArray();
        var guidBytes2 = guid2.ToByteArray();

        for (int i = 0; i < 8; i++)
            guidBytes1[i] ^= guidBytes2[i];

        return guidBytes1.ToGuid();
    }
}
