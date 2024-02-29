using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.Web.Pages.Basket.Services;

public interface IBasketService
{
    Task<Result<BasketModel>> GetUserBasketAsync();
    Task<Result<CartModel>> GetBasketData();
    Task<Result<BasketModel>> AddCartAsync(AddCartModel model);
    Task<Result<CartItemModel>> UpdateCartItemAsync(UpdateCartItemModel model);
    Task<Result<BasketModel>> DeleteCartItemAsync(Guid productId);
}