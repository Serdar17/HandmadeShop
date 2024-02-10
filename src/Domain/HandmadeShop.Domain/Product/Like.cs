using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Like : BaseEntity
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }

    public Guid UserId { get; set; }
}