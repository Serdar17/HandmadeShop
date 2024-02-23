using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.Api.Controllers.Basket.Models;

/// <summary>
/// Add cart request
/// </summary>
public class AddCartRequest
{
    /// <summary>
    /// Product id
    /// </summary>
    public required Guid ProductId { get; set; }
}

/// <summary>
/// Validation rules for <see cref="AddCartRequest"/>
/// </summary>
public class AddCartRequestValidator : AbstractValidator<AddCartRequest>
{
    /// <summary>
    /// Ctor for <see cref="AddCartRequestValidator"/>
    /// </summary>
    public AddCartRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="AddCartRequest"/>
/// </summary>
public class AddCartRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="AddCartRequestProfile"/>
    /// </summary>
    public AddCartRequestProfile()
    {
        CreateMap<AddCartRequest, AddCartModel>();
    }
}