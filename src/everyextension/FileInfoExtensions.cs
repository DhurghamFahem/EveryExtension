using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace EveryExtension;

/// <summary>
/// Provides extension methods for <see cref="FileInfo"/>.
/// </summary>
public static class FileInfoExtensions
{
    /// <summary>
    /// Computes the MD5 hash of the file content.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The MD5 hash of the file content as a string.</returns>
    public static string GetFileHash(this FileInfo fileInfo)
    {
        using var md5 = MD5.Create();
        using var stream = fileInfo.OpenRead();
        var hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    /// <summary>
    /// Reads all lines from the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>An array of lines from the file.</returns>
    public static string[] ReadAllLines(this FileInfo fileInfo)
        => File.ReadAllLines(fileInfo.FullName);

    /// <summary>
    /// Reads all text from the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The content of the file as a string.</returns>
    public static string ReadAllText(this FileInfo fileInfo)
        => File.ReadAllText(fileInfo.FullName);

    /// <summary>
    /// Writes the specified content to the file, replacing the existing content.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="content">The content to write to the file.</param>
    public static void WriteAllText(this FileInfo fileInfo, string content)
        => File.WriteAllText(fileInfo.FullName, content);

    /// <summary>
    /// Checks if the file is hidden.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>True if the file is hidden; otherwise, false.</returns>
    public static bool IsHidden(this FileInfo fileInfo)
        => (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

    /// <summary>
    /// Changes the file extension of the current file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="newExtension">The new file extension.</param>
    /// <returns>A new <see cref="FileInfo"/> instance with the modified extension.</returns>
    public static FileInfo ChangeFileExtension(this FileInfo fileInfo, string newExtension)
    {
        var newFilePath = Path.ChangeExtension(fileInfo.FullName, newExtension);
        return new FileInfo(newFilePath);
    }

    /// <summary>
    /// Checks if the file is a directory.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>True if the file is a directory; otherwise, false.</returns>
    public static bool IsDirectory(this FileInfo fileInfo)
        => (fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;

    /// <summary>
    /// Gets the name of the directory containing the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The name of the directory, or null if the directory is not available.</returns>
    public static string? GetDirectoryName(this FileInfo fileInfo)
        => fileInfo.Directory?.Name;

    /// <summary>
    /// Appends the specified content to the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="content">The content to append to the file.</param>
    public static void AppendText(this FileInfo fileInfo, string content)
        => File.AppendAllText(fileInfo.FullName, content);

    /// <summary>
    /// Checks if the file is empty.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>True if the file is empty; otherwise, false.</returns>
    public static bool IsEmpty(this FileInfo fileInfo)
        => fileInfo.Length == 0;

    /// <summary>
    /// Renames the file with the specified new file name.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="newFileName">The new name for the file.</param>
    public static void Rename(this FileInfo fileInfo, string newFileName)
    {
        var directoryName = fileInfo.GetDirectoryName();
        if (directoryName!.IsNull())
            return;
        var newPath = Path.Combine(directoryName!, newFileName);
        fileInfo.MoveTo(newPath);
    }

    /// <summary>
    /// Deletes the file if it exists.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    public static void DeleteIfExists(this FileInfo fileInfo)
    {
        if (fileInfo.Exists)
            fileInfo.Delete();
    }

    /// <summary>
    /// Compresses the file and writes the compressed data to the specified file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="compressedFilePath">The path of the compressed file to create.</param>
    public static void Compress(this FileInfo fileInfo, string compressedFilePath)
    {
        using FileStream originalFileStream = fileInfo.OpenRead();
        using FileStream compressedFileStream = File.Create(compressedFilePath);
        compressedFileStream.Compress();
    }

    /// <summary>
    /// Decompresses the compressed file and writes the decompressed data to the specified file.
    /// </summary>
    /// <param name="compressedFile">The <see cref="FileInfo"/> instance representing the compressed file.</param>
    /// <param name="decompressedFilePath">The path of the decompressed file to create.</param>
    public static void Decompress(this FileInfo compressedFile, string decompressedFilePath)
    {
        using FileStream compressedFileStream = compressedFile.OpenRead();
        using FileStream decompressedFileStream = File.Create(decompressedFilePath);
        decompressedFileStream.Decompress();
    }

    /// <summary>
    /// Writes the specified byte array to the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="content">The byte array to write to the file.</param>
    public static void WriteBytes(this FileInfo fileInfo, byte[] content)
        => File.WriteAllBytes(fileInfo.FullName, content);

    /// <summary>
    /// Reads all bytes from the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The byte array representing the content of the file.</returns>
    public static byte[] ReadBytes(this FileInfo fileInfo)
        => File.ReadAllBytes(fileInfo.FullName);

    /// <summary>
    /// Reads all lines from the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>An enumerable collection of lines from the file.</returns>
    public static IEnumerable<string> ReadLines(this FileInfo fileInfo)
        => File.ReadLines(fileInfo.FullName);

    /// <summary>
    /// Gets the number of lines in the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The number of lines in the file.</returns>
    public static int GetLinesCount(this FileInfo fileInfo)
        => File.ReadLines(fileInfo.FullName).Count();

    /// <summary>
    /// Asynchronously reads the entire file as a string.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the content of the file as a string.</returns>
    public static async Task<string> ReadAsTextAsync(this FileInfo fileInfo)
    {
        using var reader = new StreamReader(fileInfo.FullName);
        return await reader.ReadToEndAsync();
    }

    /// <summary>
    /// Asynchronously writes the specified content to the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="content">The content to write to the file.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task WriteAsTextAsync(this FileInfo fileInfo, string content)
    {
        using var writer = new StreamWriter(fileInfo.FullName);
        await writer.WriteAsync(content);
    }

    /// <summary>
    /// Gets information about the file, including name, full path, size, read-only status, creation time, and last write time.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>A string containing information about the file.</returns>
    public static string GetFileInformation(this FileInfo fileInfo)
    {
        var info = new StringBuilder();
        info.AppendLine($"Name: {fileInfo.Name}");
        info.AppendLine($"Full Path: {fileInfo.FullName}");
        info.AppendLine($"Size: {fileInfo.Length} bytes");
        info.AppendLine($"Is Read-Only: {fileInfo.IsReadOnly}");
        info.AppendLine($"Creation Time: {fileInfo.CreationTime}");
        info.AppendLine($"Last Write Time: {fileInfo.LastWriteTime}");
        return info.ToString();
    }

    /// <summary>
    /// Gets the MIME type of the file based on its extension.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>The MIME type of the file.</returns>
    public static string GetFileMimeType(this FileInfo fileInfo)
    {
        string extension = fileInfo.Extension.TrimStart('.').ToLower();
        return extension switch
        {
            "txt" => "text/plain",
            "jpg" or "jpeg" => "image/jpeg",
            "png" => "image/png",
            _ => "application/octet-stream",
        };
    }

    /// <summary>
    /// Encrypts the file using the specified key and initialization vector (IV).
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="encryptedFilePath">The path to store the encrypted file.</param>
    /// <param name="key">The key used for encryption.</param>
    /// <param name="iv">The initialization vector (IV) used for encryption.</param>
    public static void Encrypt(this FileInfo fileInfo, string encryptedFilePath, byte[] key, byte[] iv)
    {
        using var inputFileStream = fileInfo.OpenRead();
        using var encryptedFileStream = File.Create(encryptedFilePath);
        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(key, iv);
        using var cryptoStream = new CryptoStream(encryptedFileStream, encryptor, CryptoStreamMode.Write);
        inputFileStream.CopyTo(cryptoStream);
    }

    /// <summary>
    /// Decrypts the encrypted file using the specified key and initialization vector (IV).
    /// </summary>
    /// <param name="encryptedFile">The <see cref="FileInfo"/> instance representing the encrypted file.</param>
    /// <param name="decryptedFilePath">The path to store the decrypted file.</param>
    /// <param name="key">The key used for decryption.</param>
    /// <param name="iv">The initialization vector (IV) used for decryption.</param>
    public static void Decrypt(this FileInfo encryptedFile, string decryptedFilePath, byte[] key, byte[] iv)
    {
        using var encryptedFileStream = encryptedFile.OpenRead();
        using var decryptedFileStream = File.Create(decryptedFilePath);
        using var aesAlg = Aes.Create();
        using var decryptor = aesAlg.CreateDecryptor(key, iv);
        using var cryptoStream = new CryptoStream(encryptedFileStream, decryptor, CryptoStreamMode.Read);
        cryptoStream.CopyTo(decryptedFileStream);
    }

    /// <summary>
    /// Ensures that the directory containing the file exists. If not, it creates the directory.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    public static void EnsureDirectoryExists(this FileInfo fileInfo)
    {
        var directory = fileInfo.Directory!;
        if (!directory.Exists)
            directory.Create();
    }

    /// <summary>
    /// Creates a ZIP archive containing the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="zipFilePath">The path to store the ZIP archive.</param>
    public static void Zip(this FileInfo fileInfo, string zipFilePath)
    {
        using var originalFileStream = fileInfo.OpenRead();
        using var compressedFileStream = File.Create(zipFilePath);
        using var archive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create);
        var entry = archive.CreateEntry(fileInfo.Name);
        using var entryStream = entry.Open();
        originalFileStream.CopyTo(entryStream);
    }

    /// <summary>
    /// Reads the content of the file and deserializes it as an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object to deserialize.</typeparam>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <returns>An object of type <typeparamref name="T"/> deserialized from the file content.</returns>
    public static T? ReadJsonAsObject<T>(this FileInfo fileInfo)
    {
        var content = fileInfo.ReadAllText();
        return JsonSerializer.Deserialize<T>(content);
    }

    /// <summary>
    /// Serializes the specified object as JSON and writes it to the file.
    /// </summary>
    /// <param name="fileInfo">The <see cref="FileInfo"/> instance.</param>
    /// <param name="obj">The object to serialize and write to the file.</param>
    public static void WriteObjectAsJson(this FileInfo fileInfo, object obj)
    {
        var json = obj.ToJson();
        fileInfo.WriteAllText(json);
    }
}
