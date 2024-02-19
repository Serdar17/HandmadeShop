using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Review.Commands.AddFavorite;

internal sealed class AddFavoriteHandler : ICommandHandler<AddFavoriteCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IIdentityService _identityService;

    public AddFavoriteHandler(
        IUnitOfWork unitOfWork,
        IIdentityService identityService,
        UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _identityService = identityService;
        _userManager = userManager;
    }

    public async Task<Result> Handle(AddFavoriteCommand request, CancellationToken cancellationToken)
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

        if (await _unitOfWork.LikeRepository.HasLikeAsync(userId))
        {
            return Result.Success();
        }
        
        product.AddLike(user);

        await _unitOfWork.ProductRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}