using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Order.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.UseCase.Order.Commands.CancelOrder;
using HandmadeShop.UseCase.Order.Commands.ConfirmOrder;
using HandmadeShop.UseCase.Order.Commands.CreateOrder;
using HandmadeShop.UseCase.Order.Queries.GetOrderDetails;
using HandmadeShop.UseCase.Order.Queries.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Order;

/// <summary>
/// Order controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/orders")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    /// <summary>
    /// Ctor for <see cref="OrderController"/>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="mapper"></param>
    public OrderController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Create order 
    /// </summary>
    /// <param name="request">Create order request</param>
    [HttpPost]
    public async Task<IResult> CreateOrderAsync([FromBody] CreateOrderRequest request)
    {
        var command = new CreateOrderCommand(_mapper.Map<CreateOrderModel>(request));
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get orders
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<OrderModel>), 200)]
    public async Task<IResult> GetOrdersAsync([FromQuery] OrderQueryRequest request)
    {
        var query = new GetOrdersQuery(_mapper.Map<OrderQueryModel>(request));
        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get order details by id
    /// </summary>
    /// <param name="orderId">Unique order id</param>
    /// <returns></returns>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderModel), 200)]
    public async Task<IResult> GetOrderDetailsAsync([FromRoute] Guid orderId)
    {
        var query = new GetOrderDetailsQuery(orderId);
        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Cancel order endpoint
    /// </summary>
    /// <param name="request">Cancel order request</param>
    [HttpPut("cancel")]
    [ProducesResponseType(typeof(OrderModel), 200)]
    public async Task<IResult> CancelOrderAsync([FromBody] CancelOrderRequest request)
    {
        var command = new CancelOrderCommand(_mapper.Map<CancelOrderModel>(request));
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Confirm order endpoint
    /// </summary>
    /// <param name="request">ConfirmOrderRequest</param>
    [HttpPut("confirm")]
    [ProducesResponseType(typeof(OrderModel), 200)]
    public async Task<IResult> ConfirmOrderAsync([FromBody] ConfirmOrderRequest request)
    {
        var command = new ConfirmOrderCommand(_mapper.Map<ConfirmOrderModel>(request));
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }
}