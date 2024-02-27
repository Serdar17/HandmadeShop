using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Buyer : BaseEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public int OrderId { get; set; }
    public virtual Order Order { get; set; }
}