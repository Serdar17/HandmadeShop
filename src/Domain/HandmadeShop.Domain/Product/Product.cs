using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public bool HasDiscount { get; set; }
    public double DiscountPrice { get; set; }
    public List<string> Images { get; set; } = new();

    public int CatalogId { get; set; }
    public virtual Catalog Catalog { get; set; }

    public int LikeId { get; set; }
    public virtual Like? Like { get; set; }

    public Guid UserId { get; set; }
    public virtual User? User { get; set; }

    public virtual ICollection<Specification> Specifications { get; set; } = new List<Specification>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public void AddLike(User user)
    {
        if (Like is null)
        {
            Like = new Like
            {
                Quantity = 1
            };
        }
        else
        {
            Like.Quantity += 1;
        }
        
        Like.UserLikes.Add(new UserLike
        {
            Like = Like,
            User = user
        });
    }
    
    public void RemoveLike()
    {
        if (Like is null)
        {
            return;
        }
    
        Like.Quantity -= 1;
    }
}