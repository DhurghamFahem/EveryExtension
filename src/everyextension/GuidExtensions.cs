namespace EveryExtension;

public static class GuidExtensions
{
    public static bool IsEmpty(this Guid guid)
        => guid == Guid.Empty;

    public static string ToBase64(this Guid guid)
    {
        var guidBytes = guid.ToByteArray();
        return Convert.ToBase64String(guidBytes);
    }

    public static byte[] ToByteArray(this Guid guid)
        => guid.ToByteArray();

    public static Guid CombineGuids(this Guid guid1, Guid guid2)
    {
        var guidBytes1 = guid1.ToByteArray();
        var guidBytes2 = guid2.ToByteArray();
        for (int i = 0; i < 8; i++)
            guidBytes1[i] ^= guidBytes2[i];
        return guidBytes1.ToGuid();
    }
}
