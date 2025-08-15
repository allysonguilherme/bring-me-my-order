using Business.Entities;

namespace Business.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAll();
}