using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Commands.UploadReviewImage;

internal sealed class UploadReviewImageHandler(IUnitOfWork unitOfWork, IFileStorage fileStorage)
    : ICommandHandler<UploadReviewImageCommand, UploadedReviewImage>
{
    public async Task<Result<UploadedReviewImage>> Handle(UploadReviewImageCommand request, CancellationToken cancellationToken)
    {
        var review = await unitOfWork.ReviewRepository.GetByIdAsync(request.Model.ReviewId, cancellationToken);

        if (review is null)
        {
            return ReviewErrors.NotFound(request.Model.ReviewId);
        }

        var path = await fileStorage.UploadAsync(
            review.Uid,
            request.Model.Image,
            FolderPaths.PathToReviewImagesFolder,
            cancellationToken
        );
        
        review.Images.Add(path);
        
        await unitOfWork.ReviewRepository.UpdateAsync(review, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);


        return new UploadedReviewImage
        {
            DownloadUrl = await fileStorage.GetDownloadLinkAsync(path, cancellationToken),
        };
    }
}