using HandmadeShop.Domain.Email;

namespace HandmadeShop.Infrastructure.Abstractions.EmailSender;

public interface IEmailSender
{
    Task Send(EmailModel email);
}