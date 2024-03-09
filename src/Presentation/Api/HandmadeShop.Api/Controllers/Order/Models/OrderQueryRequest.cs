using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.Api.Controllers.Order.Models;

/// <summary>
/// Order query request
/// </summary>
public class OrderQueryRequest
{
    /// <summary>
    /// Order status
    /// </summary>
    public OrderStatus? Status { get; set; }
}

/// <summary>
/// Mapping rules for <see cref="OrderQueryRequest"/>
/// </summary>
public class OrderQueryRequestProfile : Profile
{
    /// <summary>
    /// Ctor for <see cref="OrderQueryRequestProfile"/>
    /// </summary>
    public OrderQueryRequestProfile()
    {
        CreateMap<OrderQueryRequest, OrderQueryModel>();
    }
}