using FluentValidation;
using HandmadeShop.Web.ValidationRules;

namespace HandmadeShop.Web.Pages.Product.Models;

public class ProductModel
{
    public Guid Uid { get; set; }
    public string Name { get; set; }
    
    public string Description { get; set; }

    public int Quantity { get; set; }
    
    public double Price { get; set; }

    public bool HasDiscount { get; set; }

    public double? DiscountPercentage { get; set; }

    public string CatalogName { get; set; }

    public string? DownloadUrl { get; set; }
}

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

        RuleFor(x => x.DiscountPercentage)
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