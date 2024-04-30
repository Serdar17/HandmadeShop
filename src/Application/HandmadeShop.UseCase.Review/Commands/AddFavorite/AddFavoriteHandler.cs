using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.AddFavorite;

internal sealed class AddFavoriteHandler(
    IUnitOfWork unitOfWork,
    IIdentityService identityService,
    UserManager<User> userManager)
    : ICommandHandler<AddFavoriteCommand>
{
    public async Task<Result> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
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

        if (product.Like is not null && await unitOfWork.LikeRepository.HasLikeAsync(userId, product.Like.Id))
        {
            return Result.Success();
        }
        
        product.AddLike(user);

        await unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}