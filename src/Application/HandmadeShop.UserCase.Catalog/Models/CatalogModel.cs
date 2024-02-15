using AutoMapper;

namespace HandmadeShop.UserCase.Catalog.Models;

public class CatalogModel
{
    public required string Name { get; set; }
}

public class CatalogModelProfile : Profile
{
    public CatalogModelProfile()
    {
        CreateMap<Domain.Catalog, CatalogModel>();
    }
}