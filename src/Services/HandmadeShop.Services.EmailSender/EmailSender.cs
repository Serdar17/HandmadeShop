using HandmadeShop.Common.Exceptions;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.EmailSender;
using HandmadeShop.Services.Settings.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace HandmadeShop.Services.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly ILogger<EmailSender> _logger;
    private readonly EmailSettings _emailSettings;

    public EmailSender(ILogger<EmailSender> logger,
        EmailSettings emailSettings)
    {
        _logger = logger;
        _emailSettings = emailSettings;
    }

    public async Task Send(EmailModel email)
    {
        var emailMessage = CreateEmailMessage(email);

        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailModel email)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(email.Subject, _emailSettings.From));
        emailMessage.To.Add(new MailboxAddress("", email.DestinationEmail));
        emailMessage.Subject = email.Subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = email.Body
        };
        
        return emailMessage;
    }

    private async Task SendAsync(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port, _emailSettings.EnableSsl);
            await client.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await client.SendAsync(mailMessage);
            _logger.LogInformation("Email to {EmailMessage}", mailMessage);
        }
        catch (Exception e)
        {
            throw new ProcessException(e);
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}