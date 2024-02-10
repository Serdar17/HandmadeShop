using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Review : BaseEntity
{
    public required string Comment { get; set; }
    public int Rating { get; set; }
    public required string UserName { get; set; }
    public bool IsApproved { get; set; }
    public List<string> Images = new();

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}