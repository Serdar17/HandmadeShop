using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Services.Settings.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Services.FileStorage;

public static class DependencyInjection
{
    public static IServiceCollection AddAppFileStorage(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Common.Settings.Settings.Load<FileStorageSettings>(FileStorageSettings.SectionName, configuration);
        services.AddSingleton(settings);
        
        services.AddSingleton<IFileStorage, HandmadeShop.Services.FileStorage.FileStorage>();
        
        return services;
    }
}