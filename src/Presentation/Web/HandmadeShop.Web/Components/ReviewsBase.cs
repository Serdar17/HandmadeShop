using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.Web.Pages.Review.Services;
using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class ReviewsBase : ComponentBase
{
    [Inject] private IReviewService ReviewService { get; set; }
    
    [Parameter] public Guid ProductId { get; set; }

    protected ReviewModel Model { get; set; } = new();
    
    protected List<ReviewInfoModel> Reviews { get; set; }

    public bool IsSuccess() => !string.IsNullOrEmpty(Model.Comment) && Model.Rating > 0;

    protected override async Task OnParametersSetAsync()
    {
        // var result = await ReviewService.GetProductReviews(ProductId);
        //
        // if (result.IsSuccess && result.Value != null)
        // {
        //     Reviews = result.Value.ToList();
        // }
    }

    protected async Task OnValidSubmit()
    {
        Console.WriteLine("submited");
    }
}