using Application.Services.Interfaces;
using Business.Entities;
using Business.Repositories;

namespace Application.Services;

public class ProductFacade (IProductRepository repository) : IProductFacade
{
    public async Task<List<Product>> GetAllProducts()
    {
        return await repository.GetAll();
    }

    public async Task<object?> CreateProduct(Product product)
    {
        return await repository.Create(product);
    }

    public async Task<Product?> GetProduct(int id)
    {
        return await repository.GetById(id);
    }

    public async Task<(bool, string)> AddProductStock(Product product, int quantity)
    {
        try
        {
            product.AddStock(quantity);
            await repository.Update(product);
            
            return (true, "Product stock added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<(bool, string)> WithdrawProductStock(Product product, int quantity)
    {
        try
        {
            if (quantity > product.Stock)
            {
                return (false, "There are not enough product stock");
            }
            
            product.WithdrawStock(quantity);
            await repository.Update(product);
            
            return (true, "Product stock withdrawn successfully");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}