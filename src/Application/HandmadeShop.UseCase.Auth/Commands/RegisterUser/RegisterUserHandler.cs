using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using HandmadeShop.UseCase.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.RegisterUser;

internal sealed class RegisterUserHandler(
    UserManager<User> userManager,
    IMapper mapper,
    IAction action,
    IEmailService emailService)
    : ICommandHandler<RegisterUserCommand, UserAccountModel>
{
    public async Task<Result<UserAccountModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await userManager.FindByEmailAsync(request.Model.Email);
    
        if (existUser is not null)
        {
            return UserErrors.SameUser;
        }
    
        var user = new User
        {
            Name = request.Model.Name,
            Email = request.Model.Email,
            UserName = request.Model.Email,
            Status = UserStatus.Active,
            PasswordHash = request.Model.Password,
            Gender = Gender.Unknown,
        };
    
        user.PasswordHash = userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
    
        var result = await userManager.CreateAsync(user);
    
        if (!result.Succeeded)
        {
            var message = $"Creating user account is wrong. " +
                        $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.CreateError(message);
        }
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var email = await emailService.GetVerificationEmail(user, token);

        await action.SendEmail(email);
    
        return mapper.Map<UserAccountModel>(user);
    }
}