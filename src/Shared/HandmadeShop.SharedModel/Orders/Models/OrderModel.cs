using HandmadeShop.Domain;

namespace HandmadeShop.SharedModel.Orders.Models;

public class OrderModel
{
    public int Id { get; set; }
    public Guid Uid { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public BuyerModel Buyer { get; set; }
    public AddressModel Address { get; set; }
    public string Description { get; set; }
    public OrderStatus OrderStatus { get; set; }

    public List<OrderItemModel> Items { get; set; }

    public double Sum => Items.Select(x => x.Price * x.Quantity).Sum();

    public string GetStatus()
    {
        return OrderStatus switch
        {
            OrderStatus.Pending => "Ожидание",
            OrderStatus.StockConfirmed => "Подтвержден запас",
            OrderStatus.Cancelled => "Отменен",
            OrderStatus.Shipped => "Доставлен",
            OrderStatus.Accepted => "Получен",
            OrderStatus.Sent => "Отправлен",
            _ => "Неизвестно"
        };
    }
}