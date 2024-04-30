namespace HandmadeShop.Web.Pages.Product.Models;

public class DeleteProductImageModel(string pathToImage)
{
    public string PathToImage { get; set; } = pathToImage;
}