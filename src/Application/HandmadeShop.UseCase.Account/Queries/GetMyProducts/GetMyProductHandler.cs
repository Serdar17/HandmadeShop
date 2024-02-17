using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.SharedModel.Accounts.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Queries.GetMyProducts;

internal sealed class GetMyProductHandler : IQueryHandler<GetMyProductQuery, UserProductModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public GetMyProductHandler(UserManager<User> userManager, IMapper mapper, IIdentityService identityService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Result<UserProductModel>> Handle(GetMyProductQuery request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();

        var user = await _userManager.FindByIdAsync(userId.ToString());
        
        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        return _mapper.Map<UserProductModel>(user);
    }
}