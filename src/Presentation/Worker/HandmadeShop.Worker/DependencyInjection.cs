using HandmadeShop.Services.EmailSender;
using HandmadeShop.Services.Logger;
using HandmadeShop.Services.RabbitMq;
using HandmadeShop.Services.Settings;

namespace HandmadeShop.Worker;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppLogger()
            .AddRabbitMq()   
            .AddEmailSender()
            .AddEmailSettings()
            .AddMainSettings()
            .AddWebSettings()
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}