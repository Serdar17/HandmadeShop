using System.Security.Claims;
using HandmadeShop.Infrastructure.Abstractions.Identity;

namespace HandmadeShop.Api.Services;

/// <summary>
/// 
/// </summary>
public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _context;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Guid GetUserIdentity()
    {
        return Guid.Parse(_context.HttpContext.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
    }
}