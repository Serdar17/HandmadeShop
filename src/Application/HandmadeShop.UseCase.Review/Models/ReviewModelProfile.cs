using AutoMapper;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Models;

public class ReviewModelProfile : Profile
{
    public ReviewModelProfile()
    {
        CreateMap<ReviewModel, Domain.Review>()
            .ForMember(x => x.ProductId, opt => opt.Ignore())
            .ForMember(x => x.Images, opt => opt.Ignore());
    }
}