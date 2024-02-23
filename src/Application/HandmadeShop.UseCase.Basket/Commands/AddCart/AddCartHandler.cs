using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Basket.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Basket.Commands.AddCart;

internal sealed class AddCartHandler : ICommandHandler<AddCartCommand, BasketModel>
{
    private readonly ICacheService _cache;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public AddCartHandler(
        ICacheService cache,
        IUnitOfWork unitOfWork,
        IIdentityService identityService, 
        UserManager<User> userManager, IMapper mapper)
    {
        _cache = cache;
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<BasketModel>> Handle(AddCartCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Model.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.ProductId);
        }
        
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var cartItem = _mapper.Map<CartItem>(product);
        cartItem.Quantity = 1;
        var data = await _cache.GetAsync<Cart>(userId.ToString(), cancellationToken: cancellationToken);
        Cart cart;
            
        if (data is null)
        {
            cart = new Cart
            {
                UserId = userId,
                Items = [cartItem],
            };
        }
        else
        {
            cart = data;
            cart.Items.Add(cartItem);
        }
        
        await _cache.PutAsync(userId.ToString(), cart, cancellationToken: cancellationToken);

        return new BasketModel
        {
            UserId = userId,
            BasketProducts = cart.Items.Select(x => x.ProductId).ToList()
        };
    }
}