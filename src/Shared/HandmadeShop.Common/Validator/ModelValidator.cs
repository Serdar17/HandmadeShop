using FluentValidation;

namespace HandmadeShop.Common.Validator;

public class ModelValidator<T> : IModelValidator<T> where T : class
{
    private readonly IValidator<T> _validator;

    public ModelValidator(IValidator<T> validator)
    {
        _validator = validator;
    }

    public void Check(T model)
    {
        var result = _validator.Validate(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }

    public async Task CheckAsync(T model)
    {
        var result = await _validator.ValidateAsync(model);
        if (!result.IsValid)
            throw new ValidationException(result.Errors);
    }
}