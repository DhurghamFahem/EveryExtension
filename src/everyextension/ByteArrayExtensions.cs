namespace EveryExtension;

public static class ByteArrayExtensions
{
    public static Guid ToGuid(this byte[] bytes)
      => new(bytes);

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
