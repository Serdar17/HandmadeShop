using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Basket.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.UseCase.Basket.Commands.AddCart;
using HandmadeShop.UseCase.Basket.Commands.DeleteCart;
using HandmadeShop.UseCase.Basket.Commands.UpdateCartItem;
using HandmadeShop.UseCase.Basket.Queries.GetBasketData;
using HandmadeShop.UseCase.Basket.Queries.GetUserBasket;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Basket;

/// <summary>
/// Basket controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/basket")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class BasketController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    /// <summary>
    /// Ctor for <see cref="BasketController"/>
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="sender"></param>
    public BasketController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// Add product to basket
    /// </summary>
    /// <param name="request">AddCartRequest</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(BasketModel), 200)]
    public async Task<IResult> AddCartAsync([FromBody] AddCartRequest request)
    {
        var command = new AddCartCommand(_mapper.Map<AddCartModel>(request));
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get user basket
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(BasketModel), 200)]
    public async Task<IResult> GetUserBasketAsync()
    {
        var query = new GetUserBasketQuery();
        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Get basket data
    /// </summary>
    /// <returns></returns>
    [HttpGet("all")]
    [ProducesResponseType(typeof(CartModel), 200)]
    public async Task<IResult> GetBasketDataAsync()
    {
        var query = new GetBasketDataQuery();
        var result = await _sender.Send(query);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Update cart item endpoint
    /// </summary>
    /// <param name="request">Update cart item request</param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(typeof(CartItemModel), 200)]
    public async Task<IResult> UpdateCartItemAsync([FromBody] UpdateCartItemRequest request)
    {
        var command = new UpdateCartItemCommand(_mapper.Map<UpdateCartItemModel>(request));
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Delete cart item form basket
    /// </summary>
    /// <param name="productId">Unique product id</param>
    /// <returns></returns>
    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(typeof(BasketModel), 200)]
    public async Task<IResult> DeleteCartAsync([FromRoute] Guid productId)
    {
        var command = new DeleteCartCommand(productId);
        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        return result.ToProblemDetails();
    }
}