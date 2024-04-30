using AutoMapper;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Basket.Models;

namespace HandmadeShop.UseCase.Basket.Models;

public class CartModelProfile : Profile
{
    public CartModelProfile()
    {
        CreateMap<Cart, CartModel>();
        CreateMap<CartItem, CartItemModel>()
            .AfterMap<CartModelProfileActions>();
    }

    public class CartModelProfileActions(IFileStorage fileStorage) : IMappingAction<CartItem, CartItemModel>
    {
        public void Process(CartItem source, CartItemModel destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImageUrl))
                return;

            destination.DownloadUrl = fileStorage.GetDownloadLinkAsync(source.ImageUrl).GetAwaiter().GetResult();
        }
    }
}