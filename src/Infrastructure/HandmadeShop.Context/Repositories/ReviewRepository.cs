using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class ReviewRepository(IAppDbContext context) : IReviewRepository
{
    public async Task<IQueryable<Review>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Reviews.AsQueryable();
    }

    public async Task<IQueryable<Review>> GetAllAsync(Expression<Func<Review, bool>> predicate)
    {
        return context.Reviews.Where(predicate);
    }

    public async Task<Review?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Reviews
            .Include(x => x.Owner)
            .FirstOrDefaultAsync(x => x.Uid == id, cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(Review model, CancellationToken cancellationToken = default)
    {
        context.Reviews.Add(model);
    }

    public async Task UpdateAsync(Review model, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(context.Reviews.Update(model));
    }

    public async Task DeleteAsync(Review model, CancellationToken cancellationToken = default)
    {
        context.Reviews.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Reviews
            .Where(x => x.Uid == id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}