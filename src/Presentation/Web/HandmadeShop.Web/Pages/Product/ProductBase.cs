using HandmadeShop.SharedModel.Catalogs.Models;
using HandmadeShop.Web.Components;
using HandmadeShop.Web.Pages.Product.Models;
using HandmadeShop.Web.Pages.Product.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;

namespace HandmadeShop.Web.Pages.Product;

public class ProductBase : ComponentBase
{
    [Inject]
    protected IProductService ProductService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    
    [Inject] protected IDialogService DialogService { get; set; }
    
    [Inject] protected NavigationManager NavigationManager { get; set; }
    
    [Parameter] public Guid? ProductId { get; set; }
    
    protected MudForm Form;

    protected ProductModel? ProductModel;
    
    protected int SelectedIndex = 0;

    protected bool ShowErrors { get; set; }
    protected string ErrorDetail = string.Empty;
    protected string Error = string.Empty;

    protected IList<ProductImageModel> Images = new List<ProductImageModel>();
    protected IList<IBrowserFile> Files = new List<IBrowserFile>();
    
    protected MudCarousel<ProductImageModel> Carousel;

    protected override async Task OnParametersSetAsync()
    {
        if (ProductId is null)
            return;

        var result = await ProductService.GetProductByIdAsync(ProductId.Value);
        
        if (result.IsSuccess && result.Value is not null)
        {
            ProductModel = result.Value;
        }

        var res = await ProductService.GetProductImagesAsync(ProductId.Value);

        if (res.IsSuccess && res.Value != null)
        {
            Images = res.Value;
        }
        
        await base.OnParametersSetAsync();
    }

    protected async Task UploadFiles(InputFileChangeEventArgs args)
    {
        if (ProductId is null)
            return;
        
        var file = args.File;
        var form = new MultipartFormDataContent();
        var content = new StreamContent(file.OpenReadStream());
        form.Add(content, "image", file.Name);
        var result = await ProductService.UploadImageAsync(form, ProductId.Value);

        if (result.IsSuccess && result.Value != null)
        {
            Images.Add(result.Value); 
            StateHasChanged();
            SelectedIndex = Images.Count - 1;
            Snackbar.Add("Файл загружен успешно!", Severity.Success);
        }
    }

    protected async Task CreateProduct()
    {
        var parameters = new DialogParameters { };
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, FullWidth = true };

        var dialog = await DialogService.ShowAsync<ProductDialog>("Создать продукт", parameters, options);

        if (!(await dialog.Result).Canceled)
        {
            var result = await ProductService.CreateProductModel(((ProductDialog)dialog.Dialog).Model);
            ProductModel = result.Value;
            ProductId = ProductModel?.Uid;
            NavigationManager.NavigateTo($"/product/{ProductId}");
        }
    }

    protected async Task EditProduct()
    {
        var parameters = new DialogParameters { };
        parameters.Add("Model", ProductModel);
        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Large, FullWidth = true };

        var dialog = await DialogService.ShowAsync<ProductDialog>("Изменить продукт", parameters, options);

        if (!(await dialog.Result).Canceled)
        {
            var result = await ProductService.UpdateProductModel(((ProductDialog)dialog.Dialog).Model);
            ProductModel = result.Value;
            ProductId = ProductModel?.Uid;
            NavigationManager.NavigateTo($"/product/{ProductId}");
        }
    }

    protected async Task DeleteProduct()
    {
        var parameters = new DialogParameters();
        parameters.Add("ContentText", "Вы действительно хотите удалить товар? Данное действие нельзя будет отменить после удаления." +
                                      "Также при удалении пропадет рейтинг товара и все отзывы!");
        parameters.Add("ButtonText", "Delete");
        parameters.Add("Color", Color.Error);

        var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        var dialog = await DialogService.ShowAsync<Dialog>("Удаление продукта", parameters, options);
        var result = await dialog.Result;

        if (result.Canceled)
        {
            return;
        }

        if (ProductId is not null)
        {
            await ProductService.DeleteProductAsync(ProductId.Value);
            NavigationManager.NavigateTo("/product");
        }
    }
    
    protected async Task DeleteAsync(int index)
    {
        if (ProductId is null)
            return;

        var path = Images[index].ImagePath;
        var result = await ProductService.DeleteProductImageAsync(ProductId.Value, new DeleteProductImageModel(path));


        if (result.IsSuccess && Images.Any())
        {
            Images.RemoveAt(index);
            StateHasChanged();
            SelectedIndex = 0;
            Snackbar.Add("Файл удален успешно!", Severity.Success);
        }
    }
}