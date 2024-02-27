using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class OrderItem : BaseEntity
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public string? ImagePath { get; set; }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}