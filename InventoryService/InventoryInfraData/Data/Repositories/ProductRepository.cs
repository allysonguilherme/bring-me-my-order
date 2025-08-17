using System.Text.Json;
using InventoryBusiness.Entities;
using InventoryBusiness.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using NHibernate;
using NHibernate.Linq;

namespace InventoryInfraData.Data.Repositories;

public class ProductRepository (ISession session, IDistributedCache cache) : IProductRepository
{
    private string GetCacheKey(int id)
    {
        return $"Product_{id}";
    }
    private const string ProductListCacheKey = "ProductList";
    
    public async Task<List<Product>> GetAll()
    {
        try
        {
                     
             var cachedProducts = await cache.GetStringAsync(ProductListCacheKey);
             if (cachedProducts != null)
             {
                 return JsonSerializer.Deserialize<List<Product>>(cachedProducts)!;
             }
             
             var products = await session.Query<Product>().ToListAsync();
            
             if (products != null && products.Count != 0) 
             {
                await cache.SetStringAsync(
                    ProductListCacheKey, 
                    JsonSerializer.Serialize(products),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    }
                );
             }
             
             return products;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<object?> Create(Product product)
    {
        try
        {
            using var transaction = session.BeginTransaction();
            var generatedId = await session.SaveAsync(product);
            await transaction.CommitAsync();
            
            await cache.RemoveAsync(ProductListCacheKey);
            return generatedId;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Product?> GetById(int id)
    {
        try
        {
            var cacheKey = GetCacheKey(id); 
            
            var cachedProduct = await cache.GetStringAsync(cacheKey);
            if (cachedProduct != null)
            {
                return JsonSerializer.Deserialize<Product>(cachedProduct);
            }
            
            var product = await session.GetAsync<Product>(id);

            if (product != null)
            {
                await cache.SetStringAsync(
                    cacheKey, 
                    JsonSerializer.Serialize(product),
                    new DistributedCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    }
                );
            }
            
            return product;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Product> Update(Product product)
    {
        try
        {
            var transaction = session.BeginTransaction();
            var updatedProduct = await session.MergeAsync(product);
            
            await transaction.CommitAsync();
            
            var cacheKey = GetCacheKey(product.Id);
            await cache.RemoveAsync(cacheKey);
            await cache.RemoveAsync(ProductListCacheKey);
            
            return updatedProduct;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}