using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.UseCase.Account.Queries.GetAllFavorite;

internal sealed class GetAllFavoriteHandler : IQueryHandler<GetAllFavoriteQuery, IEnumerable<Guid>>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;

    public GetAllFavoriteHandler(UserManager<User> userManager, IIdentityService identityService)
    {
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<Result<IEnumerable<Guid>>> Handle(GetAllFavoriteQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        return Result<IEnumerable<Guid>>.Success(user.UserLikes.Select(x => x.Like.Product.Uid));
    }
}