using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Basket;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.SharedModel.Orders.Models;

namespace HandmadeShop.UseCase.Order.Models;

public class OrderModelProfile : Profile
{
    public OrderModelProfile()
    {
        CreateMap<BuyerModel, Buyer>()
            .ReverseMap();
        
        CreateMap<OrderItem, OrderItemModel>()
            .AfterMap<CartItemModelActions>()
            .ReverseMap();
        
        CreateMap<OrderModel, Domain.Order>()
            .ForMember(x => x.CreatedAt, opt => opt.Ignore())
            .ForMember(x => x.Uid, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(x => x.CreatedAt));

        CreateMap<AddressModel, Address>()
            .ReverseMap();
        
        CreateMap<CartItem, OrderItem>()
            .ForMember(x => x.ProductName, opt => opt.MapFrom(x => x.Name))
            .ForMember(x => x.Price, opt => opt.MapFrom(x => x.HasDiscount ? x.DiscountPrice : x.Price))
            .ForMember(x => x.ImagePath, opt => opt.MapFrom(x => x.ImageUrl))
            .ReverseMap();
    }
    
    public class CartItemModelActions(IFileStorage fileStorage) : IMappingAction<OrderItem, OrderItemModel>
    {
        public void Process(OrderItem source, OrderItemModel destination, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.ImagePath))
                return;
            
            destination.DownloadUrl = fileStorage.GetDownloadLinkAsync(source.ImagePath).GetAwaiter().GetResult();
        }
    }
}