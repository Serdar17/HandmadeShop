using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Pages.Product.Models;
using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Product;

public class ProductDialogBase : ComponentBase
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }

    [Inject] protected IProductService ProductService { get; set; }

    [Parameter] public ProductModel Model { get; set; } = new()
    {
        Name = string.Empty,
        Description = string.Empty
    };
    
    [Parameter] public Guid? ProductId { get; set; }
    
    protected ProductModelValidator ModelValidator = new();

    protected MudForm Form = default!;

    private IEnumerable<string> _categoryNames = new List<string>();
    
    
    protected async Task Submit()
    {
        await Form.Validate();
        if (Form.IsValid)
        {
            MudDialog.Close(DialogResult.Ok(true));
        }
    }

    protected void Cancel() => MudDialog.Cancel();
    
    protected override async Task OnInitializedAsync()
    {
        var result = await ProductService.GetAllCatalogsAsync();

        if (result.IsSuccess && result.Value is not null)
        {
            _categoryNames = result.Value.Select(x => x.Name);
        }
    }

    protected async Task<IEnumerable<string>> Search(string value)
    {
        if (string.IsNullOrEmpty(value))
            return _categoryNames;
        return _categoryNames.Where(x => x.Contains(value, StringComparison.InvariantCultureIgnoreCase));
    }
}