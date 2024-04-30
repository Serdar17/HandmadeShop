using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.FileStorage;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Reviews.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.AddReview;

internal sealed class AddReviewHandler(
    IUnitOfWork unitOfWork,
    UserManager<User> userManager,
    IIdentityService identityService,
    IMapper mapper,
    IFileStorage fileStorage)
    : ICommandHandler<AddReviewCommand, ReviewInfoModel>
{
    private readonly IFileStorage _fileStorage = fileStorage;

    public async Task<Result<ReviewInfoModel>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var userId = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }
        
        var product = await unitOfWork.ProductRepository.GetByIdAsync(request.Model.ProductId, cancellationToken);

        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.ProductId);
        }

        var review = mapper.Map<Domain.Review>(request.Model);
        review.Owner = user;
        product.Reviews.Add(review);

        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<ReviewInfoModel>(review);
    }
}