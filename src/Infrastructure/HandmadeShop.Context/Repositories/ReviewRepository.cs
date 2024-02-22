using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly IAppDbContext _context;

    public ReviewRepository(IAppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Uid == id, cancellationToken: cancellationToken);
    }

    public Task InsertAsync(Review model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Review model, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(_context.Reviews.Update(model));
    }

    public Task DeleteAsync(Review model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Reviews
            .Where(x => x.Uid == id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}