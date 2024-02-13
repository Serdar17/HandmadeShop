using System.Text.Json;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Common;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Services;

namespace HandmadeShop.Web.Pages.Profile.Services;

public class AccountService : IAccountService
{
    private readonly HttpClient _httpClient;
    private readonly IIdentityService _identityService;
    private readonly IAuthService _authService;

    public AccountService(HttpClient httpClient, IIdentityService identityService, IAuthService authService)
    {
        _httpClient = httpClient;
        _identityService = identityService;
        _authService = authService;
    }

    public async Task<Result<AccountInfoModel>> GetAccountInfoAsync()
    {
        var id = await _identityService.GetClaimsPrincipalData();

        if (string.IsNullOrEmpty(id))
        {
            return UserErrors.NotAuthorized();
        }

        var url = $"{Settings.ApiRoot}/api/v1/accounts/info/{id}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            var model = JsonSerializer.Deserialize<AccountInfoModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (model is null)
                return UserErrors.NotFound(Guid.Parse(id));
            
            return model;
        }
        
        var error = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ErrorResult>(error, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ErrorResult();

        return result.Errors.First();
    }
}