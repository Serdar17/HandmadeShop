namespace HandmadeShop.Web.Pages.Auth.Models;

public class VerifyEmailModel(Guid userId, string token)
{
    public Guid UserId { get; set; } = userId;
    public string Token { get; set; } = token;
}