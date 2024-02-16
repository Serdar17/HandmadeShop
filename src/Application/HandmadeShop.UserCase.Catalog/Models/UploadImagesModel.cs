using Microsoft.AspNetCore.Http;

namespace HandmadeShop.UserCase.Catalog.Models;

public class UploadImagesModel
{
    /// <summary>
    /// List of product images
    /// </summary>
    public required IFormFile Image { get; set; }
}