using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductModelProfile : Profile
{
    public ProductModelProfile()
    {
        CreateMap<Product, ProductModel>()
            .ForMember(x => x.CatalogName, opt => opt.MapFrom(x => x.Catalog.Name))
            .AfterMap<ProductModelActions>();
    }
    
    public class ProductModelActions(IFileStorage fileStorage) : IMappingAction<Product, ProductModel>
    {
        public void Process(Product source, ProductModel destination, ResolutionContext context)
        {
            if (source.Images.Count == 0)
                return;

            var path = source.Images.First();
            destination.DownloadUrl = fileStorage.GetDownloadLinkAsync(path).GetAwaiter().GetResult();
        }
    }
}