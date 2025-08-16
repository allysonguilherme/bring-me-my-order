using OrderBusiness.Entities;

namespace OrderBusiness.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int id);
    Task<int> CreateAsync(Order order);
    Task<int> DeleteAsync(Order order);
}