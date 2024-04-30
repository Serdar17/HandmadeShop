using System.Net.Mime;
using System.Text;
using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.Web.Extensions;

namespace HandmadeShop.Web.Pages.Basket.Services;

public class BasketService(IHttpClientFactory factory) : IBasketService
{
    private static readonly string Root = $"{Settings.ApiRoot}/api/v1/basket";
    
    private readonly HttpClient _httpClient = factory.CreateClient(Settings.Api);

    public async Task<Result<BasketModel>> GetUserBasketAsync()
    {
        var response = await _httpClient.GetAsync(Root);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<BasketModel>(content, new JsonSerializerOptions
                       { PropertyNameCaseInsensitive = true })
                   ?? new BasketModel();
        }

        return content.ToError();
    }

    public async Task<Result<CartModel>> GetBasketData()
    {
        var response = await _httpClient.GetAsync($"{Root}/all");
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<CartModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CartModel();
        }

        return content.ToError();
    }

    public async Task<Result<BasketModel>> AddCartAsync(AddCartModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

        var response = await _httpClient.PostAsync(Root, data);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<BasketModel>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new BasketModel();
        }

        return content.ToError();
    }

    public async Task<Result<CartItemModel>> UpdateCartItemAsync(UpdateCartItemModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        
        var response = await _httpClient.PutAsync(Root, data);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<CartItemModel>(content, new JsonSerializerOptions()
                { PropertyNameCaseInsensitive = true }) ?? new CartItemModel();
        }

        return content.ToError();
    }

    public async Task<Result<BasketModel>> DeleteCartItemAsync(Guid productId)
    {
        var response = await _httpClient.DeleteAsync($"{Root}/{productId}");
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<BasketModel>(content, new JsonSerializerOptions
                { PropertyNameCaseInsensitive = true }) ?? new BasketModel();
        }

        return content.ToError();
    }
}