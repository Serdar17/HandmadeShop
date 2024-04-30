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

public class AuthService(
    IHttpClientFactory factory,
    AuthenticationStateProvider authenticationStateProvider,
    ILocalStorageService localStorage)
    : IAuthService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";
    
    private readonly HttpClient _httpClient = factory.CreateClient(Settings.Api);

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

        await localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);

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

    public async Task<LoginResult> RefreshTokenAsync()
    {
        var url = $"{Settings.IdentityRoot}/connect/token";
        var refreshToken = await localStorage.GetItemAsStringAsync(LocalStorageRefreshTokenKey);
        refreshToken = refreshToken.Replace("\"", "");
        Console.WriteLine($"Refresh token is {refreshToken}");
        var formData = new[] 
        {
            new KeyValuePair<string, string>("grant_type", "refresh_token"),
            new KeyValuePair<string, string>("client_id", Settings.ClientId),
            new KeyValuePair<string, string>("client_secret", Settings.ClientSecret),
            new KeyValuePair<string, string>("refresh_token", refreshToken.Replace("\"", ""))
        };
        
        var requestContent = new FormUrlEncodedContent(formData);

        var response = await _httpClient.PostAsync(url, requestContent);

        var content = await response.Content.ReadAsStringAsync();

        var loginResult = JsonSerializer.Deserialize<LoginResult>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new LoginResult();
        loginResult.Successful = response.IsSuccessStatusCode;
        
        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }

        await localStorage.SetItemAsync(LocalStorageAuthTokenKey, loginResult.AccessToken);
        await localStorage.SetItemAsync(LocalStorageRefreshTokenKey, loginResult.RefreshToken);

        return loginResult;
    }

    public async Task Logout()
    {
        await localStorage.RemoveItemAsync(LocalStorageAuthTokenKey);
        await localStorage.RemoveItemAsync(LocalStorageRefreshTokenKey);

        ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    private async Task<Result> SendAsync(string url, string json)
    {
        var data = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, data);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
            return Result.Success();

        return content.ToError();
    }
}