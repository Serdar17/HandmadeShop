using System.Linq.Expressions;
using HandmadeShop.Domain.Common;

namespace HandmadeShop.Infrastructure.Abstractions.Repositories;

public interface IBaseRepository<TModel, in TKey> 
    where TModel : BaseEntity
{
    Task<IQueryable<TModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IQueryable<TModel>> GetAllAsync(Expression<Func<TModel, bool>> predicate);
    Task<TModel?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);
    Task InsertAsync(TModel model, CancellationToken cancellationToken = default);
    Task UpdateAsync(TModel model, CancellationToken cancellationToken = default);
    Task DeleteAsync(TModel model, CancellationToken cancellationToken = default);
    Task DeleteByIdAsync(TKey id, CancellationToken cancellationToken = default);
}