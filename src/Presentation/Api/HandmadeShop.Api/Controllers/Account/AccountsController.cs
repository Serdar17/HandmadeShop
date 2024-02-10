using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Common.Extensions;
using HandmadeShop.UseCase.Account.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Account;

/// <summary>
/// Accounts controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/accounts")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]

public class AccountsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="sender"></param>
    public AccountsController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// Get user info by id
    /// </summary>
    /// <param name="userId">Unique user id</param>
    /// <returns></returns>
    [HttpGet("info/{userId:guid}")]
    public async Task<IResult> GetUserInfoAsync([FromRoute] Guid userId)
    {
        var query = new GetUserInfoQuery(userId);
        var result = await _sender.Send(query);
        
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }
}