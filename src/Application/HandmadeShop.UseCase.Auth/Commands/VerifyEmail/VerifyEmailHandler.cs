using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.VerifyEmail;

internal sealed class VerifyEmailHandler(UserManager<User> userManager) : ICommandHandler<VerifyEmailCommand>
{
    public async Task<Result> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Model.UserId.ToString());

        if (user is null)
        {
            return UserErrors.NotFound(request.Model.UserId);
        }

        var result = await userManager.ConfirmEmailAsync(user, request.Model.Token);

        if (!result.Succeeded)
        {
            return UserErrors.VerifyEmail(string.Join(", ", result.Errors.Select(s => s.Description)));
        }
        
        return Result.Success();
    }
}