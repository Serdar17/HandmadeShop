using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;

namespace HandmadeShop.UseCase.Basket.Models;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<Product, CartItem>()
            .ForMember(x => x.Quantity, opt => opt.Ignore())
            .ForMember(x => x.MaxQuantity, opt => opt.MapFrom(x => x.Quantity))
            .ForMember(x => x.ProductId, opt => opt.MapFrom(x => x.Uid))
            .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Images.FirstOrDefault()));
    }
}