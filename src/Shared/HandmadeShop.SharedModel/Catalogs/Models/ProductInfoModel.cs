namespace HandmadeShop.SharedModel.Catalogs.Models;

public class ProductInfoModel
{
    public Guid Uid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public bool HasDiscount { get; set; }
    public double DiscountPrice { get; set; }
    public int ReviewCount { get; set; }
    public int Rating { get; set; }

    public IList<string> ImageUrls { get; set; } = new List<string>();

    public DateTime CreatedAt { get; set; }
    
    public required string CatalogName { get; set; }
}
