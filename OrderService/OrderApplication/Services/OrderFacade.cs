using OrderApplication.DTOs;
using OrderApplication.Mappings;
using OrderApplication.Services.Interfaces;
using OrderBusiness.Publishers;
using OrderBusiness.Repositories;

namespace OrderApplication.Services;

public class OrderFacade (IOrderRepository repository, IOrderPublisher publisher) :  IOrderFacade
{
    public async Task<bool> CreateOrder(CreateOrderDto createOrderDto)
    {
        try
        {
            var newOrder = createOrderDto.ToBusinessEntity();
            var orderCreated = await repository.CreateAsync(newOrder) > 0;

            if (orderCreated)
            {
                publisher.OrderCreatedPublish(newOrder);
            }
            
            return orderCreated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<OrderDto>> GetOrders()
    {
        var order = await repository.GetAllAsync();
        return order.Select(o => o.ToDto()).ToList();
    }

    public async Task<OrderDto?> GetOrder(int id)
    {
        var order = await repository.GetByIdAsync(id);
        return order?.ToDto();
    }

    public async Task<bool?> CancelOrder(int id)
    {
        try
        {
            var order = await repository.GetByIdAsync(id);
            if(order == null) return null;
            
            var deleted = await repository.DeleteAsync(order) > 0;
            if (deleted)
            {
                publisher.OrderCancelledPublish(order);
            }
            
            return deleted;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}