namespace HandmadeShop.Web.Pages.Product.Models;

public class DeleteProductImageModel
{
    public string PathToImage { get; set; }
    
    public DeleteProductImageModel(string pathToImage)
    {
        PathToImage = pathToImage;
    }
}