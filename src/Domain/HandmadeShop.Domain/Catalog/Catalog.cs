using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Catalog(string name) : BaseEntity
{
    public string Name { get; set; } = name;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}