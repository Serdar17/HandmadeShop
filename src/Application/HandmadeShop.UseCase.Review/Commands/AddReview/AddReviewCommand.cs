using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Commands.AddReview;

public sealed record AddReviewCommand(ReviewModel Model) : ICommand<ReviewInfoModel>;