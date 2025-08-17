using OrderBusiness.Entities;
using Shared.Contracts;

namespace OrderInfraData.Message.Mappings;

public static class DomainEventMapping
{
    public static OrderCreatedEvent ToCreatedEvent(this Order order)
    {
        var productsDto = order.OrderProducts.Select(x => new OrderProductDto()
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity
        }).ToList();
        
        return  new OrderCreatedEvent(){OrderNumber = order.Id, Products = productsDto};
    }

    public static OrderCancelledEvent ToCancelledEvent(this Order order)
    {
        var productsDto = order.OrderProducts.Select(x => new OrderProductDto()
        {
            ProductId = x.ProductId,
            Quantity = x.Quantity
        }).ToList();
        
        return  new OrderCancelledEvent(){OrderNumber = order.Id, Products = productsDto}; 
    }
}