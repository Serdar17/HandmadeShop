using HandmadeShop.Context.Factories;
using HandmadeShop.Context.Settings;
using HandmadeShop.Infrastructure.Abstractions.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

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

        services.AddDbContext<AppDbContext>(dbInitOptionsDelegate);
        
        services.AddTransient(typeof(Lazy<>));

        // Registration of all repositories via the interface as scoped
        services.Scan(selector => selector.FromAssemblies(
                typeof(IAppDbContext).Assembly,
                typeof(AppDbContext).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());
        
        return services;
    }
}