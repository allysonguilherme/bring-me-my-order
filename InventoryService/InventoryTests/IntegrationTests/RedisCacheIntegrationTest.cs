using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryUnitTests.IntegrationTests;

public class RedisCacheIntegrationTest: IAsyncLifetime
{
    private IDistributedCache _cache;
    private ServiceProvider _serviceProvider;
    
    public async Task InitializeAsync()
    {
        var services = new ServiceCollection();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "localhost:6379";
            options.InstanceName = "InventoryService";
        });
        
        _serviceProvider = services.BuildServiceProvider();
        _cache = _serviceProvider.GetService<IDistributedCache>();
        
        await _cache.RemoveAsync("cache:test1");
        await _cache.RemoveAsync("cache:test2"); 
    }

    public Task DisposeAsync()
    {
        _serviceProvider?.Dispose();
        return Task.CompletedTask;
    }

    [Fact]
    public async Task ShouldCacheAndReturnRedisValue()
    {
        var key = "cache:test1";
        var value = "test1";
        
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions(){AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)});
        
        var cached = await _cache.GetStringAsync(key);
        
        Assert.Equal(value, cached);
    }

    [Fact]
    public async Task CacheValueShouldExpire()
    {
        var key = "cache:test2";
        var value = "test2";
        
        await _cache.SetStringAsync(key, value, new DistributedCacheEntryOptions(){AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1)});
        
        var dataNow =  await _cache.GetStringAsync(key);
        Assert.Equal(value, dataNow);
        
        await Task.Delay(1500);
        
        var expiredData = await _cache.GetStringAsync(key);
        Assert.Null(expiredData);
    }
}