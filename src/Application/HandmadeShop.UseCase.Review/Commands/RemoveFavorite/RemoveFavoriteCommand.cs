using HandmadeShop.Application.Abstraction.Messaging;
using HandmadeShop.SharedModel.Reviews.Models;

namespace HandmadeShop.UseCase.Review.Commands.RemoveFavorite;

public sealed record RemoveFavoriteCommand(RemoveFavoriteModel Model) : ICommand;