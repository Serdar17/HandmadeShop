namespace HandmadeShop.SharedModel.Basket.Models;

public class BasketModel
{
    public Guid UserId { get; set; }

    public List<Guid> BasketProducts { get; set; } = new();
}