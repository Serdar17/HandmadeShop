using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;

internal sealed class UpdateAccountInfoHandler : ICommandHandler<UpdateAccountInfoCommand, AccountInfoModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IIdentityService _identityService;

    public UpdateAccountInfoHandler(
        UserManager<User> userManager, 
        IMapper mapper, 
        IIdentityService identityService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
    }

    public async Task<Result<AccountInfoModel>> Handle(UpdateAccountInfoCommand request, CancellationToken cancellationToken)
    {
        var userId = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userId);
        }

        _mapper.Map(request.Model, user);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return UserErrors.UpdateError(string.Join(", ", result.Errors.Select(s => s.Description)));
        }

        return _mapper.Map<AccountInfoModel>(user);
    }
}