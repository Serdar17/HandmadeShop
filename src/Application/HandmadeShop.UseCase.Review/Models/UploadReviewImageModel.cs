using Microsoft.AspNetCore.Http;

namespace HandmadeShop.UseCase.Review.Models;

public class UploadReviewImageModel
{
    public Guid ReviewId { get; set; }
    public required IFormFile Image { get; set; }
}