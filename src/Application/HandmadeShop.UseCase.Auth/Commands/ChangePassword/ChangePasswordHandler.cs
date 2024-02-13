using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ChangePassword;

internal sealed class ChangePasswordHandler : ICommandHandler<ChangePasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ChangePasswordHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Model.Email);

        if (user is null)
        {
            return UserErrors.NotFoundByEmail(request.Model.Email);
        }

        var isCorrect = await _userManager.CheckPasswordAsync(user, request.Model.OldPassword);

        if (!isCorrect)
        {
            return UserErrors.InvalidPassword();
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.Model.NewPassword);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var message = $"Updating user is wrong. " +
                          $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.CreateError(message);
        }

        return Result.Success();
    }
}