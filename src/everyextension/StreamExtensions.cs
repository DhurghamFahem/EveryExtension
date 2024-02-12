using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace EveryExtension;

/// <summary>
/// Provides extension methods for working with Streams.
/// </summary>
public static class StreamExtensions
{
    /// <summary>
    /// Reads the entire content of the stream as a string using UTF-8 encoding.
    /// </summary>
    /// <param name="stream">The Stream object.</param>
    /// <returns>The content of the stream as a string.</returns>
    public static string ReadToEnd(this Stream stream)
    {
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return reader.ReadToEnd();
    }

    /// <summary>
    /// Seeks to the specified offset in the stream and reads a specified number of bytes.
    /// </summary>
    /// <param name="stream">The Stream object.</param>
    /// <param name="offset">The offset to seek in the stream.</param>
    /// <param name="count">The number of bytes to read.</param>
    /// <returns>The read bytes as a byte array.</returns>
    public static byte[] SeekAndRead(this Stream stream, long offset, int count)
    {
        stream.Seek(offset, SeekOrigin.Begin);
        byte[] buffer = new byte[count];
        stream.Read(buffer, 0, count);
        return buffer;
    }

    /// <summary>
    /// Writes a byte array to the stream asynchronously.
    /// </summary>
    /// <param name="stream">The Stream object.</param>
    /// <param name="data">The byte array to write to the stream.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task WriteToStreamAsync(this Stream stream, byte[] data)
        => await stream.WriteAsync(data, 0, data.Length);

    /// <summary>
    /// Compresses the content of the stream using GZip compression.
    /// </summary>
    /// <param name="input">The Stream object containing the content to compress.</param>
    /// <returns>The compressed stream.</returns>
    public static Stream Compress(this Stream input)
    {
        using var compressedStream = new MemoryStream();
        using var gzipStream = new GZipStream(compressedStream, CompressionMode.Compress);
        input.CopyTo(gzipStream);
        compressedStream.Position = 0;
        return compressedStream;
    }

    /// <summary>
    /// Decompresses the content of the stream using GZip decompression.
    /// </summary>
    /// <param name="input">The Stream object containing the content to decompress.</param>
    /// <returns>The decompressed stream.</returns>
    public static Stream Decompress(this Stream input)
    {
        using var decompressedStream = new MemoryStream();
        using var gzipStream = new GZipStream(input, CompressionMode.Decompress);
        gzipStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;
        return decompressedStream;
    }

    /// <summary>
    /// Appends the content of the stream to a file asynchronously.
    /// </summary>
    /// <param name="stream">The Stream object containing the content to append to the file.</param>
    /// <param name="filePath">The path to the file where the content will be appended.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public static async Task AppendToFileAsync(this Stream stream, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write);
        await stream.CopyToAsync(fileStream);
    }

    /// <summary>
    /// Encrypts the content of the stream using the specified symmetric algorithm, key, and IV.
    /// </summary>
    /// <param name="input">The Stream object containing the content to encrypt.</param>
    /// <param name="algorithm">The symmetric algorithm for encryption.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="iv">The initialization vector (IV) for encryption.</param>
    /// <returns>The encrypted stream.</returns>
    public static Stream Encrypt(this Stream input, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
    {
        var encryptor = algorithm.CreateEncryptor(key, iv);
        return new CryptoStream(input, encryptor, CryptoStreamMode.Read);
    }

    /// <summary>
    /// Decrypts the content of the stream using the specified symmetric algorithm, key, and IV.
    /// </summary>
    /// <param name="input">The Stream object containing the content to decrypt.</param>
    /// <param name="algorithm">The symmetric algorithm for decryption.</param>
    /// <param name="key">The decryption key.</param>
    /// <param name="iv">The initialization vector (IV) for decryption.</param>
    /// <returns>The decrypted stream.</returns>
    public static Stream Decrypt(this Stream input, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
    {
        var decryptor = algorithm.CreateDecryptor(key, iv);
        return new CryptoStream(input, decryptor, CryptoStreamMode.Read);
    }
}
