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
    
    public class ProductModelActions : IMappingAction<Product, ProductModel>
    {
        private readonly IFileStorage _fileStorage;
    
        public ProductModelActions(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public void Process(Product source, ProductModel destination, ResolutionContext context)
        {
            if (source.Images.Count == 0)
                return;

            var path = source.Images.First();
            destination.DownloadUrl = _fileStorage.GetDownloadLinkAsync(path).GetAwaiter().GetResult();
        }
    }
}