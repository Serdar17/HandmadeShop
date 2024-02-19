using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Commands.AddFavorite;

public sealed record AddFavoriteCommand(AddFavoriteModel Model) : ICommand;