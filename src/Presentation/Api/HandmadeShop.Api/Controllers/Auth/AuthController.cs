using Asp.Versioning;
using AutoMapper;
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
}