using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.Web.Common;
using HandmadeShop.Web.Pages.Basket.Services;
using HandmadeShop.Web.TransferServices;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Pages.Basket;

public class BasketBase : ComponentBase
{
    [Inject] protected IBasketService BasketService { get; set; }
    [Inject] protected BasketTransferService BasketTransferService { get; set; }

    protected CartModel Model;
    protected bool IsFirst = true;

    protected string Title => Model.Items.Count > 0 ?
        $"В корзине {Model.Items.Count} {StringHelper.Decline("товар", Model.Items.Count)}" 
        : "Козина пуста";

    protected override async Task OnInitializedAsync()
    {
        var result = await BasketService.GetBasketData();
        
        if (result is { IsSuccess: true, Value: not null })
        {
            Model = result.Value;
        }
    }

    protected async Task Add(CartItemModel cartItem, int value)
    {
        Console.WriteLine("add");
        cartItem.Quantity = value;
        var result =
            await BasketService.UpdateCartItemAsync(new UpdateCartItemModel(cartItem.ProductId, cartItem.Quantity));

        if (result.IsSuccess && result.Value != null)
        {
            cartItem = result.Value;
        }
    }
    
    protected async Task DeleteFromBasket(CartItemModel model)
    {
        var result = await BasketService.DeleteCartItemAsync(model.ProductId);

        if (result.IsSuccess && result.Value != null)
        {
            Model.Items.Remove(model);
            BasketTransferService.Data = result.Value;
        }
    }
}