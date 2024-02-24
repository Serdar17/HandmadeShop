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

    public DateTime CreatedAt { get; set; }
    
    public required string CatalogName { get; set; }
}
