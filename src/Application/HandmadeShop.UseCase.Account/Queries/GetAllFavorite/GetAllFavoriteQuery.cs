using HandmadeShop.Application.Abstraction.Messaging;

namespace HandmadeShop.UseCase.Account.Queries.GetAllFavorite;

public sealed class GetAllFavoriteQuery : IQuery<IEnumerable<Guid>>;