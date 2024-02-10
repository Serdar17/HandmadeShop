using Microsoft.AspNetCore.Identity;

namespace HandmadeShop.Identity.Configuration;

public static class CustomIdentityBuilderExtensions
{
    public static IdentityBuilder AddCustomEmailTokenProvider(this IdentityBuilder builder)
    {
        var userType = builder.UserType;
        var totpProvider = typeof(EmailConfirmationTokenProvider<>).MakeGenericType(userType);
        return builder.AddTokenProvider(TokenOptions.DefaultEmailProvider, totpProvider);
    }
}