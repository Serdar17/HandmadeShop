using HandmadeShop.Domain.Email;

namespace HandmadeShop.Infrastructure.Abstractions.Actions;

public interface IAction
{
    Task SendEmail(EmailModel email);
}