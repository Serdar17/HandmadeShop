using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class LikeRepository : ILikeRepository
{
    private readonly IAppDbContext _context;

    public LikeRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Like>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Likes.AsQueryable();
    }

    public async Task<IQueryable<Like>> GetAllAsync(Expression<Func<Like, bool>> predicate)
    {
        return _context.Likes.Where(predicate);
    }

    public async Task<Like?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Likes.FirstOrDefaultAsync(x => x.Uid.Equals(id), cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(Like model, CancellationToken cancellationToken = default)
    {
        _context.Likes.Add(model);
    }

    public async Task UpdateAsync(Like model, CancellationToken cancellationToken = default)
    {
        _context.Likes.Update(model);
    }

    public async Task DeleteAsync(Like model, CancellationToken cancellationToken = default)
    {
        _context.Likes.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Likes.Where(x => x.Uid.Equals(id)).ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public async Task<bool> HasLikeAsync(Guid userId, int likeId)
    {
        return await _context.UserLikes
            .Where(x => x.LikeId.Equals(likeId) && x.UserId.Equals(userId))
            .AnyAsync();
    }

    public async Task RemoveLikeAsync(Guid userId, int likeId)
    {
        var entity = await _context.UserLikes
            .Where(x => x.LikeId.Equals(likeId) && x.UserId.Equals(userId))
            .FirstOrDefaultAsync();

        if (entity is null)
            return;
        _context.UserLikes.Remove(entity);
    }
}