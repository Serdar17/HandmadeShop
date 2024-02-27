using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.Api.Controllers.Order.Models;

/// <summary>
/// Create order request
/// </summary>
public class CreateOrderRequest
{
    /// <summary>
    /// Buyer
    /// </summary>
    public required BuyerModel Buyer { get; set; }
    
    /// <summary>
    /// Order model
    /// </summary>
    public required OrderModel Order { get; set; }
}

/// <summary>
/// Validation rules for <see cref="CreateOrderRequest"/>
/// </summary>
public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
{
    /// <summary>
    /// Ctor for <see cref="CreateOrderRequestValidator"/>
    /// </summary>
    public CreateOrderRequestValidator()
    {
        RuleFor(x => x.Buyer.Name)
            .NotEmpty();

        RuleFor(x => x.Buyer.PhoneNumber)
            .NotEmpty();

        RuleFor(x => x.Order.Description)
            .NotEmpty();

        RuleFor(x => x.Order.Address.City)
            .NotEmpty();
        
        RuleFor(x => x.Order.Address.Country)
            .NotEmpty();
        
        RuleFor(x => x.Order.Address.ExactAddress)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="CreateOrderRequest"/>
/// </summary>
public class CreateOrderRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="CreateOrderRequestProfile"/>
    /// </summary>
    public CreateOrderRequestProfile()
    {
        CreateMap<CreateOrderRequest, CreateOrderModel>();
    }
}