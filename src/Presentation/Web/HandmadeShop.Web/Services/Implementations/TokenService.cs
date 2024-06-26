﻿using System.Text.Json;
using Blazored.LocalStorage;
using HandmadeShop.Web.Pages.Auth.Models;
using Microsoft.VisualBasic;

namespace HandmadeShop.Web.Services;

public class TokenService(
    ILocalStorageService localStorage,
    IHttpClientFactory clientFactory)
    : ITokenService
{
    private const string LocalStorageAuthTokenKey = "authToken";
    private const string LocalStorageRefreshTokenKey = "refreshToken";

    public async Task<LoginResult> RefreshTokenAsync()
    {
        var httpClient = clientFactory.CreateClient(Settings.Identity);
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

        var response = await httpClient.PostAsync("connect/token", requestContent);

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
}