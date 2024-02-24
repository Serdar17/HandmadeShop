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

internal sealed class GetBasketDataHandler : IQueryHandler<GetBasketDataQuery, CartModel>
{
    private readonly ICacheService _cache;
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public GetBasketDataHandler(
        ICacheService cache,
        UserManager<User> userManager,
        IIdentityService identityService,
        IMapper mapper)
    {
        _cache = cache;
        _userManager = userManager;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<Result<CartModel>> Handle(GetBasketDataQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var data = await _cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);

        if (data is null)
        {
            return new CartModel();
        }

        return _mapper.Map<CartModel>(data);
    }
}