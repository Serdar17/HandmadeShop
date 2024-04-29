﻿using HandmadeShop.Domain;
using HandmadeShop.Domain.Enums;
using HandmadeShop.UseCase.Account.Commands.UpdateAccountInfo;
using HandmadeShop.UseCase.Account.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace Application.UseCase.Account.UnitTests.TestModels;

public static class TestUserModels
{
    public static Guid TestUserId = Guid.Parse("5ecf0ba0-d93a-4580-a199-db2380006831");
    public static string TestName = "TestName";
    public static string TestEmail = "TestEmail";
    public static string TestPassword = "TestPassword";

    public static User TestUser => new()
    {
        Name = TestName,
        Email = TestEmail,
        PasswordHash = "SomeHash",
        EmailConfirmed = true,
        UserName = TestEmail,
        Status = UserStatus.Active,
    };

    public static UpdateAccountInfoModel TestUpdateAccountInfoModel => 
        new UpdateAccountInfoModel { Name =  TestName, Gender = Gender.Male, BirthDate = DateTime.Now };
    
    public static readonly IUserStore<User> Store = Substitute.For<IUserStore<User>>();
    public static readonly IPasswordHasher<User> Hasher = Substitute.For<IPasswordHasher<User>>();
    public static readonly IOptions<IdentityOptions> Options = Substitute.For<IOptions<IdentityOptions>>();
    
    public static UserManager<User> GetMockUserManager()
    {
        var userManagerMock =
            Substitute.For<UserManager<User>>(Store, Options, Hasher, null, null, null, null, null, null);

        return userManagerMock;
    }
}