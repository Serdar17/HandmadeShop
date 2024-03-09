using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Exceptions;

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

    public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

    public int BuyerId { get; set; }
    public virtual Buyer Buyer { get; set; }

    public virtual ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public void SetCancelledStatus()
    {
        if (OrderStatus == OrderStatus.Accepted || OrderStatus == OrderStatus.Shipped)
        {
            throw new OrderingDomainException($"The order with id={Id} wasn't cancelled");
        }
        OrderStatus = OrderStatus.Cancelled;
    }

    public void SetAcceptedStatus()
    {
        if (OrderStatus == OrderStatus.Shipped)
        {
            OrderStatus = OrderStatus.Accepted;
            return;
        }

        throw new OrderingDomainException($"The order with id={Id} wasn't accepted");
    }
}