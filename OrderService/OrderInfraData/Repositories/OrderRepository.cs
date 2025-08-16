using Microsoft.EntityFrameworkCore;
using OrderBusiness.Entities;
using OrderBusiness.Repositories;

namespace OrderInfraData.Repositories;

public class OrderRepository (ApplicationDbContext dbContext)  : IOrderRepository
{
    public async Task<Order> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }
    public async Task<int> DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }
}