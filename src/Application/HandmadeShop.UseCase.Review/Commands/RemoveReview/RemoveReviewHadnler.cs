using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;

namespace HandmadeShop.UseCase.Review.Commands.RemoveReview;

internal sealed class RemoveReviewHandler : ICommandHandler<RemoveReviewCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public RemoveReviewHandler(
        IUnitOfWork unitOfWork, 
        IIdentityService identityService)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
    }

    public async Task<Result> Handle(RemoveReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var review = await _unitOfWork.ReviewRepository.GetByIdAsync(request.ReviewId, cancellationToken);
        
        if (review is null)
        {
            return ReviewErrors.NotFound(request.ReviewId);
        }

        if (review.Owner.Id != userId)
        {
            return ReviewErrors.Prohibition();
        }

        await _unitOfWork.ReviewRepository.DeleteByIdAsync(review.Uid, cancellationToken);

        return Result.Success();
    }
}