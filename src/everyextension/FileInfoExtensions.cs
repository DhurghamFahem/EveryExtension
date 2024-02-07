using System.Security.Cryptography;

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
}
