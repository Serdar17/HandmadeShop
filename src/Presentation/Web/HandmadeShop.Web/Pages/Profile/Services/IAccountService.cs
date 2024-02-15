using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Pages.Profile.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HandmadeShop.Web.Pages.Profile.Services;

public interface IAccountService
{
    Task<Result<AccountInfoModel>> GetAccountInfoAsync();
    Task<Result<AccountInfoModel>> UpdateAccountInfoModel(AccountInfoModel model);
    Task<Result<AccountInfoModel>> UploadAvatarAsync(MultipartFormDataContent form);
    Task<Result<AccountInfoModel>> DeleteAvatarAsync();
}