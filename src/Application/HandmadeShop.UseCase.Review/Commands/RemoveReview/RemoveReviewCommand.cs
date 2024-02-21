using HandmadeShop.Application.Abstraction.Messaging;

namespace HandmadeShop.UseCase.Review.Commands.RemoveReview;

public sealed record RemoveReviewCommand(Guid ReviewId) : ICommand;