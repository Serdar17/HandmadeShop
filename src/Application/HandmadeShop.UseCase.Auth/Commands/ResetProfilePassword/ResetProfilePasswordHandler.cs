using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;

public class ResetProfilePasswordHandler : ICommandHandler<ResetProfilePasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;

    public ResetProfilePasswordHandler(UserManager<User> userManager, IIdentityService identityService)
    {
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<Result> Handle(ResetProfilePasswordCommand request, CancellationToken cancellationToken)
    {
        var userid = _identityService.GetUserIdentity();
        var user = await _userManager.FindByIdAsync(userid.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userid);
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Model.Password);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var message = $"Confirmation user email is wrong. " +
                          $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.ResetPassword(message);
        }
        
        return Result.Success();
    }
}