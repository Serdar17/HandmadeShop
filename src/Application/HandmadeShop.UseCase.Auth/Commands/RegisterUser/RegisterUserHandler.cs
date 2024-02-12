using AutoMapper;
using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Actions;
using HandmadeShop.Domain.Common;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Auth.Models;
using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.UseCase.Auth.Commands.RegisterUser;

internal sealed class RegisterUserHandler : ICommandHandler<RegisterUserCommand, UserAccountModel>
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly IAction _action;
    private readonly IEmailService _emailService;

    public RegisterUserHandler(
        UserManager<User> userManager, 
        IMapper mapper,
        IAction action, 
        IEmailService emailService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _action = action;
        _emailService = emailService;
    }

    public async Task<Result<UserAccountModel>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _userManager.FindByEmailAsync(request.Model.Email);
    
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
            PasswordHash = request.Model.Password
        };
    
        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.PasswordHash);
    
        var result = await _userManager.CreateAsync(user);
    
        if (!result.Succeeded)
        {
            var message = $"Creating user account is wrong. " +
                        $"{string.Join(", ", result.Errors.Select(s => s.Description))}";
            return UserErrors.CreateError(message);
        }
        
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var email = await _emailService.GetVerificationEmail(user, token);

        await _action.SendEmail(email);
    
        return _mapper.Map<UserAccountModel>(user);
    }
}