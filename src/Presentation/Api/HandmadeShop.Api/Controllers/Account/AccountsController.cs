using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Account.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.UseCase.Account.Commands.DeleteAvatar;
using HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;
using HandmadeShop.UseCase.Account.Commands.UploadAvatar;
using HandmadeShop.UseCase.Account.Models;
using HandmadeShop.UseCase.Account.Queries.GetUserInfo;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
[Authorize]

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
    [ProducesResponseType(typeof(AccountInfoModel), 200)]
    public async Task<IResult> GetUserInfoAsync([FromRoute] Guid userId)
    {
        var query = new GetUserInfoQuery(userId);
        var result = await _sender.Send(query);
        
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Upload avatar image, max size of the uploaded file is 5MB.
    /// </summary>
    /// <param name="request">Upload avatar request</param>
    /// <returns></returns>
    [HttpPost("avatar/upload")]
    [RequestFormLimits(MultipartBodyLengthLimit = 5_242_880)]
    [ProducesResponseType(typeof(AccountInfoModel), 200)]
    public async Task<IResult> UploadAvatarAsync([FromForm] UploadAvatarRequest request)
    {
        var command = new UploadAvatarCommand(_mapper.Map<UploadAvatarModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Delete user avatar
    /// </summary>
    /// <returns></returns>
    [HttpDelete("avatar/delete")]
    [ProducesResponseType(typeof(AccountInfoModel), 200)]
    public async Task<IResult> DeleteAvatarAsync()
    {
        var command = new DeleteAvatarCommand();

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);
        
        return result.ToProblemDetails();
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut("info")]
    [ProducesResponseType(typeof(AccountInfoModel), 200)]
    public async Task<IResult> UpdateAccountInfoAsync([FromBody] UpdateAccountInfoRequest request)
    {
        var command = new UpdateAccountInfoCommand(_mapper.Map<UpdateAccountInfoModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.ToProblemDetails();
    }
    
    
}