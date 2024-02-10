using HandmadeShop.Services.Logger.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.Logger;

public static class DependencyInjection
{
    public static IServiceCollection AddAppLogger(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAppLogger, AppLogger>();
    }
}