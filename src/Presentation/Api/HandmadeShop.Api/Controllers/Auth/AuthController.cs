using Asp.Versioning;
using AutoMapper;
using HandmadeShop.Api.Controllers.Auth.Models;
using HandmadeShop.Api.Services;
using HandmadeShop.Common.Extensions;
using HandmadeShop.UseCase.Auth.Commands.ChangePassword;
using HandmadeShop.UseCase.Auth.Commands.ForgotPassword;
using HandmadeShop.UseCase.Auth.Commands.RegisterUser;
using HandmadeShop.UseCase.Auth.Commands.ResetPassword;
using HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;
using HandmadeShop.UseCase.Auth.Commands.VerifyEmail;
using HandmadeShop.UseCase.Auth.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ForgotPasswordRequest = HandmadeShop.Api.Controllers.Auth.Models.ForgotPasswordRequest;

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
    private readonly IIdentityService _identityService;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapper"></param>
    /// <param name="sender"></param>
    /// <param name="identityService"></param>
    public AuthController(IMapper mapper, ISender sender, IIdentityService identityService)
    {
        _mapper = mapper;
        _sender = sender;
        _identityService = identityService;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
    [AllowAnonymous]
    [HttpPost("verify-email")]
    [ProducesResponseType(200)]
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

    /// <summary>
    /// Forgot password endpoint
    /// </summary>
    /// <param name="request">ForgotPasswordRequest</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("password/forgot")]
    public async Task<IResult> ForgotPasswordAsync([FromBody] ForgotPasswordRequest request)
    {
        var command = new ForgotPasswordCommand(_mapper.Map<ForgotPasswordModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="request">Reset password request</param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("password/reset")]
    [ProducesResponseType(200)]
    public async Task<IResult> ResetPasswordAsync([FromBody] ResetPasswordRequest request)
    {
        var command = new ResetPasswordCommand(_mapper.Map<ResetPasswordModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }

    /// <summary>
    /// Change password from profile
    /// </summary>
    /// <param name="request">Change password request</param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("password/change")]
    [ProducesResponseType(200)]
    public async Task<IResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
    {
        var command = new ChangePasswordCommand(_mapper.Map<ChangePasswordModel>(request));

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("password/reset")]
    public async Task<IResult> ResetPasswordFromProfileAsync([FromBody] ResetProfilePasswordRequest request)
    {
        var id = _identityService.GetUserIdentity();
        var model = _mapper.Map<ResetProfilePasswordModel>(request);
        model.UserId = id;
        
        var command = new ResetProfilePasswordCommand(model);

        var result = await _sender.Send(command);

        if (result.IsSuccess)
            return Results.Ok();

        return result.ToProblemDetails();
    }
}