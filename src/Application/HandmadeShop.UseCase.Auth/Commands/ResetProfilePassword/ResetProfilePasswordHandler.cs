using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;

public class ResetProfilePasswordHandler : ICommandHandler<ResetProfilePasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ResetProfilePasswordHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result> Handle(ResetProfilePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Model.UserId);

        if (user is null)
        {
            return UserErrors.NotFound(Guid.Parse(request.Model.UserId));
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