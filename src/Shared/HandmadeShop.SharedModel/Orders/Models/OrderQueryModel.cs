using HandmadeShop.Domain;

namespace HandmadeShop.SharedModel.Orders.Models;

public class OrderQueryModel
{
    public OrderStatus? Status { get; set; }
}