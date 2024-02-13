namespace HandmadeShop.Web.Services;

public interface IIdentityService
{
    Task<string?> GetClaimsPrincipalData();
}
