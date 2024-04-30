using FluentValidation;

namespace HandmadeShop.Common.Validator;

public class ModelValidator<T>(IValidator<T> validator) : IModelValidator<T>
    where T : class
{
    public void Check(T model)
    {
        var result = validator.Validate(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    public async Task CheckAsync(T model)
    {
        var result = await validator.ValidateAsync(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}