﻿using System.Text;
using System.Text.Json;
using HandmadeShop.Domain;
using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Accounts.Models;
using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Auth.Services;
using HandmadeShop.Web.Pages.Profile.Models;
using HandmadeShop.Web.Services;

namespace HandmadeShop.Web.Pages.Profile.Services;

public class AccountService(IHttpClientFactory factory, IIdentityService identityService, IAuthService authService)
    : IAccountService
{
    private readonly HttpClient _httpClient = factory.CreateClient(Settings.Api);
    private readonly IAuthService _authService = authService;

    public async Task<Result<AccountInfoModel>> GetAccountInfoAsync()
    {
        var id = await identityService.GetClaimsPrincipalData();

        if (string.IsNullOrEmpty(id))
        {
            return UserErrors.NotAuthorized();
        }

        var url = $"{Settings.ApiRoot}/api/v1/accounts/info/{id}";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        
        if (response.IsSuccessStatusCode)
        {
            var model = JsonSerializer.Deserialize<AccountInfoModel>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (model is null)
                return UserErrors.NotFound(Guid.Parse(id));
            
            return model;
        }
        
        return content.ToError();
    }

    public async Task<Result<PagedList<ProductModel>?>> GetUserProducts(ProductQueryModel model)
    {
        var url = new Uri($"{Settings.ApiRoot}/api/v1/accounts/my-products").GetUrlWithParams(model);

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<PagedList<ProductModel>>(content,
                       new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        return content.ToError();
    }

    public async Task<Result<IEnumerable<Guid>>> GetAllFavoriteAsync()
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/favorite/all";

        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var model = JsonSerializer.Deserialize<IEnumerable<Guid>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                   ?? Enumerable.Empty<Guid>();
            
            return Result<IEnumerable<Guid>>.Success(model);
        }

        return content.ToError();
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
            var newModel = JsonSerializer.Deserialize<AccountInfoModel>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (newModel is null)
                return model;
            
            return newModel;
        }
        
        return content.ToError();
    }

    public async Task<Result<AccountInfoModel>?> UploadAvatarAsync(MultipartFormDataContent form)
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/avatar/upload";
        
        var response = await _httpClient.PostAsync(url, form);
        
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<AccountInfoModel>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        return content.ToError();
    }

    public async Task<Result<AccountInfoModel>?> DeleteAvatarAsync()
    {
        var url = $"{Settings.ApiRoot}/api/v1/accounts/avatar/delete";

        var response = await _httpClient.DeleteAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        { 
            return JsonSerializer.Deserialize<AccountInfoModel>(content, 
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        return content.ToError();
    }
    
}