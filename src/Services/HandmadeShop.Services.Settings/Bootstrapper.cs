using HandmadeShop.Services.Settings.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection AddMainSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<MainSettings>(MainSettings.SectionName, configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<SwaggerSettings>(SwaggerSettings.SectionName, configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddLogSettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<LogSettings>(LogSettings.SectionName, configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddIdentitySettings(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<IdentitySettings>(IdentitySettings.SectionName, configuration);
        services.AddSingleton(settings);

        return services;
    }

    public static IServiceCollection AddEmailSettings(this IServiceCollection services,
        IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<EmailSettings>(EmailSettings.SectionName, configuration);

        services.AddSingleton(settings);
        
        return services;
    }
    
    public static IServiceCollection AddFileStorageSettings(this IServiceCollection services,
        IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<FileStorageSettings>(FileStorageSettings.SectionName, configuration);
        services.AddSingleton(settings);
        return services;
    }
    
    public static IServiceCollection AddWebSettings(this IServiceCollection services,
        IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<WebSettings>(WebSettings.SectionName, configuration);
        services.AddSingleton(settings);
        return services;
    }
}