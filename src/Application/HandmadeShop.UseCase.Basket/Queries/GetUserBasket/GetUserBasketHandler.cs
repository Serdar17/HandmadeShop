using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Queries.GetUserBasket;

internal sealed class GetUserBasketHandler : IQueryHandler<GetUserBasketQuery, BasketModel>
{
    private readonly ICacheService _cache;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    
    public GetUserBasketHandler(
        ICacheService cache,
        IIdentityService identityService,
        UserManager<User> userManager)
    {
        _cache = cache;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result<BasketModel>> Handle(GetUserBasketQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cart = await _cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);
        var model = new BasketModel { UserId = userId };
        
        if (cart is not null)
        {
            model.BasketProducts = cart.Items.Select(x => x.ProductId).ToList();
        }

        return model;
    }
}