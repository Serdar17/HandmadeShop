using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.SharedModel.Accounts.Models;

public class UserProductModel
{
    public Guid Id { get; set; }

    public List<ProductModel> Products { get; set; } = new();

}

public class UserProductInfoModel
{
    public Guid Uid { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    
    public string? DownloadImage { get; set; }
}