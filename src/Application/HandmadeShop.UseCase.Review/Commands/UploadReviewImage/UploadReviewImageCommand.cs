using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Reviews.Models;
using HandmadeShop.UseCase.Review.Models;

namespace HandmadeShop.UseCase.Review.Commands.UploadReviewImage;

public sealed record UploadReviewImageCommand(UploadReviewImageModel Model) : ICommand<UploadedReviewImage>;