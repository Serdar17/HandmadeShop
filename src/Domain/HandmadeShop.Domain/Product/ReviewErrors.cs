using HandmadeShop.Domain.Common;

namespace HandmadeShop.Domain;

public static class ReviewErrors
{
    public static Error NotFound(Guid reviewId) =>
        Error.NotFound("Reviews.NotFound", $"Review with id={reviewId} was not found");

    public static Error Prohibition() =>
        Error.Forbidden("Reviews.RemoveReviews", "Access to deleting a review is prohibited");
}