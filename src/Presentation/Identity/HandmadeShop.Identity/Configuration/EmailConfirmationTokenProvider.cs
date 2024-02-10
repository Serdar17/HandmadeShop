using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HandmadeShop.Identity.Configuration;

public class EmailConfirmationTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public EmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider, 
        IOptions<EmailConfirmationTokenProviderOptions> options, 
        ILogger<DataProtectorTokenProvider<TUser>> logger)
        : base(dataProtectionProvider, options, logger)
    {
    }

    // public override async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    // {
    //     if (user == null)
    //     {
    //         throw new ArgumentNullException(nameof(user));
    //     }
    //
    //     var userId = await manager.GetUserIdAsync(user);
    //     var token = new Random().NextInt64(1000, 9999).ToString();
    //     await _cache.Put(userId, token);
    //     return token;
    // }
    //
    // public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    // {
    //     if (user == null)
    //     {
    //         throw new ArgumentNullException(nameof(user));
    //     }
    //     
    //     var tokenFromCache = await _cache.Get<string>(manager.GetUserIdAsync(user).ToString());
    //     
    //     return string.Equals(token, tokenFromCache);
    // }
}

public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
}