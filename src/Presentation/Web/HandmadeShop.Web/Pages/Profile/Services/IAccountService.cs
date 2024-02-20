using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Profile.Models;

namespace HandmadeShop.Web.Pages.Profile.Services;

public interface IAccountService
{
    Task<Result<AccountInfoModel>> GetAccountInfoAsync();
    Task<Result<PagedList<ProductModel>>> GetUserProducts(ProductQueryModel model);
    Task<Result<IEnumerable<Guid>>> GetAllFavoriteAsync();
    Task<Result<AccountInfoModel>> UpdateAccountInfoModel(AccountInfoModel model);
    Task<Result<AccountInfoModel>> UploadAvatarAsync(MultipartFormDataContent form);
    Task<Result<AccountInfoModel>> DeleteAvatarAsync();
}