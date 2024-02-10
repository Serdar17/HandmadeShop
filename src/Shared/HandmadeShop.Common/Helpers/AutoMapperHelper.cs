using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.Common.Helpers;

public static class AutoMappersRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("handmadeshop."));

        services.AddAutoMapper(assemblies);
    }
}