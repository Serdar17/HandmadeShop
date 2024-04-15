using Application.UseCase.Auth.UnitTests.TestModels;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Auth.Commands.VerifyEmail;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class VerifyEmailCommandTests
{
    private static readonly VerifyEmailCommand Command = new (TestUserModels.TestVerifyEmailModel);
    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly UserManager<User> _userManagerMock;
    private readonly VerifyEmailHandler _handler;

    public VerifyEmailCommandTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _handler = new VerifyEmailHandler(_userManagerMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserError_WhenUserWasNotFound()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).ReturnsNull();
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserError_WhenTokenWasExpired()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.ConfirmEmailAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(IdentityResult.Failed());
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenTokenWasNotExpired()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.ConfirmEmailAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(IdentityResult.Success);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
}