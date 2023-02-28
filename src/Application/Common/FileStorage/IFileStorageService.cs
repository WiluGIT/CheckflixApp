using CheckflixApp.Domain.Common;

namespace CheckflixApp.Application.Common.FileStorage;
public interface IFileStorageService
{
    public Task<string> UploadAsync<T>(FileUploadCommand? request, FileType supportedFileType, CancellationToken cancellationToken = default)
    where T : class;

    public void Remove(string? path);
}