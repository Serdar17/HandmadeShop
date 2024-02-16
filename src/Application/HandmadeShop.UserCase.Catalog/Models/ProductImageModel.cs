namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductImageModel
{
    public string ImagePath { get; set; }
    
    public string DownloadUrl { get; set; }

    public ProductImageModel(string imagePath, string downloadUrl)
    {
        ImagePath = imagePath;
        DownloadUrl = downloadUrl;
    }
}