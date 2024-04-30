using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class LikeRepository(IAppDbContext context) : ILikeRepository
{
    public async Task<IQueryable<Like>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Likes.AsQueryable();
    }

    public async Task<IQueryable<Like>> GetAllAsync(Expression<Func<Like, bool>> predicate)
    {
        return context.Likes.Where(predicate);
    }

    public async Task<Like?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Likes.FirstOrDefaultAsync(x => x.Uid.Equals(id), cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(Like model, CancellationToken cancellationToken = default)
    {
        context.Likes.Add(model);
    }

    public async Task UpdateAsync(Like model, CancellationToken cancellationToken = default)
    {
        context.Likes.Update(model);
    }

    public async Task DeleteAsync(Like model, CancellationToken cancellationToken = default)
    {
        context.Likes.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Likes.Where(x => x.Uid.Equals(id)).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> HasLikeAsync(Guid userId, int likeId)
    {
        return await context.UserLikes
            .Where(x => x.LikeId.Equals(likeId) && x.UserId.Equals(userId))
            .AnyAsync();
    }

    public async Task RemoveLikeAsync(Guid userId, int likeId)
    {
        var entity = await context.UserLikes
            .Where(x => x.LikeId.Equals(likeId) && x.UserId.Equals(userId))
            .FirstOrDefaultAsync();

        if (entity is null)
            return;
        context.UserLikes.Remove(entity);
    }
}