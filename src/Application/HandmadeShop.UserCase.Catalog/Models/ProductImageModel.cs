namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductImageModel(string imagePath, string downloadUrl)
{
    public string ImagePath { get; set; } = imagePath;

    public string DownloadUrl { get; set; } = downloadUrl;
}