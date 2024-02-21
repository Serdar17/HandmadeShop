using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Queries.GetProductReviews;

public sealed record GetProductReviewQuery(Guid ProductId) : IQuery<IEnumerable<ReviewInfoModel>>;