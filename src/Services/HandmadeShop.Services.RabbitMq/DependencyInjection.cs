using HandmadeShop.Services.RabbitMq.RabbitMq;
using HandmadeShop.Services.RabbitMq.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<RabbitMqSettings>(RabbitMqSettings.SectionName, configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IRabbitMq, RabbitMq.RabbitMq>();

        return services;
    }
}
