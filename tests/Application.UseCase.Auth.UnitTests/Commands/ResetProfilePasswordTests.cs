using Application.UseCase.Auth.UnitTests.TestModels;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Auth.Commands.ResetProfilePassword;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Auth.UnitTests.Commands;

public class ResetProfilePasswordTests
{
    private static readonly ResetProfilePasswordCommand Command = new(TestUserModels.TestResetProfilePasswordModel);
    private static readonly User FakeUser = TestUserModels.TestUser;

    private readonly UserManager<User> _userManagerMock;
    private readonly IIdentityService _identityServiceMock;
    private readonly ResetProfilePasswordHandler _handler;

    public ResetProfilePasswordTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _identityServiceMock = Substitute.For<IIdentityService>();
        _handler = new ResetProfilePasswordHandler(_userManagerMock, _identityServiceMock);
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
    public async Task Handle_ShouldReturnUserError_WhenUserUpdateWasFailed()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.UpdateAsync(Arg.Any<User>()).Returns(IdentityResult.Failed());
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.Conflict);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnSuccessResult_WhenUserUpdateWasFailed()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).Returns(FakeUser);
        _userManagerMock.UpdateAsync(Arg.Any<User>()).Returns(IdentityResult.Success);
        
        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().Be(true);
    }
}