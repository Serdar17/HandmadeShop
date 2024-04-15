using Application.UseCase.Auth.UnitTests.TestModels;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Auth.Commands.ChangePassword;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class ChangePasswordCommandTests
{
    private static readonly ChangePasswordCommand Command = new(TestUserModels.TestChangePasswordModel);
    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly UserManager<User> _userManagerMock;
    private readonly ChangePasswordHandler _handler;

    public ChangePasswordCommandTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _handler = new ChangePasswordHandler(_userManagerMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnUserError_WhenUserWasNotFoundByEmail()
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
    public async Task Handle_ShouldReturnUserError_WhenOldPasswordWrong()
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
    public async Task Handle_ShouldReturnUserError_WhenUserUpdateWasFailed()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.CheckPasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(true);
        _userManagerMock.UpdateAsync(Arg.Any<User>()).Returns(IdentityResult.Failed());
         
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenUpdateWasSuccess()
    {
        // Arrange
        _userManagerMock.FindByEmailAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.CheckPasswordAsync(Arg.Any<User>(), Arg.Any<string>()).Returns(true);
        _userManagerMock.UpdateAsync(Arg.Any<User>()).Returns(IdentityResult.Success);
         
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
}