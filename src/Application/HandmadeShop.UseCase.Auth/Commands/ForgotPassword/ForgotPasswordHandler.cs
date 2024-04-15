using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Email;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.ForgotPassword;

internal sealed class ForgotPasswordHandler : ICommandHandler<ForgotPasswordCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailService _emailService;
    private readonly IAction _action;

    public ForgotPasswordHandler(
        UserManager<User> userManager,
        IEmailService emailService,
        IAction action)
    {
        _userManager = userManager;
        _emailService = emailService;
        _action = action;
    }

    public async Task<Result> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Model.Email);

        if (user is null)
        {
            return UserErrors.NotFoundByEmail(request.Model.Email);
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var email = await _emailService.GetResetPasswordEmail(user, token);
        
        await _action.SendEmail(email);

        return Result.Success();
    }
}