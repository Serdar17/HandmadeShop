namespace HandmadeShop.Domain.Email;

public interface IEmailService
{
    Task<EmailModel> GetVerificationEmail(User user, string token);
}