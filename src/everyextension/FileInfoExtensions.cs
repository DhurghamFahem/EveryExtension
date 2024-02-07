using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;

namespace EveryExtension;

public static class FileInfoExtensions
{
    public static string GetFileHash(this FileInfo fileInfo)
    {
        using var md5 = MD5.Create();
        using var stream = fileInfo.OpenRead();
        var hashBytes = md5.ComputeHash(stream);
        return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
    }

    public static string[] ReadAllLines(this FileInfo fileInfo)
        => File.ReadAllLines(fileInfo.FullName);

    public static string ReadAllText(this FileInfo fileInfo)
        => File.ReadAllText(fileInfo.FullName);

    public static void WriteAllText(this FileInfo fileInfo, string content)
        => File.WriteAllText(fileInfo.FullName, content);

    public static bool IsHidden(this FileInfo fileInfo)
        => (fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;

    public static FileInfo ChangeFileExtension(this FileInfo fileInfo, string newExtension)
    {
        var newFilePath = Path.ChangeExtension(fileInfo.FullName, newExtension);
        return new FileInfo(newFilePath);
    }

    public static bool IsDirectory(this FileInfo fileInfo)
        => (fileInfo.Attributes & FileAttributes.Directory) == FileAttributes.Directory;

    public static string? GetDirectoryName(this FileInfo fileInfo)
        => fileInfo.Directory?.Name;

    public static void AppendText(this FileInfo fileInfo, string content)
        => File.AppendAllText(fileInfo.FullName, content);

    public static bool IsEmpty(this FileInfo fileInfo)
        => fileInfo.Length == 0;

    public static void Rename(this FileInfo fileInfo, string newFileName)
    {
        var directoryName = fileInfo.GetDirectoryName();
        if (directoryName!.IsNull())
            return;
        var newPath = Path.Combine(directoryName!, newFileName);
        fileInfo.MoveTo(newPath);
    }

    public static void DeleteIfExists(this FileInfo fileInfo)
    {
        if (fileInfo.Exists)
            fileInfo.Delete();
    }

    public static void Compress(this FileInfo fileInfo, string compressedFilePath)
    {
        using FileStream originalFileStream = fileInfo.OpenRead();
        using FileStream compressedFileStream = File.Create(compressedFilePath);
        compressedFileStream.Compress();
    }

    public static void Decompress(this FileInfo compressedFile, string decompressedFilePath)
    {
        using FileStream compressedFileStream = compressedFile.OpenRead();
        using FileStream decompressedFileStream = File.Create(decompressedFilePath);
        decompressedFileStream.Decompress();
    }

    public static void WriteBytes(this FileInfo fileInfo, byte[] content)
        => File.WriteAllBytes(fileInfo.FullName, content);

    public static byte[] ReadBytes(this FileInfo fileInfo)
        => File.ReadAllBytes(fileInfo.FullName);

    public static IEnumerable<string> ReadLines(this FileInfo fileInfo)
        => File.ReadLines(fileInfo.FullName);

    public static int GetLinesCount(this FileInfo fileInfo)
        => File.ReadLines(fileInfo.FullName).Count();

    public static async Task<string> ReadAsTextAsync(this FileInfo fileInfo)
    {
        using var reader = new StreamReader(fileInfo.FullName);
        return await reader.ReadToEndAsync();
    }

    public static async Task WriteAsTextAsync(this FileInfo fileInfo, string content)
    {
        using var writer = new StreamWriter(fileInfo.FullName);
        await writer.WriteAsync(content);
    }

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

    public static void Encrypt(this FileInfo fileInfo, string encryptedFilePath, byte[] key, byte[] iv)
    {
        using var inputFileStream = fileInfo.OpenRead();
        using var encryptedFileStream = File.Create(encryptedFilePath);
        using var aesAlg = Aes.Create();
        using var encryptor = aesAlg.CreateEncryptor(key, iv);
        using var cryptoStream = new CryptoStream(encryptedFileStream, encryptor, CryptoStreamMode.Write);
        inputFileStream.CopyTo(cryptoStream);
    }

    public static void Decrypt(this FileInfo encryptedFile, string decryptedFilePath, byte[] key, byte[] iv)
    {
        using var encryptedFileStream = encryptedFile.OpenRead();
        using var decryptedFileStream = File.Create(decryptedFilePath);
        using var aesAlg = Aes.Create();
        using var decryptor = aesAlg.CreateDecryptor(key, iv);
        using var cryptoStream = new CryptoStream(encryptedFileStream, decryptor, CryptoStreamMode.Read);
        cryptoStream.CopyTo(decryptedFileStream);
    }

    public static void EnsureDirectoryExists(this FileInfo fileInfo)
    {
        var directory = fileInfo.Directory!;
        if (!directory.Exists)
            directory.Create();
    }

    public static void Zip(this FileInfo fileInfo, string zipFilePath)
    {
        using var originalFileStream = fileInfo.OpenRead();
        using var compressedFileStream = File.Create(zipFilePath);
        using var archive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create);
        var entry = archive.CreateEntry(fileInfo.Name);
        using var entryStream = entry.Open();
        originalFileStream.CopyTo(entryStream);
    }
}
