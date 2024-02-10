using FluentValidation.AspNetCore;
using HandmadeShop.Common.Helpers;
using HandmadeShop.Common.Validator;

namespace HandmadeShop.Api.Configuration;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddAppValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(opt => { opt.DisableDataAnnotationsValidation = true; });

        ValidatorsRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}