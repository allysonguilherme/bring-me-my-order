using Business.Entities;

namespace Business.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<object?> Create(Product product);
    
    Task<Product?> GetById(int id);
    Task<Product> Update(Product product);
}