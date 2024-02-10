using HandmadeShop.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.Domain;

public class User : IdentityUser<Guid>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public UserStatus Status { get; set; }
    public DateTime BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string? AvatarUrl { get; set; }
}