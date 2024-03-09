using FluentValidation;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.ValidationRules;

namespace HandmadeShop.Web.Pages.Product.Models;

public class ProductModelValidator : AbstractValidator<ProductModel>
{
    public ProductModelValidator()
    {
        RuleFor(x => x.CatalogName)
            .CatalogName();

        RuleFor(x => x.Name)
            .ProductName();

        RuleFor(x => x.Description)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThan(0);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.DiscountPrice)
            .NotEmpty()
            .When(x => x.HasDiscount)
            .GreaterThan(0);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<ProductModel>.CreateWithOptions((ProductModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}