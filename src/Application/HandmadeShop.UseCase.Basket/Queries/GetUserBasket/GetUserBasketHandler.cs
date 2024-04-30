using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Queries.GetUserBasket;

internal sealed class GetUserBasketHandler(
    ICacheService cache,
    IIdentityService identityService,
    UserManager<User> userManager)
    : IQueryHandler<GetUserBasketQuery, BasketModel>
{
    public async Task<Result<BasketModel>> Handle(GetUserBasketQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cart = await cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);
        var model = new BasketModel { UserId = userId };
        
        if (cart is not null)
        {
            model.BasketProducts = cart.Items.Select(x => x.ProductId).ToList();
        }

        return model;
    }
}