using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Review : BaseEntity
{
    public required string Comment { get; set; }
    public int Rating { get; set; }
    public bool IsApproved { get; set; }
    public List<string> Images = new();

    public Guid OwnerId { get; set; }
    public virtual User Owner { get; set; }

    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}