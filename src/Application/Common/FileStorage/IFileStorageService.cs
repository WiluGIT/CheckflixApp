using CheckflixApp.Domain.Common;
using Microsoft.AspNetCore.Http;

namespace CheckflixApp.Application.Common.FileStorage;
public interface IFileStorageService
{
    public Task<string> UploadAsync<T>(IFormFile? formFile, FileType supportedFileType, CancellationToken cancellationToken = default)
        where T : class;

    public void Remove(string? path);
}