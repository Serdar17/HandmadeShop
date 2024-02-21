using HandmadeShop.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.Domain;

public class User : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public UserStatus Status { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? AvatarUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<UserLike> UserLikes { get; set; } = new List<UserLike>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}