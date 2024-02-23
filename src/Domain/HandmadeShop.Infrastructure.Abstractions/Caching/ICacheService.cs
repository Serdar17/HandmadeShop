namespace HandmadeShop.Infrastructure.Abstractions.Caching;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key, bool resetLifeTime = false, CancellationToken cancellationToken = default)
        where T : class;

    Task PutAsync<T>(string key, T data, TimeSpan? storeTime = null, CancellationToken cancellationToken = default)
        where T : class;

    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    Task SetStoreTimeAsync(string key, TimeSpan? storeTime = null);


}