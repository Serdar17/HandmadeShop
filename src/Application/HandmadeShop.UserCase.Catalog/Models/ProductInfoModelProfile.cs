using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Catalogs.Models;

namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductInfoModelProfile : Profile
{
    public ProductInfoModelProfile()
    {
        CreateMap<Product, ProductInfoModel>()
            .ForMember(x => x.CatalogName, opt => opt.MapFrom(x => x.Catalog.Name))
            .ForMember(x => x.ReviewCount, opt => opt.MapFrom(x => x.Reviews.Count))
            .ForMember(x => x.Rating, opt => opt.MapFrom(p => p.Reviews.Count == 0 ? 0 : p.Reviews.Sum(r => r.Rating) / p.Reviews.Count))
            .AfterMap<ProductInfoModelActions>();
    }

    public class ProductInfoModelActions : IMappingAction<Product, ProductInfoModel>
    {
        private readonly IFileStorage _fileStorage;
    
        public ProductInfoModelActions(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public void Process(Product source, ProductInfoModel destination, ResolutionContext context)
        {
            if (source.Images.Count == 0)
                return;

            foreach (var image in source.Images)
            {
                destination.ImageUrls.Add(_fileStorage.GetDownloadLinkAsync(image).GetAwaiter().GetResult());
            }
        }
    }
}