namespace HandmadeShop.SharedModel.Basket.Models;

public class CartItemModel
{
    public Guid ProductId { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public int MaxQuantity { get; set; }
    public double Price { get; set; }
    public bool HasDiscount { get; set; }
    public double DiscountPrice { get; set; }
    public string DownloadUrl { get; set; }
}