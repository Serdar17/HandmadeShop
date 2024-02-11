using HandmadeShop.Services.Logger;
using HandmadeShop.Services.RabbitMq;

namespace HandmadeShop.Worker;

public static class DependencyInjection
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddAppLogger()
            .AddRabbitMq()            
            ;

        services.AddSingleton<ITaskExecutor, TaskExecutor>();

        return services;
    }
}