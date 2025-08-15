using Business.Entities;

namespace Application.Services.Interfaces;

public interface IProductFacade
{
    Task<List<Product>> GetAllProducts();
}