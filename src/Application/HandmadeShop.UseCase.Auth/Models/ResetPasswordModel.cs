namespace HandmadeShop.UseCase.Auth.Models;

public class ResetPasswordModel
{
    public required string Email { get; set; }
    public required string Token { get; set; }
    public required string Password { get; set; }
}