namespace HandmadeShop.UseCase.Auth.Models;

public class VerifyEmailModel
{
    public Guid UserId { get; set; }
    public required string Token { get; set; }
}