using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;

namespace HandmadeShop.UseCase.Review.Commands.RemoveReview;

internal sealed class RemoveReviewHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService)
    : ICommandHandler<RemoveReviewCommand>
{
    public async Task<Result> Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var review = await unitOfWork.ReviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
        
        if (review is null)
        {
            return ReviewErrors.NotFound(request.ReviewId);
        }

        if (review.Owner.Id != userId)
        {
            return ReviewErrors.Prohibition();
        }

        await unitOfWork.ReviewRepository.DeleteByIdAsync(review.Uid, cancellationToken);

        return Result.Success();
    }
}