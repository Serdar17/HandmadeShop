using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Auth.Models;
using HandmadeShop.Common.Extensions;
using HandmadeShop.UseCase.Auth.Commands.RegisterUser;
using HandmadeShop.UseCase.Auth.Commands.VerifyEmail;
using HandmadeShop.UseCase.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.Api.Controllers.Auth;

/// <summary>
/// Auth controller
/// </summary>
/// <response code="400">Bad Request</response>;
/// <response code="401">Unauthorized</response>;
/// <response code="403">Forbidden</response>;
/// <response code="404">Not Found</response>;
/// <response code="409">Conflict</response>;
[Route("api/v{version:apiVersion}/auth")]
[Produces("application/json")]
[ApiController]
[ApiVersion("1.0")]

public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="sender"></param>
    public AuthController(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserAccountModel), 200)]
    public async Task<IResult> RegisterAsync([FromBody] RegistrationUserRequest request)
    {
        var command = new RegisterUserCommand(_mapper.Map<RegisterUserModel>(request));
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
    [HttpPost("verify-email")]
    public async Task<IResult> VerifyEmailAsync([FromBody] VerifyEmailRequest request)
    {
        var command = new VerifyEmailCommand(_mapper.Map<VerifyEmailModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
        {
            return Results.Ok();
        }

        return result.ToProblemDetails();
    }
}