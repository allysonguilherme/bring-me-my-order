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
}