using Application.UseCase.Auth.UnitTests.TestModels;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Auth.Commands.ResetPassword;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class ResetPasswordCommandTests
{
    private static readonly ResetPasswordCommand Command = new(TestUserModels.TestResetPasswordModel);
    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly UserManager<User> _userManagerMock;
    private readonly ResetPasswordHandler _handler;

    public ResetPasswordCommandTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _handler = new ResetPasswordHandler(_userManagerMock);
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
    public async Task Handle_ShouldReturnUserError_WhenResetPasswordWasFailed()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.ResetPasswordAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(IdentityResult.Failed());
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenResetPasswordWasSuccess()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.ResetPasswordAsync(Arg.Any<User>(), Arg.Any<string>(), Arg.Any<string>())
            .Returns(IdentityResult.Success);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
}