using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;

internal sealed class ResetProfilePasswordHandler(
    UserManager<User> userManager,
    IIdentityService identityService)
    : ICommandHandler<ResetProfilePasswordCommand>
{
    public async Task<Result> Handle(ResetProfilePasswordCommand request, CancellationToken cancellationToken)
    {
        var userid = identityService.GetUserIdentity();
        var user = await userManager.FindByIdAsync(userid.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(userid);
        }

        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, request.Model.Password);

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var message = $"Confirmation user email is wrong. " +
                          $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.ResetPassword(message);
        }
        
        return Result.Success();
    }
}