using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Queries.GetBasketData;

internal sealed class GetBasketDataHandler(
    ICacheService cache,
    UserManager<User> userManager,
    IIdentityService identityService,
    IMapper mapper)
    : IQueryHandler<GetBasketDataQuery, CartModel>
{
    public async Task<Result<CartModel>> Handle(GetBasketDataQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var data = await cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);

        if (data is null)
        {
            return new CartModel();
        }

        return mapper.Map<CartModel>(data);
    }
}