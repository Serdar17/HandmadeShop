using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.RemoveFavorite;

public class RemoveFavoriteHandler : ICommandHandler<RemoveFavoriteCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;
    private readonly UserManager<User> _userManager;

    public RemoveFavoriteHandler(
        IUnitOfWork unitOfWork,
        IIdentityService identityService,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result> Handle(RemoveFavoriteCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Model.ProductId, cancellationToken);
        if (product is null)
        {
            return ProductErrors.NotFound(request.Model.ProductId);
        }

        if (product.Like is null || !await _unitOfWork.LikeRepository.HasLikeAsync(userId, product.Like.Id))
        {
            return Result.Success();
        }
        
        product.RemoveLike();
        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.LikeRepository.RemoveLikeAsync(userId, product.Like.Id);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}