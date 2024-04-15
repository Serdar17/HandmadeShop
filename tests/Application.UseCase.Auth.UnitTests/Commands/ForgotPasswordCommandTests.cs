using Application.UseCase.Auth.UnitTests.TestModels;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using HandmadeShop.UseCase.Auth.Commands.ForgotPassword;
using HandmadeShop.UseCase.Auth.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class ForgotPasswordCommandTests
{
    private static readonly ForgotPasswordCommand Command = new(new ForgotPasswordModel
        { Email = TestUserModels.TestEmail });

    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly UserManager<User> _userManagerMock;
    private readonly IEmailService _emailServiceMock;
    private readonly IAction _actionMock;
    private readonly ForgotPasswordHandler _handler;

    public ForgotPasswordCommandTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _emailServiceMock = Substitute.For<IEmailService>();
        _actionMock = Substitute.For<IAction>();
        _handler = new ForgotPasswordHandler(_userManagerMock, _emailServiceMock, _actionMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserError_WhenUserWasNotFound()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
    }
    
    [Fact]
    public async Task Handle_ShouldSendNotification_WhenUserWasFound()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.GeneratePasswordResetTokenAsync(Arg.Any<User>()).Returns("token");
        _emailServiceMock.GetResetPasswordEmail(Arg.Any<User>(), Arg.Any<string>()).Returns(TestEmailModels.TestEmailModel);

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
        await _actionMock.Received(1).SendEmail(Arg.Any<EmailModel>());
    }
}