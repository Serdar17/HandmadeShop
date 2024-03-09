using FluentValidation;
using HandmadeShop.Domain;

namespace HandmadeShop.Web.ValidationRules;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, string> ProductName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Name(nameof(Product));
    }
    
    public static IRuleBuilderOptions<T, string> CatalogName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Name(nameof(Catalog), max: 50);
    }
    
    public static IRuleBuilderOptions<T, string> Name<T>(this IRuleBuilder<T, string> ruleBuilder, string type,
        int min = 1, int max = 100)
    {
        return ruleBuilder
            .NotEmpty().WithMessage($"{type} name is required")
            .MinimumLength(min).WithMessage("Minimum length is 1")
            .MaximumLength(max).WithMessage("Maximum length is 100");
    }

    public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>
        (this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
    {
        return ruleBuilder
            .Must(list => list.Count < num).WithMessage("The list contains too many items");
    }
}