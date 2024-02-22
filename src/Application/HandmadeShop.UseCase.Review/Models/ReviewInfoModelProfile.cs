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
    
    public class ReviewInfoModelActions : IMappingAction<Domain.Review, ReviewInfoModel>
    {
        private readonly IFileStorage _fileStorage;
    
        public ReviewInfoModelActions(IFileStorage fileStorage)
        {
            _fileStorage = fileStorage;
        }
    
        public void Process(Domain.Review source, ReviewInfoModel destination, ResolutionContext context)
        {
            foreach (var image in source.Images)
            {
                var downloadUrl = _fileStorage.GetDownloadLinkAsync(image).GetAwaiter().GetResult();
                destination.ImageUrls.Add(downloadUrl);
            }
        }
    }
}