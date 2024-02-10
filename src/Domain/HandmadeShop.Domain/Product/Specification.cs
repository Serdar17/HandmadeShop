using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Specification : BaseEntity
{
    public required string Name { get; set; }
    public required string Value { get; set; }
    
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}