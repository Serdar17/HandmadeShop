using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Models;

public class CreateOrderProfile : Profile
{
    public CreateOrderProfile()
    {
        CreateMap<BuyerModel, Buyer>();
        
        CreateMap<OrderModel, Domain.Order>();

        CreateMap<AddressModel, Address>();
        
        CreateMap<CartItem, OrderItem>()
            .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Price, opt => opt.MapFrom(x => x.HasDiscount ? x.DiscountPrice : x.Price))
            .ForMember(x => x.ImagePath, opt => opt.MapFrom(x => x.ImageUrl));
        
    }
}