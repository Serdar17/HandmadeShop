using System.Net;
using System.Net.Http.Headers;
using Blazored.LocalStorage;
using HandmadeShop.Web.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Handlers;

public class AuthenticationDelegatingHandler(
    ILocalStorageService localStorage,
    NavigationManager navigationManager,
    ITokenService tokenService)
    : DelegatingHandler
{
    private bool _isRefreshed;


    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Console.WriteLine("interceptor called");
        var accessToken = await localStorage.GetItemAsStringAsync("authToken", cancellationToken);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken?.Replace("\"", ""));
        var response = await base.SendAsync(request, cancellationToken);
        
        if (!_isRefreshed && response.StatusCode == HttpStatusCode.Unauthorized)
        {
            try
            {
                _isRefreshed = true;
                var tokenResult = await tokenService.RefreshTokenAsync();
        
                if (tokenResult.Successful)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResult.AccessToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    navigationManager.NavigateTo("/logout");
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