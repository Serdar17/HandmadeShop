using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Catalog : BaseEntity
{
    public required string Name { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}