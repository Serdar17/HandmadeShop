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

    public Task<IQueryable<Like>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Like>> GetAllAsync(Expression<Func<Like, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Like?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Like model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Like model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Like model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> HasLikeAsync(Guid userId, int likeId)
    {
        return await _context.UserLikes
            .Where(x => x.LikeId.Equals(likeId) && x.UserId.Equals(userId))
            .AnyAsync();
        // return await _context.Likes
        //     .Include(x => x.Users)
        //     .Where(x => x.Users.Any(u => u.Id.Equals(userId)))
            // .AnyAsync();
            // return true;
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