using HandmadeShop.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.Domain;

public class User : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public UserStatus Status { get; set; }
    public DateTime BirthDate { get; set; }
    
    // TODO: ПОменять на nullable
    public Gender Gender { get; set; }
    public string? AvatarUrl { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
}