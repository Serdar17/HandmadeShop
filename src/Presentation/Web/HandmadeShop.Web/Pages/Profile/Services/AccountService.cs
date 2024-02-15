using System.Text;
using System.Text.Json;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Services;
using Microsoft.AspNetCore.Components.Forms;

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
        
        return await response.Content.ToErrorAsync();
    }

    public async Task<Result<AccountInfoModel>> UpdateAccountInfoModel(AccountInfoModel model)
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/info";

        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _httpClient.PutAsync(url, data);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            var newModel = JsonSerializer.Deserialize<AccountInfoModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (newModel is null)
                return model;
            
            return newModel;
        }
        
        return await response.Content.ToErrorAsync();
    }

    public async Task<Result<AccountInfoModel>> UploadAvatarAsync(MultipartFormDataContent form)
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/avatar/upload";
        
        // var form = new MultipartFormDataContent();
        // var fileContent = new StreamContent(file.OpenReadStream());
        // form.Add(fileContent, "avatar", file.Name);
        //
        var response = await _httpClient.PostAsync(url, form);
        
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<AccountInfoModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return await response.Content.ToErrorAsync();
    }

    public async Task<Result<AccountInfoModel>> DeleteAvatarAsync()
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/avatar/delete";

        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        { 
            return JsonSerializer.Deserialize<AccountInfoModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return await response.Content.ToErrorAsync();
    }
}