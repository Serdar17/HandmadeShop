using HandmadeShop.Api.Middlewares;

namespace HandmadeShop.Api.Configuration;

/// <summary>
/// Extension for <see cref="IApplicationBuilder"/>
/// </summary>
public static class MiddlewaresConfiguration
{
    /// <summary>
    /// Extension method for <see cref="IApplicationBuilder"/>
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    public static void UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionsMiddleware>();
    }
}