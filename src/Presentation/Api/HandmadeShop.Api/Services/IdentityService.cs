using System.Security.Claims;
using HandmadeShop.Infrastructure.Abstractions.Identity;

namespace HandmadeShop.Api.Services;

/// <summary>
/// Identity service
/// </summary>
public class IdentityService : IIdentityService
{
    private readonly IHttpContextAccessor _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">IHttpContextAccessor</param>
    /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Get the user id if user is logged in
    /// </summary>
    /// <returns>Guid</returns>
    public Guid GetUserIdentity()
    {
        return Guid.Parse(_context.HttpContext.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
    }
}