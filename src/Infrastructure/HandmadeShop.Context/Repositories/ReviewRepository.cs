using System.Linq.Expressions;
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

    public async Task<IQueryable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Reviews.AsQueryable();
    }

    public async Task<IQueryable<Review>> GetAllAsync(Expression<Func<Review, bool>> predicate)
    {
        return _context.Reviews.Where(predicate);
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Reviews
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Uid == id, cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(Review model, CancellationToken cancellationToken = default)
    {
        _context.Reviews.Add(model);
    }

    public async Task UpdateAsync(Review model, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(_context.Reviews.Update(model));
    }

    public async Task DeleteAsync(Review model, CancellationToken cancellationToken = default)
    {
        _context.Reviews.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Reviews
            .Where(x => x.Uid == id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}