using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.Web.Extensions;
using HandmadeShop.Web.Pages.Review.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using ZXing.QrCode.Internal;

namespace HandmadeShop.Web.Components;

public class ReviewsBase : ComponentBase
{
    [Inject] private IReviewService ReviewService { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IDialogService DialogService { get; set; }
    [Parameter] public Guid ProductId { get; set; }

    protected ReviewModel Model { get; set; } = new();
    protected List<string> Images { get; set; } = new();

    protected List<ReviewInfoModel> Reviews { get; set; } = new();

    protected List<string> SelectedImages { get; set; } = new();

    protected bool IsSuccess() => !string.IsNullOrEmpty(Model.Comment) && Model.Rating > 0;
    protected bool Processing { get; set; }
    protected bool Disabled => Images.Count >= 5;

    protected bool IsCarouselOpen { get; set; } = true;

    protected override async Task OnParametersSetAsync()
    {
        Processing = true;
        var result = await ReviewService.GetProductReviews(ProductId);
        
        if (result.IsSuccess && result.Value != null)
        {
            Reviews = result.Value.ToList();
        }
        
        Processing = false;
    }

    protected async Task OnValidSubmit()
    {
        Processing = true;
        Model.ProductId = ProductId;
        var result = await ReviewService.AddReviewAsync(Model, Images);

        if (result.IsSuccess && result.Value != null)
        {
            Reviews.Add(result.Value);
            Snackbar.Add("Отзыв успешно добавлен!", Severity.Success);
        }

        Model = new ReviewModel();
        Images = new List<string>();
        Processing = false;
    }

    protected async Task UploadFiles(IBrowserFile file)
    {
        var content = await file.GetDataUrl();
        Images.Add(content);
    }

    protected void RemoveFile(string content)
    {
        Images.Remove(content);
    }

    protected async Task ShowDialog(List<string> images)
    {
        var parameters = new DialogParameters { };
        parameters.Add("Images", images);
        
        var options = new DialogOptions
        {
            CloseButton = true, 
            MaxWidth = MaxWidth.ExtraLarge, 
            Position = DialogPosition.Center,
            FullWidth = true,
        };

        await DialogService.ShowAsync<CarouselDialog>("", parameters, options);
    }
}