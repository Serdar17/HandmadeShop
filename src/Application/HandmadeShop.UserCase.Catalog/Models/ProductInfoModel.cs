using AutoMapper;
using HandmadeShop.Domain;

namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductInfoModel
{
    public Guid Uid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? DiscountPercentage { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public required string CatalogName { get; set; }
}

public class ProductInfoModelProfile : Profile
{
    public ProductInfoModelProfile()
    {
        CreateMap<Product, ProductInfoModel>()
            .ForMember(x => x.CatalogName, opt => opt.MapFrom(x => x.Catalog.Name));
    }
}