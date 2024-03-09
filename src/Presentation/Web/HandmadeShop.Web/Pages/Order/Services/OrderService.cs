using System.Net.Mime;
using System.Text;
using System.Text.Json;
using HandmadeShop.Domain.Common;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.Web.Extensions;

namespace HandmadeShop.Web.Pages.Order.Services;

public class OrderService : IOrderService
{
    private static readonly string Root = $"{Settings.ApiRoot}/api/v1/orders";
    private readonly HttpClient _httpClient;

    public OrderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<List<OrderModel>>> GetOrdersAsync(OrderQueryModel model)
    {
        var response = await _httpClient.GetAsync($"{Root}?status={model.Status}");
        
        return await ReadFromResponseAsync<List<OrderModel>>(response);
    }

    public async Task<Result<OrderModel>> GetOrderDetailsAsync(Guid orderId)
    {
        var response = await _httpClient.GetAsync($"{Root}/{orderId}");

        return await ReadFromResponseAsync<OrderModel>(response);
    }

    public async Task<Result> CreateOrderAsync(CreateOrderModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PostAsync(Root, data);
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return Result.Success();
        }

        return content.ToError();
    }

    public async Task<Result<OrderModel>> CancelOrderAsync(CancelOrderModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PutAsync($"{Root}/cancel", data);

        return await ReadFromResponseAsync<OrderModel>(response);
    }

    public async Task<Result<OrderModel>> ConfirmOrderAsync(ConfirmOrderModel model)
    {
        var json = JsonSerializer.Serialize(model);
        var data = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);
        var response = await _httpClient.PutAsync($"{Root}/confirm", data);

        return await ReadFromResponseAsync<OrderModel>(response);
    }

    private async Task<Result<T>> ReadFromResponseAsync<T>(HttpResponseMessage message)
        where T : new()
    {
        var content = await message.Content.ReadAsStringAsync();

        if (message.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<T>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new T();
        }

        return content.ToError();
    }
}