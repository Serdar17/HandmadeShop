namespace HandmadeShop.SharedModel.Basket.Models;

public class CartModel
{
    public Guid UserId { get; set; }
    public List<CartItemModel> Items { get; set; } = new();

    public double TotalPrice => Items
        .Select(x => x.HasDiscount ? x.DiscountPrice : x.Price)
        .Sum();
}