using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Order : BaseEntity
{
    /// <summary>
    /// Order description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Owned type
    /// </summary>
    public Address Address { get; set; }

    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;

    public int BuyerId { get; set; }
    public virtual Buyer Buyer { get; set; }

    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}