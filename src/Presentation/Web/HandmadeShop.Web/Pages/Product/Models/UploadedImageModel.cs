﻿namespace HandmadeShop.Web.Pages.Product.Models;

public class UploadedImageModel
{
    public required string ImagePath { get; set; }
    
    public string? DownloadUrl { get; set; } 
}