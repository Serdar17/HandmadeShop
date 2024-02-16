namespace HandmadeShop.UserCase.Catalog.Models;

public class UploadedImageModel
{
    public required string ImagePath { get; set; }
    
    public string? DownloadUrl { get; set; } 
}
