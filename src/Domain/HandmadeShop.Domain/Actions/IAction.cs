using HandmadeShop.Domain.Email;

namespace HandmadeShop.Domain.Actions;

public interface IAction
{
    Task SendEmail(EmailModel email);
}