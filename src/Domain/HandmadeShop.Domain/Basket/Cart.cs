namespace HandmadeShop.Domain.Basket;

public class Cart
{
    public Guid UserId { get; set; }
    public List<CartItem> Items { get; set; }
}