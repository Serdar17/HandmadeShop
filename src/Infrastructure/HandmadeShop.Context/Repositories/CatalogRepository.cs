using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class CatalogRepository : ICatalogRepository
{
    private readonly IAppDbContext _context;

    public CatalogRepository(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<Catalog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _context.Catalogs;
    }

    public Task<IQueryable<Catalog>> GetAllAsync(Expression<Func<Catalog, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<Catalog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Catalog?> GetByNameAsync(string name)
    {
        return await _context.Catalogs
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<bool> IsUniqueAsync(string name)
    {
        return await _context.Catalogs.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()) is null;
    }
}