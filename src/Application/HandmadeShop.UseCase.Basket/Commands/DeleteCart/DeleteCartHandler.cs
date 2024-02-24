using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Commands.DeleteCart;

internal sealed class DeleteCartHandler : ICommandHandler<DeleteCartCommand, BasketModel>
{
    private readonly ICacheService _cache;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public DeleteCartHandler(
        ICacheService cache,
        IIdentityService identityService,
        UserManager<User> userManager)
    {
        _cache = cache;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result<BasketModel>> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var data = await _cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);
        var model = new BasketModel { UserId = userId };
        
        if (data is null)
        {
            return model;
        }

        var item = data.Items.FirstOrDefault(x => x.ProductId == request.ProductId);
        if (item is not null)
        {
            data.Items.Remove(item);
        }

        await _cache.PutAsync(userId.ToString(), item, cancellationToken: cancellationToken);
        model.BasketProducts = data.Items.Select(x => x.ProductId).ToList();
        return model;
    }
}