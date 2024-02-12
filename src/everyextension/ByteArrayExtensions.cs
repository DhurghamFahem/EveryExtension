namespace EveryExtension;

/// <summary>
/// Extension methods for byte[] type.
/// </summary>
public static class ByteArrayExtensions
{
    /// <summary>
    /// Converts a byte array to a Guid.
    /// </summary>
    /// <param name="bytes">The byte array to convert.</param>
    /// <returns>A Guid created from the byte array.</returns>
    public static Guid ToGuid(this byte[] bytes)
      => new(bytes);

    /// <summary>
    /// Converts a byte array to an array of Guids.
    /// </summary>
    /// <param name="byteArray">The byte array to convert.</param>
    /// <returns>An array of Guids created from the byte array.</returns>
    /// <exception cref="ArgumentException">Thrown if the byte array length is not a multiple of 16.</exception>
    public static Guid[] ToGuidArray(this byte[] byteArray)
    {
        if (byteArray.Length % 16 != 0)
            throw new ArgumentException("The byte array length must be a multiple of 16.", nameof(byteArray));
        var result = new Guid[byteArray.Length / 16];
        for (int i = 0; i < result.Length; i++)
        {
            var guidBytes = new byte[16];
            Array.Copy(byteArray, i * 16, guidBytes, 0, 16);
            result[i] = new Guid(guidBytes);
        }
        return result;
    }
}
