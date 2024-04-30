using HandmadeShop.Common.Exceptions;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.EmailSender;
using HandmadeShop.Services.Settings.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace HandmadeShop.Services.EmailSender;

public class EmailSender(
    ILogger<EmailSender> logger,
    EmailSettings emailSettings)
    : IEmailSender
{
    public async Task Send(EmailModel email)
    {
        var emailMessage = CreateEmailMessage(email);

        await SendAsync(emailMessage);
    }

    private MimeMessage CreateEmailMessage(EmailModel email)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress(email.Subject, emailSettings.From));
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
            await client.ConnectAsync(emailSettings.SmtpServer, emailSettings.Port, emailSettings.EnableSsl);
            await client.AuthenticateAsync(emailSettings.UserName, emailSettings.Password);
            await client.SendAsync(mailMessage);
            logger.LogInformation("Email to {EmailMessage}", mailMessage);
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