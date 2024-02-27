using HandmadeShop.Application.Events;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.SharedModel.Catalogs.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Application.EventHandlers;

public class ProductPriceChangedEventHandler : INotificationHandler<ProductPriceChangedEvent>
{
    private readonly IAppDbContext _context;
    private readonly ICacheService _cacheService;

    public ProductPriceChangedEventHandler(ICacheService cacheService, IAppDbContext context)
    {
        _cacheService = cacheService;
        _context = context;
    }

    public async Task Handle(ProductPriceChangedEvent notification, CancellationToken cancellationToken)
    {
        var userIds = await _context.Users.Select(x => x.Id.ToString()).ToListAsync(cancellationToken);

        foreach (var userId in userIds)
        {
            var cart = await _cacheService.GetAsync<Cart>(userId, cancellationToken: cancellationToken);
            
            await UpdateCartItemAsync(cart, notification.Model);
        }
    }

    private async Task UpdateCartItemAsync(Cart? cart, ProductModel model)
    {
        if (cart is null)
            return;
        
        var items = cart.Items.Where(x => x.ProductId == model.Uid);

        foreach (var item in items)
        {
            item.Name = model.Name;
            item.Price = model.Price;
            item.HasDiscount = model.HasDiscount;
            item.DiscountPrice = model.DiscountPrice;
            item.MaxQuantity = model.Quantity;
        }

        await _cacheService.PutAsync(cart.UserId.ToString(), cart);
    }
}