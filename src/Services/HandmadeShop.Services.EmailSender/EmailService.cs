using HandmadeShop.Common.Extensions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Email.Constants;
using HandmadeShop.Services.Settings.Settings;

namespace HandmadeShop.Services.EmailSender;

public class EmailService(MainSettings mainSettings, WebSettings webSettings) : IEmailService
{
    private readonly MainSettings _mainSettings = mainSettings;

    public Task<EmailModel> GetVerificationEmail(User user, string token)
    {
        var confirmationLink = new Uri($"{webSettings.Url}/verify-email")
            .AddParameter("userId", user.Id.ToString())
            .AddParameter("token", token);

        return Task.FromResult(new EmailModel(
            EmailSubject.Verification,
            EmailBody.Verification(user.Name, confirmationLink.ToString()),
            user.Email
        ));
    }

    public Task<EmailModel> GetResetPasswordEmail(User user, string token)
    {
        var uri = new Uri($"{webSettings.Url}/reset-password")
            .AddParameter("email", user.Email)
            .AddParameter("token", token);
        
        return Task.FromResult(new EmailModel(
            EmailSubject.PasswordReset,
            EmailBody.PasswordReset(user.Name, uri.ToString()),
            user.Email
            ));
    }
}