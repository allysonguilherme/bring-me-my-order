using Microsoft.EntityFrameworkCore;
using OrderBusiness.Entities;
using OrderBusiness.Repositories;

namespace OrderInfraData.Repositories;

public class OrderRepository (ApplicationDbContext dbContext)  : IOrderRepository
{
    public async Task<List<Order>> GetAllAsync()
    {
        try
        {
            return await dbContext.Orders
                .Include(o => o.OrderProducts)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        try
        {
            return await dbContext.Orders
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<int> CreateAsync(Order order)
    {
        await dbContext.Orders.AddAsync(order);
        return await dbContext.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Order order)
    {
        throw new NotImplementedException();
    }
}