namespace HandmadeShop.UseCase.Auth.Models;

public class ChangePasswordModel
{
    public required string Email { get; set; }
    public required string OldPassword { get; set; }
    public required string NewPassword { get; set; }
}