using HandmadeShop.Domain.Enums;

namespace HandmadeShop.Web.Pages.Profile.Models;

public class AccountInfoModel
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Gender Gender { get; set; }
    public string? DownloadPath { get; set; }
    public string? AvatarUrl { get; set; }
    public DateTime? BirthDate { get; set; }
}