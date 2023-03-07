using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using CheckflixApp.Application.Common.FileStorage;
using CheckflixApp.Domain.Common;
using CheckflixApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Infrastructure.FileStorage;
public class LocalFileStorageService : IFileStorageService
{
    public async Task<string> UploadAsync<T>(IFormFile? formFile, FileType supportedFileType, CancellationToken cancellationToken = default) where T : class
    {
        if (formFile == null || formFile.Length <= 0)
        {
            return string.Empty;
        }

        var extension = Path.GetExtension(formFile.FileName).ToLower();

        if (string.IsNullOrEmpty(formFile.FileName))
            throw new InvalidOperationException("Name is required.");

        if (string.IsNullOrEmpty(extension) || !supportedFileType.GetDescriptionList().Contains(extension))
            throw new InvalidOperationException("File Format Not Supported.");

        string folder = typeof(T).Name;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            folder = folder.Replace(@"\", "/");
        }

        string folderName = supportedFileType switch
        {
            FileType.Image => Path.Combine("Files", "Images", folder),
            _ => Path.Combine("Files", "Others", folder),
        };
        string pathToSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName);
        Directory.CreateDirectory(pathToSave);

        string fileName = formFile.FileName.Trim('"');
        fileName = RemoveSpecialCharacters(fileName);
        fileName = fileName.ReplaceWhitespace("-");
        string fullPath = Path.Combine(pathToSave, fileName);
        string dbPath = Path.Combine(folderName, fileName);
        if (File.Exists(fullPath))
        {
            dbPath = NextAvailableFilename(dbPath);
            fullPath = NextAvailableFilename(fullPath);
        }

        using var stream = new FileStream(fullPath, FileMode.Create);
        await formFile.CopyToAsync(stream, cancellationToken);
        return dbPath.Replace("\\", "/");
    }

    public static string RemoveSpecialCharacters(string str)
    {
        return Regex.Replace(str, "[^a-zA-Z0-9_.]+", string.Empty, RegexOptions.Compiled);
    }

    public void Remove(string? path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    private const string NumberPattern = "-{0}";

    private static string NextAvailableFilename(string path)
    {
        if (!File.Exists(path))
        {
            return path;
        }

        if (Path.HasExtension(path))
        {
            return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path), StringComparison.Ordinal), NumberPattern));
        }

        return GetNextFilename(path + NumberPattern);
    }

    private static string GetNextFilename(string pattern)
    {
        string tmp = string.Format(pattern, 1);

        if (!File.Exists(tmp))
        {
            return tmp;
        }

        int min = 1, max = 2;

        while (File.Exists(string.Format(pattern, max)))
        {
            min = max;
            max *= 2;
        }

        while (max != min + 1)
        {
            int pivot = (max + min) / 2;
            if (File.Exists(string.Format(pattern, pivot)))
            {
                min = pivot;
            }
            else
            {
                max = pivot;
            }
        }

        return string.Format(pattern, max);
    }
}