﻿using HandmadeShop.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using NetSchool.Common;

namespace HandmadeShop.Api.Configuration;

public static class ControllerAndViewsConfiguration
{
    public static IServiceCollection AddAppControllerAndViews(this IServiceCollection services)
    {
        services
            .AddRazorPages()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            ;

        services
            .AddControllers()
            .AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultSettings())
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                    new BadRequestObjectResult(context.ModelState.ToErrorResponse());
            });

        return services;
    }

    public static IEndpointRouteBuilder UseAppControllerAndViews(this IEndpointRouteBuilder app)
    {
        app.MapRazorPages();
        app.MapControllers();

        return app;
    }
}