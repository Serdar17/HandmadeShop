using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class CatalogRepository(IAppDbContext context) : ICatalogRepository
{
    public async Task<IQueryable<Catalog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return context.Catalogs.AsQueryable();
    }

    public async Task<IQueryable<Catalog>> GetAllAsync(Expression<Func<Catalog, bool>> predicate)
    {
        return context.Catalogs.Where(predicate);
    }

    public async Task<Catalog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Catalogs
            .FirstOrDefaultAsync(x => x.Uid.Equals(id),cancellationToken: cancellationToken);
    }
    
    public async Task InsertAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        context.Catalogs.Add(model);
    }

    public async Task UpdateAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        context.Catalogs.Update(model);
    }

    public async Task DeleteAsync(Catalog model, CancellationToken cancellationToken = default)
    {
        context.Catalogs.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Catalogs
            .Where(x => x.Uid.Equals(id))
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }

    public async Task<Catalog?> GetByNameAsync(string name)
    {
        return await context.Catalogs
            .FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<bool> IsUniqueAsync(string name)
    {
        return await context.Catalogs.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower()) is null;
    }
}