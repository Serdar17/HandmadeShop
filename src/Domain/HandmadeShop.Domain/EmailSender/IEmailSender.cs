using HandmadeShop.Domain.Email;

namespace HandmadeShop.Domain.EmailSender;

public interface IEmailSender
{
    Task Send(EmailModel email);
}