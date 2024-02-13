using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Pages.Profile.Models;

namespace HandmadeShop.Web.Pages.Profile.Services;

public interface IAccountService
{
    Task<Result<AccountInfoModel>> GetAccountInfoAsync();
}