using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Order.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.UseCase.Order.Commands.CreateOrder;
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
}