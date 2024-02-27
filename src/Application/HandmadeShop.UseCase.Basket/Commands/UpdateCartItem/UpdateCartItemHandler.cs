using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Commands.UpdateCartItem;

internal sealed class UpdateCartItemHandler : ICommandHandler<UpdateCartItemCommand, CartItemModel>
{
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public UpdateCartItemHandler(
        ICacheService cacheService,
        IMapper mapper,
        IIdentityService identityService,
        UserManager<User> userManager)
    {
        _cacheService = cacheService;
        _mapper = mapper;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result<CartItemModel>> Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cart = await _cacheService.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);

        if (cart is null)
        {
            return CartErrors.NotFound(userId);
        }

        var cartItem = cart.Items.FirstOrDefault(x => x.ProductId == request.Model.ProductId);
        
        if (cartItem is null)
        {
            return CartItemErrors.NotFound(request.Model.ProductId);
        }

        cartItem.Quantity = request.Model.Quantity >= cartItem.MaxQuantity
            ? cartItem.MaxQuantity
            : request.Model.Quantity;

        await _cacheService.PutAsync(userId.ToString(), cart, cancellationToken: cancellationToken);

        return _mapper.Map<CartItemModel>(cartItem);
    }
}