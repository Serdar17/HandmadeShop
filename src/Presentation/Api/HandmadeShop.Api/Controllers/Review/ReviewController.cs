using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Account.Models;
using HandmadeShop.Api.Controllers.Review.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.UseCase.Review.Commands.AddFavorite;
using HandmadeShop.UseCase.Review.Commands.RemoveFavorite;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Review;

/// <summary>
/// Reviews controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/reviews")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]
[Authorize]
public class ReviewController : ControllerBase
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    /// <summary>
    /// Ctor for <see cref="ReviewController"/>
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="mapper"></param>
    public ReviewController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    /// <summary>
    /// Add favorite
    /// </summary>
    /// <param name="request">Add favorite request</param>
    /// <returns></returns>
    [HttpPost("favorites")]
    public async Task<IResult> AddFavoriteAsync([FromBody] AddFavoriteRequest request)
    {
        var command = new AddFavoriteCommand(_mapper.Map<AddFavoriteModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Remove favorite
    /// </summary>
    /// <param name="request">Remove favorite request</param>
    /// <returns></returns>
    [HttpDelete("favorites")]
    public async Task<IResult> RemoveFavoritesAsync([FromBody] RemoveFavoriteRequest request)
    {
        var command = new RemoveFavoriteCommand(_mapper.Map<RemoveFavoriteModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }
    
}