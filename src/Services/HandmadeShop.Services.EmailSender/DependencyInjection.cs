using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.EmailSender;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.EmailSender;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailSender(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSender, EmailSender>();

        services.AddSingleton<IEmailService, EmailService>();

        return services;
    }
}