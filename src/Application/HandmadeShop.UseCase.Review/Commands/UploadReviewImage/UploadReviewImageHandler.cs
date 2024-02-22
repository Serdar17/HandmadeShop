using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Common.Constants;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Commands.UploadReviewImage;

internal sealed class UploadReviewImageHandler : ICommandHandler<UploadReviewImageCommand, UploadedReviewImage>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileStorage _fileStorage;

    public UploadReviewImageHandler(IUnitOfWork unitOfWork, IFileStorage fileStorage)
    {
        _unitOfWork = unitOfWork;
        _fileStorage = fileStorage;
    }

    public async Task<Result<UploadedReviewImage>> Handle(UploadReviewImageCommand request, CancellationToken cancellationToken)
    {
        var review = await _unitOfWork.ReviewRepository.GetByIdAsync(request.Model.ReviewId, cancellationToken);

        if (review is null)
        {
            return ReviewErrors.NotFound(request.Model.ReviewId);
        }

        var path = await _fileStorage.UploadAsync(
            review.Uid,
            request.Model.Image,
            FolderPaths.PathToReviewImagesFolder,
            cancellationToken
        );
        
        review.Images.Add(path);
        
        await _unitOfWork.ReviewRepository.UpdateAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return new UploadedReviewImage
        {
            DownloadUrl = await _fileStorage.GetDownloadLinkAsync(path, cancellationToken),
        };
    }
}