using Application.UseCase.Account.UnitTests.TestModels;
using Application.UseCase.Account.UnitTests.Utils;
using FluentAssertions;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Infrastructure.Abstractions.Identity;
using HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UseCase.Account.UnitTests.Commands;

public class UpdateAccountInfoCommandTests
{
    private static readonly UpdateAccountInfoCommand Command = new (TestUserModels.TestUpdateAccountInfoModel);

    private UserManager<User> _userManagerMock;
    private IIdentityService _identityServiceMock;
    private ICacheService _cacheServiceMock;
    private readonly UpdateAccountInfoHandler _handler;

    public UpdateAccountInfoCommandTests()
    {
        _userManagerMock = TestUserModels.GetMockUserManager();
        _identityServiceMock = Substitute.For<IIdentityService>();
        _cacheServiceMock = Substitute.For<ICacheService>();
        var mapper = MapperUtil.GetTypedMapper(typeof(AccountInfoModelProfile), typeof(UpdateAccountInfoModelProfile));
        _handler = new UpdateAccountInfoHandler(_userManagerMock, mapper, _identityServiceMock, _cacheServiceMock);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserWasNotFound()
    {
        // Arrange
        _userManagerMock.FindByIdAsync(Arg.Any<string>()).ReturnsNull();

        // Act
        var result = await _handler.Handle(Command, CancellationToken.None);
        
        // Assert
        result.IsFailure.Should().Be(true);
        result.Error.ErrorType.Should().Be(ErrorType.NotFound);
    }
}