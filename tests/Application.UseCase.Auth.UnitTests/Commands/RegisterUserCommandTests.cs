using Application.UseCase.Auth.UnitTests.TestModels;
using Application.UseCase.Auth.UnitTests.Utils;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Email;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.Actions;
using HandmadeShop.UseCase.Auth.Commands.RegisterUser;
using HandmadeShop.UseCase.Auth.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class RegisterUserCommandTests
{
    private static readonly RegisterUserCommand Command = new (TestUserModels.RegisterUserModelTest);
    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly IAction _actionMock;
    private readonly IEmailService _emailServiceMock;
    private readonly UserManager<User> _userManagerMock;
    private readonly RegisterUserHandler _handler;

    public RegisterUserCommandTests()
    {
        _actionMock = Substitute.For<IAction>();
        _emailServiceMock = Substitute.For<IEmailService>();
        _userManagerMock = TestUserModels.GetMockUserManager();
        var mapper = MapperUtil.GetTypedMapper(typeof(RegisterUserModelProfile), typeof(UserAccountModelProfile));
        _handler = new RegisterUserHandler(_userManagerMock, mapper, _actionMock, _emailServiceMock);
    }

    [Fact]
    public async Task Handler_ShouldReturnUserError_WhenEmailIsNotUnique()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserError_WhenCreatedWasFailed()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).ReturnsNull();
        _userManagerMock.CreateAsync(Arg.Any<User>()).Returns(IdentityResult.Failed());
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public async Task Handler_ShouldSendEmailNotification_WhenCreateWasSuccess()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).ReturnsNull();
        _userManagerMock.CreateAsync(Arg.Any<User>()).Returns(IdentityResult.Success);
        _userManagerMock.GenerateEmailConfirmationTokenAsync(Arg.Any<User>()).Returns("token");
        _emailServiceMock.GetVerificationEmail(Arg.Any<User>(), Arg.Any<string>()).Returns(TestEmailModels.TestEmailModel);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().Be(true);
        await _actionMock.Received(1).SendEmail(Arg.Any<EmailModel>());
    }
}