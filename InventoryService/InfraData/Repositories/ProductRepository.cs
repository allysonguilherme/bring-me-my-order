using Business.Entities;
using Business.Repositories;
using NHibernate;
using NHibernate.Linq;

namespace InfraData.Repositories;

public class ProductRepository (ISession session) : IProductRepository
{
    public async Task<List<Product>> GetAll()
    {
        try
        {
            var products = await session.Query<Product>().ToListAsync();
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
            return await session.GetAsync<Product>(id);
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
            
            return updatedProduct;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}