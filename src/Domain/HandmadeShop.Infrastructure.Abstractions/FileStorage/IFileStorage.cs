using Microsoft.AspNetCore.Http;

namespace HandmadeShop.Infrastructure.Abstractions.FileStorage;

public interface IFileStorage
{
    Task<string> UploadAsync(Guid id, IFormFile image, string pathToFolder, CancellationToken cancellationToken = default);
    Task<string> GetDownloadLinkAsync(string path, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string pathToFolder, CancellationToken cancellationToken = default);
}