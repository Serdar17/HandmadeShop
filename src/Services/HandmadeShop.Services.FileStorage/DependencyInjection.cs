using CRM.Infrastructure.Abstractions.FileStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Services.FileStorage;

public static class DependencyInjection
{
    public static IServiceCollection AddAppFileStorage(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = CRM.Settings.Settings.Load<FileStorageSettings>(FileStorageSettings.SectionName, configuration);
        services.AddSingleton(settings);
        
        services.AddSingleton<IFileStorage, HandmadeShop.Services.FileStorage.FileStorage>();
        
        return services;
    }
}