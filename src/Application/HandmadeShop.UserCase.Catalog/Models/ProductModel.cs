using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;

namespace HandmadeShop.UserCase.Catalog.Models;

public class ProductModel
{
    public Guid Uid { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    
    public string CatalogName { get; set; }
    public string DownloadUrl { get; set; }
    // public List<ProductImageModel> Images { get; set; } = new();
}

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