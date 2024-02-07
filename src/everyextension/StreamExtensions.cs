using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace EveryExtension;

public static class StreamExtensions
{
    public static string ReadToEnd(this Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    public static byte[] SeekAndRead(this Stream stream, long offset, int count)
    {
        stream.Seek(offset, SeekOrigin.Begin);
        byte[] buffer = new byte[count];
        stream.Read(buffer, 0, count);
        return buffer;
    }

    public static async Task WriteToStreamAsync(this Stream stream, byte[] data)
        => await stream.WriteAsync(data, 0, data.Length);

    public static Stream Compress(this Stream input)
    {
        using var compressedStream = new MemoryStream();
        using var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress);
        input.CopyTo(gzipStream);
        compressedStream.Position = 0;
        return compressedStream;
    }

    public static Stream Decompress(this Stream input)
    {
        using var decompressedStream = new MemoryStream();
        using var gzipStream = new GZipStream(input, CompressionMode.Decompress);
        gzipStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;
        return decompressedStream;
    }

    public static async Task AppendToFileAsync(this Stream stream, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
    }

    public static Stream Encrypt(this Stream input, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
    {
        var encryptor = algorithm.CreateEncryptor(key, iv);
        return new CryptoStream(input, encryptor, CryptoStreamMode.Read);
    }

    public static Stream Decrypt(this Stream input, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
    {
        var decryptor = algorithm.CreateDecryptor(key, iv);
        return new CryptoStream(input, decryptor, CryptoStreamMode.Read);
    }
}
