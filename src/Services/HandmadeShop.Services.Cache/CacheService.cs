using System.Text.Json;
using HandmadeShop.Infrastructure.Abstractions.Caching;
using HandmadeShop.Services.Settings.Settings;
using NetSchool.Common;
using StackExchange.Redis;

namespace HandmadeShop.Services.Cache;

public class CacheService : ICacheService
{
    private readonly IDatabase _cacheDb;
    private static string _redisUri;
    private readonly TimeSpan _cacheLifeTime;
    private static ConnectionMultiplexer Connection => LazyConnection.Value;
    private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new(() => ConnectionMultiplexer.Connect(_redisUri));

    public CacheService(CacheSettings settings)
    {
        _redisUri = settings.Uri;
        _cacheLifeTime = TimeSpan.FromMinutes(settings.CacheLifeTime);
        _cacheDb = Connection.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key, bool resetLifeTime = false, CancellationToken cancellationToken = default) 
        where T : class
    {
        try
        {
            string? cacheData = await _cacheDb.StringGetAsync(key);
            
            if (string.IsNullOrEmpty(cacheData))
                return null;

            if (resetLifeTime)
                await SetStoreTimeAsync(key);
            
            var value = JsonSerializer.Deserialize<T>(cacheData);
            return value;
        }
        catch (Exception e)
        {
            throw new Exception($"Can`t get data from cache for {key}", e);
        }
    }

    public async Task PutAsync<T>(string key, T data, TimeSpan? storeTime = null, CancellationToken cancellationToken = default)
        where T : class
    {
        await _cacheDb.StringSetAsync(key, data.ToJsonString(), storeTime ?? _cacheLifeTime);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cacheDb.KeyDeleteAsync(key);
    }

    public async Task SetStoreTimeAsync(string key, TimeSpan? storeTime = null)
    {
        await _cacheDb.KeyExpireAsync(key, storeTime ?? _cacheLifeTime);
    }
}