using HandmadeShop.Domain;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.Web.Pages.Order.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Order;

public class OrderDetailsBase : ComponentBase
{
    [Parameter] public Guid OrderId { get; set; }
    [Inject] protected IOrderService OrderService { get; set; }
    protected OrderModel? Model { get; set; }

    protected bool CancelDisabled => Model.OrderStatus is not OrderStatus.Pending and not OrderStatus.StockConfirmed and not OrderStatus.Sent;
    protected bool ConfirmDisabled => Model.OrderStatus != OrderStatus.Shipped;

    protected List<string> icons = new()
    {
        Icons.Material.Filled.HourglassBottom,
        Icons.Material.Filled.Warehouse,
        Icons.Material.Filled.AirplanemodeActive,
        Icons.Material.Filled.DeliveryDining,
        Icons.Material.Filled.Cancel,
        Icons.Material.Filled.Check
    };

    protected List<Color> colors = new()
    {
        Color.Warning,
        Color.Info,
        Color.Primary,
        Color.Success,
        Color.Error,
        Color.Tertiary

    };

    protected override async Task OnParametersSetAsync()
    {
        var result = await OrderService.GetOrderDetailsAsync(OrderId);
        
        if (result.IsSuccess && result.Value != null)
        {
            Model = result.Value;
        }
    }

    protected async Task ConfirmOrder()
    {
        var result = await OrderService.ConfirmOrderAsync(new ConfirmOrderModel { OrderId = OrderId });
        
        if (result.IsSuccess && result.Value != null)
        {
            Model = result.Value;
        }
    }

    protected async Task CancelOrder()
    {
        var result = await OrderService.CancelOrderAsync(new CancelOrderModel { OrderId = OrderId });
        
        if (result.IsSuccess && result.Value != null)
        {
            Model = result.Value;
        }
    }
}