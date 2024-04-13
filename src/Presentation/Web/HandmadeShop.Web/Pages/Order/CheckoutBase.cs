using FluentValidation;
using HandmadeShop.SharedModel.Basket.Models;
using HandmadeShop.SharedModel.Orders.Models;
using HandmadeShop.Web.Pages.Basket.Services;
using HandmadeShop.Web.Pages.Order.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Severity = MudBlazor.Severity;

namespace HandmadeShop.Web.Pages.Order;

public class CheckoutBase : ComponentBase
{
    [Inject] private IBasketService BasketService { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IOrderService OrderService { get; set; }
    protected CartModel? CartModel { get; set; }
    protected CreateOrderModel Model = new();
    protected CreateOrderModelValidator Validator = new();
    protected MudForm Form;
    protected bool Processing { get; set; }
    

    protected override async Task OnInitializedAsync()
    {
        var result = await BasketService.GetBasketData();
        if (result.IsSuccess && result.Value != null)
        {
            CartModel = result.Value;
        }
    }
    
    protected async Task Submit()
    {
        Processing = true;
        await Form.Validate();

        if (!Form.IsValid)
        {
            return;
        }

        var result = await OrderService.CreateOrderAsync(Model);
        if (result.IsSuccess)
        {
            Snackbar.Add("Заказ отправлен на обработку!");
        }
        else
        {
            Snackbar.Add(result.Error.Message, Severity.Error);
        }
        // TODO: navigate to the my-orders page
        await Task.Delay(500);

        Processing = false;
    }
}

public class CreateOrderModelValidator : AbstractValidator<CreateOrderModel>
{
    public CreateOrderModelValidator()
    {
        RuleFor(x => x.Buyer.Name)
            .NotEmpty();
        RuleFor(x => x.Buyer.PhoneNumber)
            .NotEmpty();
        RuleFor(x => x.Order.Address.ExactAddress)
            .NotEmpty();
        RuleFor(x => x.Order.Address.City)
            .NotEmpty();
        RuleFor(x => x.Order.Address.Country)
            .NotEmpty();
        RuleFor(x => x.Order.Description)
            .NotEmpty();
    }
    
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CreateOrderModel>.CreateWithOptions((CreateOrderModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return Array.Empty<string>();
        return result.Errors.Select(e => e.ErrorMessage);
    };
}