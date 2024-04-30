using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Models;

public class ReviewInfoModelProfile : Profile
{
    public ReviewInfoModelProfile()
    {
        CreateMap<Domain.Review, ReviewInfoModel>()
            .AfterMap<ReviewInfoModelActions>();
        CreateMap<User, OwnerModel>();
    }
    
    public class ReviewInfoModelActions(IFileStorage fileStorage) : IMappingAction<Domain.Review, ReviewInfoModel>
    {
        public void Process(Domain.Review source, ReviewInfoModel destination, ResolutionContext context)
        {
            foreach (var image in source.Images)
            {
                var downloadUrl = fileStorage.GetDownloadLinkAsync(image).GetAwaiter().GetResult();
                destination.ImageUrls.Add(downloadUrl);
            }
        }
    }
}