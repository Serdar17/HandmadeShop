using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HandmadeShop.UseCase.Review;

public static class DependencyInjection
{
    public static IServiceCollection AddReviewService(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}