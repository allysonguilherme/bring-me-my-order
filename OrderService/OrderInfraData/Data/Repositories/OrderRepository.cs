using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderBusiness.Entities;
using OrderBusiness.Repositories;

namespace OrderInfraData.Repositories;

public class OrderRepository (ApplicationDbContext dbContext, ILogger<OrderRepository> logger)  : IOrderRepository
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
            logger.LogError($"Error in obtaining all orders: {e.Message}");
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
            logger.LogError($"Error in obtaining the order n. {id}: {e.Message}");
            throw;
        }
    }

    public async Task<int> CreateAsync(Order order)
    {
        try
        {
            await dbContext.Orders.AddAsync(order);
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError($"Error in adding new order: {e.Message}");
            throw;
        }
    }

    public async Task<int> DeleteAsync(Order order)
    {
        try
        {
            dbContext.Orders.Remove(order);
            return await dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            logger.LogError($"Error in removing order n.{order.Id}: {e.Message}");
            throw;
        }
    }
}