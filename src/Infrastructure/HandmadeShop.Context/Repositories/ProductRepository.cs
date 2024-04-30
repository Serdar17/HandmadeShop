using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class ProductRepository(IAppDbContext context) : IProductRepository
{
    public async Task<IQueryable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Products.AsQueryable();
    }

    public async Task<IQueryable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        return context.Products.Where(predicate);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Products
            .Where(x => x.Uid.Equals(id))
            .Include(x => x.Catalog)
            .Include(x => x.Like)
            .Include(x => x.Reviews)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(Product model, CancellationToken cancellationToken = default)
    {
        context.Products.Add(model);
    }

    public async Task UpdateAsync(Product model, CancellationToken cancellationToken = default)
    {
        context.Products.Update(model);
    }

    public async Task DeleteAsync(Product model, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Products
            .Where(x => x.Uid.Equals(id))
            .ExecuteDeleteAsync(cancellationToken);
    }
}