using Microsoft.AspNetCore.Components;

namespace HandmadeShop.Web.Components;

public class ReviewsBase : ComponentBase
{
    [Parameter] public Guid ProductId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        // TODO: fetch all reviews
    }
}