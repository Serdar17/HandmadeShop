namespace HandmadeShop.SharedModel.Basket.Models;

public class UpdateCartItemModel(Guid productId, int quantity)
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}