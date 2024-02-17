using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.Web.Pages.Profile.Models;

namespace HandmadeShop.Web.Pages.Profile.Services;

public interface IAccountService
{
    Task<Result<AccountInfoModel>> GetAccountInfoAsync();
    Task<Result<UserProductModel>> GetUserProducts();
    Task<Result<AccountInfoModel>> UpdateAccountInfoModel(AccountInfoModel model);
    Task<Result<AccountInfoModel>> UploadAvatarAsync(MultipartFormDataContent form);
    Task<Result<AccountInfoModel>> DeleteAvatarAsync();
}