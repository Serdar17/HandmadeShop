using AutoMapper;
using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Models;

public class ReviewInfoModelProfile : Profile
{
    public ReviewInfoModelProfile()
    {
        CreateMap<Domain.Review, ReviewInfoModel>();
        CreateMap<User, OwnerModel>();
    }
}