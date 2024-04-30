using System.Linq.Expressions;
using HandmadeShop.Domain;
using HandmadeShop.Infrastructure.Abstractions.Context;
using HandmadeShop.Infrastructure.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HandmadeShop.Context.Repositories;

public class OrderRepository(IAppDbContext context) : IOrderRepository
{
    public async Task<IQueryable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.FromResult(context.Orders.AsQueryable());
    }

    public async Task<IQueryable<Order>> GetAllAsync(Expression<Func<Order, bool>> predicate)
    {
        return context.Orders
            .Include(x => x.Buyer)
            .Include(x => x.Items)
            .Where(predicate);
    }

    public async Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Orders
            .Include(x => x.Buyer)
            .Include(x => x.Items)
            .FirstOrDefaultAsync(x => x.Uid == id, cancellationToken: cancellationToken);
    }

    public async Task InsertAsync(Order model, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(context.Orders.Add(model));
    }

    public async Task UpdateAsync(Order model, CancellationToken cancellationToken = default)
    {
        await Task.FromResult(context.Orders.Update(model));
    }

    public async Task DeleteAsync(Order model, CancellationToken cancellationToken = default)
    {
        context.Orders.Remove(model);
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await context.Orders
            .Where(x => x.Uid == id)
            .ExecuteDeleteAsync(cancellationToken);
    }
}