using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ResetPassword;

internal sealed class ResetPasswordHandler : ICommandHandler<ResetPasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ResetPasswordHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Model.Email);

        if (user is null)
        {
            return UserErrors.NotFoundByEmail(request.Model.Email);
        }

        var result = await _userManager.ResetPasswordAsync(user, request.Model.Token, request.Model.Password);
        
        if (!result.Succeeded)
        {
            var message = $"Confirmation user email is wrong. " +
                          $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.ResetPassword(message);
        }

        return Result.Success();
    }
}