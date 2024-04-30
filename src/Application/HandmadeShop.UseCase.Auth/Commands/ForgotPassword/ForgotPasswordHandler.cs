using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ForgotPassword;

internal sealed class ForgotPasswordHandler(
    UserManager<User> userManager,
    IEmailService emailService,
    IAction action)
    : ICommandHandler<ForgotPasswordCommand>
{
    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Model.Email);

        if (user is null)
        {
            return UserErrors.NotFoundByEmail(request.Model.Email);
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var email = await emailService.GetResetPasswordEmail(user, token);
        
        await action.SendEmail(email);

        return Result.Success();
    }
}