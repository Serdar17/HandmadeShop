using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IAppDbContext _context;

    public ProductRepository(IAppDbContext context)
    {
        _context = context;
    }

    public Task<IQueryable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Product>> GetAllAsync(Expression<Func<Product, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(x => x.Uid.Equals(id))
            .Include(x => x.Catalog)
            .Include(x => x.Like)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task InsertAsync(Product model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Product model, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(model);
    }

    public async Task DeleteAsync(Product model, CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _context.Products
            .Where(x => x.Uid.Equals(id))
            .ExecuteDeleteAsync(cancellationToken);
    }
}