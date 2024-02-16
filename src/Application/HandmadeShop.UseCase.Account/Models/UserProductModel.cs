using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;

namespace HandmadeShop.UseCase.Account.Models;

public class UserProductModel
{
    public Guid Id { get; set; }

    public List<UserProductInfoModel> Products { get; set; } = new();

}

public class UserProductInfoModel
{
    public Guid Uid { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool HasDiscount { get; set; }
    public decimal? DiscountPercentage { get; set; }
    
    public string? DownloadImage { get; set; }
}

public class UserProductModelProfile : Profile
{
    public UserProductModelProfile()
    {
        CreateMap<User, UserProductModel>();

        CreateMap<Product, UserProductInfoModel>()
            .AfterMap<UserProductInfoModelActions>();
    }
    
    public class UserProductInfoModelActions : IMappingAction<Product, UserProductInfoModel>
    {
        private readonly IFileStorage _fileStorage;

        public UserProductInfoModelActions(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public void Process(Product source, UserProductInfoModel destination, ResolutionContext context)
        {
            if (source.Images.Count == 0)
                return;

            var path = source.Images.First();

            destination.DownloadImage = _fileStorage.GetDownloadLinkAsync(path).GetAwaiter().GetResult();
        }
    }
}