using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.Api.Controllers.Basket.Models;

/// <summary>
/// Update cart item model
/// </summary>
public class UpdateCartItemRequest
{
    /// <summary>
    /// Product id
    /// </summary>
    public Guid ProductId { get; set; }
    
    /// <summary>
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }
}

/// <summary>
/// Validation rules for <see cref="UpdateCartItemRequest"/>
/// </summary>
public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
{
    /// <summary>
    /// Ctor for <see cref="UpdateCartItemRequestValidator"/>
    /// </summary>
    public UpdateCartItemRequestValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty();

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1);
    }
}

/// <summary>
/// Mapping rules for <see cref="UpdateCartItemRequest"/>
/// </summary>
public class UpdateCartItemRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="UpdateCartItemRequestProfile"/>
    /// </summary>
    public UpdateCartItemRequestProfile()
    {
        CreateMap<UpdateCartItemRequest, UpdateCartItemModel>();
    }
}