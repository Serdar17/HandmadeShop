using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Models;

public class OrderModelProfile : Profile
{
    public OrderModelProfile()
    {
        CreateMap<BuyerModel, Buyer>()
            .ReverseMap();
        
        CreateMap<OrderItem, OrderItemModel>()
            .ReverseMap();
        
        CreateMap<OrderModel, Domain.Order>()
            .ReverseMap();

        CreateMap<AddressModel, Address>()
            .ReverseMap();
        
        CreateMap<CartItem, OrderItem>()
            .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Price, opt => opt.MapFrom(x => x.HasDiscount ? x.DiscountPrice : x.Price))
            .ForMember(x => x.ImagePath, opt => opt.MapFrom(x => x.ImageUrl))
            .ReverseMap();
    }
}