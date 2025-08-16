using Business.Entities;

namespace Application.Services.Interfaces;

public interface IProductFacade
{
    Task<List<Product>> GetAllProducts();
    Task<object?> CreateProduct(Product product);
    Task<Product?> GetProduct(int id);
}