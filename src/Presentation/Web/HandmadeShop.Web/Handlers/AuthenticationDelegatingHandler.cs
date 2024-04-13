using System.Net;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using HandmadeShop.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Handlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;
    private readonly ITokenService _tokenService;
    private bool _isRefreshed;
    
    public AuthenticationDelegatingHandler(
        ILocalStorageService localStorage,
        NavigationManager navigationManager, 
        ITokenService tokenService)
    {
        _localStorage = localStorage;
        _navigationManager = navigationManager;
        _tokenService = tokenService;
    }
    
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("interceptor called");
        var accessToken = await _localStorage.GetItemAsStringAsync("authToken", cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken?.Replace("\"", ""));
        var response = await base.SendAsync(request, cancellationToken);
        
        if (!_isRefreshed && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                _isRefreshed = true;
                var tokenResult = await _tokenService.RefreshTokenAsync();
        
                if (tokenResult.Successful)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    _navigationManager.NavigateTo("/logout");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                _isRefreshed = false;
            }
            
        }
        
        return response;
    }
}