using HandmadeShop.Common.Extensions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Email.Constants;

namespace HandmadeShop.Services.EmailSender;

public class EmailService : IEmailService
{
    private readonly MainSettings _mainSettings;
    
    public EmailService(MainSettings mainSettings)
    {
        _mainSettings = mainSettings;
    }
    
    public Task<EmailModel> GetVerificationEmail(User user, string token)
    {
        var confirmationLink = new Uri($"{_mainSettings.PublicUrl}/api/v1/auth/verify")
            .AddParameter("userId", user.Id.ToString())
            .AddParameter("token", token);

        return Task.FromResult(new EmailModel(
            EmailSubject.Verification,
            EmailBody.Verification(user.Name, confirmationLink.ToString()),
            user.Email
        ));
    }
}