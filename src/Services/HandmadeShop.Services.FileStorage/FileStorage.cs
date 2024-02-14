using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Services.Settings.Settings;
using Microsoft.AspNetCore.Http;
using YandexDisk.Client;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;

namespace HandmadeShop.Services.FileStorage;

public class FileStorage : IFileStorage
{
    private readonly IDiskApi _diskApi;

    public FileStorage(FileStorageSettings settings)
    {
        _diskApi = new DiskHttpApi(settings.OAuthToken);
    }

    public async Task<string> UploadAsync(Guid id, 
        IFormFile image, 
        string pathToFolder,
        CancellationToken cancellationToken = default)
    {
        await CreateDirectoryIfNotExistAsync(id, pathToFolder, cancellationToken);
        
        var fileName = string.Join("", Path.GetRandomFileName(), Path.GetExtension(image.FileName));
        var filePath = Path.Combine(pathToFolder, id.ToString(), fileName)
            .Replace('\\', '/');
        
        var uploadLink =
            await _diskApi.Files.GetUploadLinkAsync(filePath, 
                true, cancellationToken);

        await _diskApi.Files.UploadAsync(uploadLink, image.OpenReadStream(), cancellationToken);

        return filePath;
    }

    public async Task<string> GetDownloadLinkAsync(string path, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        
        var downloadLink = await _diskApi.Files.GetDownloadLinkAsync(path, cancellationToken);

        return downloadLink.Href;
    }

    public async Task DeleteFileAsync(string pathToFile, CancellationToken cancellationToken = default)
    {
        var request = new DeleteFileRequest { Path = pathToFile, Permanently = true };
        var link = await _diskApi.Commands.DeleteAsync(request, cancellationToken);
    }

    private async Task CreateDirectoryIfNotExistAsync(Guid id, string pathToFolder,
        CancellationToken cancellationToken = default)
    {
        var resource = await _diskApi.MetaInfo.GetInfoAsync(
                new ResourceRequest { Path = pathToFolder },
                cancellationToken);
        
        if (!resource.Embedded.Items.Any(x => x.Type == ResourceType.Dir && x.Name.Equals(id.ToString())))
        {
            await _diskApi.Commands.CreateDictionaryAsync($"{pathToFolder}/{id}", cancellationToken);
        }
    }

    // private string GetFilePathByType(Guid id, FileUploadType type)
    // {
    //     string pathToFolder;
    //     switch (type)
    //     {
    //         case FileUploadType.User:
    //             pathToFolder = PathToAvatarsFolder;
    //             break;
    //         case FileUploadType.Department:
    //             pathToFolder = PathToDepartmentImagesFolder;
    //             break;
    //         case FileUploadType.Session:
    //             pathToFolder = PathToSessionImagesFolder;
    //             break;
    //         default:
    //             throw new ArgumentException(nameof(type));
    //     }
    //     
    //     
    //     return Path.Combine(pathToFolder, id.ToString(), Path.GetRandomFileName())
    //         .Replace('\\', '/');
    // }
}