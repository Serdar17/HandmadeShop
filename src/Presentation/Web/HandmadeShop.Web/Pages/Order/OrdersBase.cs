using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.Web.Pages.Order.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Order;

public class OrdersBase : ComponentBase
{
    [Inject] protected IOrderService OrderService { get; set; }
    
    protected List<OrderModel> Orders { get; set; }
    protected MudTable<OrderModel> MudTable { get; set; }

    protected OrderStatus Status = OrderStatus.Pending;

    protected bool IsCurrentStatus =>
        Status is OrderStatus.Shipped or OrderStatus.Pending or OrderStatus.StockConfirmed or OrderStatus.Sent;

    protected bool IsPast => Status == OrderStatus.Accepted;
    protected bool IsCancelled => Status == OrderStatus.Cancelled;

    protected string Title => IsCurrentStatus ? "Текущие заказы"
        : IsPast ? "Прошлые заказы"
        : "Отмененные заказы";
    protected bool Processing { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await ReloadDataAsync();
    }

    protected async Task CurrentClick()
    {
        Status = OrderStatus.Pending;
        await ReloadDataAsync();
    }

    protected async Task PastClick()
    {
        Status = OrderStatus.Accepted;
        await ReloadDataAsync();
    }

    protected async Task CancelledClick()
    {
        Status = OrderStatus.Cancelled;
        await ReloadDataAsync();
    } 

    private async Task ReloadDataAsync()
    {
        Processing = true;
        await Task.Delay(5000);
        var result = await OrderService.GetOrdersAsync(new OrderQueryModel { Status = Status });

        if (result.IsSuccess && result.Value != null)
        {
            Orders = result.Value.ToList();
        }

        Processing = false;
    }
}