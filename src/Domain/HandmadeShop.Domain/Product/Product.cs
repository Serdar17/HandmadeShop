using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public List<string> Images { get; set; } = new();

    public int CatalogId { get; set; }
    public virtual required Catalog Catalog { get; set; }

    public int LikeId { get; set; }
    public virtual Like? Like { get; set; }

    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}