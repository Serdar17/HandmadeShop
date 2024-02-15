using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Common;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Auth.Models;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace HandmadeShop.Web.Pages.Auth.Services;

public class AuthService : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";
    
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorage;

    public AuthService(HttpClient httpClient,
                       AuthenticationStateProvider authenticationStateProvider,
                       ILocalStorageService localStorage)
    {
        _httpClient = httpClient;
        _authenticationStateProvider = authenticationStateProvider;
        _localStorage = localStorage;
    }

    public async Task<LoginResult> LoginAsync(LoginModel loginModel)
    {
        var url = $"{Settings.IdentityRoot}/connect/token";

        var form = new[] 
        {
            new KeyValuePair<string, string>("grant_type", "password"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("username", loginModel.Email),
            new KeyValuePair<string, string>("password", loginModel.Password)
        };

        var requestContent = new FormUrlEncodedContent(form);

        var response = await _httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await _localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await _localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.AccessToken);

        return loginResult;
    }

    public async Task<Result> RegisterAsync(RegisterModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/auth/register";
        
        var json = JsonSerializer.Serialize(model);
        
        return await SendAsync(url, json);
    }

    public async Task<Result> VerifyEmailAsync(VerifyEmailModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/auth/verify-email";

        var json = JsonSerializer.Serialize(model);
        
        return await SendAsync(url, json);
    }

    public async Task<Result> ForgotPasswordAsync(ForgotPasswordModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/auth/password/forgot";

        var json = JsonSerializer.Serialize(model);

        return await SendAsync(url, json);
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/auth/password/reset";

        var json = JsonSerializer.Serialize(model);
        
        return await SendAsync(url, json);
    }

    public async Task<Result> ResetProfilePasswordAsync(ResetProfilePasswordModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/auth/password/reset";

        var json = JsonSerializer.Serialize(model);

        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(url, data);

        if (response.IsSuccessStatusCode)
            return Result.Success();
        
        var error = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ErrorResult>(error, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ErrorResult();

        return result.Errors.First();
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await _localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private async Task<Result> SendAsync(string url, string json)
    {
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, data);

        if (response.IsSuccessStatusCode)
            return Result.Success();

        return await response.Content.ToErrorAsync();
    }
}