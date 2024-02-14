using Microsoft.AspNetCore.Http;

namespace HandmadeShop.UseCase.Account.Models;

public class UploadAvatarModel
{
    public required IFormFile Avatar { get; set; }
}