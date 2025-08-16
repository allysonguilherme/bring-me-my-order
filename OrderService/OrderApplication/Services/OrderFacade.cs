using OrderApplication.DTOs;
using OrderApplication.Mappings;
using OrderApplication.Services.Interfaces;
using OrderBusiness.Repositories;

namespace OrderApplication.Services;

public class OrderFacade (IOrderRepository repository) :  IOrderFacade
{
    public async Task<bool> CreateOrder(CreateOrderDto createOrderDto)
    {
        try
        {
            var newOrder = createOrderDto.ToBusinessEntity();
            return await repository.CreateAsync(newOrder) > 0;
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
}