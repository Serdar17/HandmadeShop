using HandmadeShop.Web.Pages.Product.Models;

namespace HandmadeShop.Web.Pages.Profile.Models;

public class UserProductModel
{
    public Guid Id { get; set; }

    public List<ProductModel> Products { get; set; } = new();

}

// public class UserProductInfoModel
// {
//     public string Name { get; set; }
//     public decimal Price { get; set; }
//     public bool HasDiscount { get; set; }
//     public decimal? DiscountPercentage { get; set; }
//     
//     public string? DownloadImage { get; set; }
// }