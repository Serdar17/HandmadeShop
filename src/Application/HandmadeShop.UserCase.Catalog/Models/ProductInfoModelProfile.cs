using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductInfoModelProfile : Profile
{
    public ProductInfoModelProfile()
    {
        CreateMap<Product, ProductInfoModel>()
            .ForMember(x => x.CatalogName, opt => opt.MapFrom(x => x.Catalog.Name));
    }
}