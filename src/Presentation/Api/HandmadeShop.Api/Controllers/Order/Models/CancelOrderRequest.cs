using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.Api.Controllers.Order.Models;

/// <summary>
/// Cancel order request
/// </summary>
public class CancelOrderRequest
{
    /// <summary>
    /// Order id
    /// </summary>
    public Guid OrderId { get; set; }
}

/// <summary>
/// Validation rules for <see cref="CancelOrderRequest"/>
/// </summary>
public class CancelOrderRequestValidator : AbstractValidator<CancelOrderRequest>
{
    /// <summary>
    /// Ctor for <see cref="CancelOrderRequestValidator"/>
    /// </summary>
    public CancelOrderRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="CancelOrderRequest"/>
/// </summary>
public class CancelOrderRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="CancelOrderRequestProfile"/>
    /// </summary>
    public CancelOrderRequestProfile()
    {
        CreateMap<CancelOrderRequest, CancelOrderModel>();
    }
}