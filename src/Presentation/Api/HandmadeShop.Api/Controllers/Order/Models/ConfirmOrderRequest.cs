using AutoMapper;
using FluentValidation;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.Api.Controllers.Order.Models;

/// <summary>
/// Confirm order request
/// </summary>
public class ConfirmOrderRequest
{
    /// <summary>
    /// Order id
    /// </summary>
    public Guid OrderId { get; set; }
}

/// <summary>
/// Validation rules for <see cref="ConfirmOrderRequest"/>
/// </summary>
public class ConfirmOrderRequestValidator : AbstractValidator<ConfirmOrderRequest>
{
    /// <summary>
    /// Ctor for <see cref="ConfirmOrderRequestValidator"/>
    /// </summary>
    public ConfirmOrderRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty();
    }
}

/// <summary>
/// Mapping rules for <see cref="ConfirmOrderRequestProfile"/>
/// </summary>
public class ConfirmOrderRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="ConfirmOrderRequestProfile"/>
    /// </summary>
    public ConfirmOrderRequestProfile()
    {
        CreateMap<ConfirmOrderRequest, ConfirmOrderModel>();
    }
}