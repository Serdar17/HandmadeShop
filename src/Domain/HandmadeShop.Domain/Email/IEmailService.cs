namespace HandmadeShop.Domain.Email;

public interface IEmailService
{
    Task<EmailModel> GetVerificationEmail(User user, string token);
    Task<EmailModel> GetResetPasswordEmail(User user, string token);
}