using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.RemoveFavorite;

public class RemoveFavoriteHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    UserManager<User> userManager)
    : ICommandHandler<RemoveFavoriteCommand>
{
    public async Task<Result> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
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

        if (product.Like is null || !await unitOfWork.LikeRepository.HasLikeAsync(userId, product.Like.Id))
        {
            return Result.Success();
        }
        
        product.RemoveLike();
        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.LikeRepository.RemoveLikeAsync(userId, product.Like.Id);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}