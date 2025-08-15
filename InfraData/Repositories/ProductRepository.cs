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
}