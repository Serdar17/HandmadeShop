using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Accounts.Models;

namespace HandmadeShop.UseCase.Account.Models;

public class UserProductModelProfile : Profile
{
    public UserProductModelProfile()
    {
        CreateMap<User, UserProductModel>();

        CreateMap<Product, UserProductInfoModel>()
            .AfterMap<UserProductInfoModelActions>();
    }
    
    public class UserProductInfoModelActions(IFileStorage fileStorage) : IMappingAction<Product, UserProductInfoModel>
    {
        public void Process(Product source, UserProductInfoModel destination, ResolutionContext context)
        {
            if (source.Images.Count == 0)
                return;

            var path = source.Images.First();

            destination.DownloadImage = fileStorage.GetDownloadLinkAsync(path).GetAwaiter().GetResult();
        }
    }
}