namespace HandmadeShop.SharedModel.Basket.Models;

public class UpdateCartItemModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public UpdateCartItemModel(Guid productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}