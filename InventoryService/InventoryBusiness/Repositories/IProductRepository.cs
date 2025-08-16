using InventoryBusiness.Entities;

namespace InventoryBusiness.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
    Task<object?> Create(Product product);
    
    Task<Product?> GetById(int id);
    Task<Product> Update(Product product);
}