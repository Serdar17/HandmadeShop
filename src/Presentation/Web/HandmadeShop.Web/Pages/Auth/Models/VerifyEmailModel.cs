namespace HandmadeShop.Web.Pages.Auth.Models;

public class VerifyEmailModel
{
    public Guid UserId { get; set; }
    public string Token { get; set; }

    public VerifyEmailModel(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }
}