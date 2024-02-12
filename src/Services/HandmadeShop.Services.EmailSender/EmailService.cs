using HandmadeShop.Common.Extensions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Email.Constants;
using HandmadeShop.Services.Settings.Settings;

namespace HandmadeShop.Services.EmailSender;

public class EmailService : IEmailService
{
    private readonly MainSettings _mainSettings;
    private readonly WebSettings _webSettings;
    
    public EmailService(MainSettings mainSettings, WebSettings webSettings)
    {
        _mainSettings = mainSettings;
        _webSettings = webSettings;
    }
    
    public Task<EmailModel> GetVerificationEmail(User user, string token)
    {
        var confirmationLink = new Uri($"{_webSettings.Url}/verify-email")
            .AddParameter("userId", user.Id.ToString())
            .AddParameter("token", token);

        return Task.FromResult(new EmailModel(
            EmailSubject.Verification,
            EmailBody.Verification(user.Name, confirmationLink.ToString()),
            user.Email
        ));
    }
}