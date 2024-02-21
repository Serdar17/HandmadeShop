using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.Web.Pages.Review.Services;

public interface IReviewService
{
    Task<Result<IEnumerable<ReviewInfoModel>>> GetProductReviews(Guid productId);
    Task<Result<ReviewInfoModel>> AddReviewAsync(ReviewModel model);
    Task<Result> RemoveReviewAsync(Guid reviewId);
    Task<Result> AddFavoriteAsync(AddFavoriteModel model);
    Task<Result> RemoveFavoriteAsync(RemoveFavoriteModel model);
}