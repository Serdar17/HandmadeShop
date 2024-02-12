using HandmadeShop.Context.Factories;
using HandmadeShop.Context.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Context;

public static class DependencyInjection
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection services,
        IConfiguration? configuration = null)
    {
        var settings = Common.Settings.Settings.Load<DbSettings>(DbSettings.SectionName, configuration);
        services.AddSingleton(settings);

        var dbInitOptionsDelegate = DbContextOptionsFactory.Configure(settings.ConnectionString, settings.Type, true);
        services.AddDbContextFactory<AppDbContext>(dbInitOptionsDelegate);

        return services;
    }
}