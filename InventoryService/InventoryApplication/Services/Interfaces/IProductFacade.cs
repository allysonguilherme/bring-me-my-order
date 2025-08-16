using InventoryBusiness.Entities;

namespace InventoryApplication.Services.Interfaces;

public interface IProductFacade
{
    Task<List<Product>> GetAllProducts();
    Task<object?> CreateProduct(Product product);
    Task<Product?> GetProduct(int id);
    Task<(bool, string)> AddProductStock(Product product, int quantity);
    Task<(bool, string)> WithdrawProductStock(Product product, int quantity);
}