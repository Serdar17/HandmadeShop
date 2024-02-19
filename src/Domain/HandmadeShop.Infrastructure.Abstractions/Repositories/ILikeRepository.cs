using HandmadeShop.Domain;

namespace HandmadeShop.Infrastructure.Abstractions.Repositories;

public interface ILikeRepository : IBaseRepository<Like, Guid>
{
    Task<bool> HasLikeAsync(Guid userId);
}