using HandmadeShop.Domain.Email;

namespace Application.UseCase.Auth.UnitTests.TestModels;

public static class TestEmailModels
{
    public static EmailModel TestEmailModel => new ("subject", "fake body", "test@user.com");
}