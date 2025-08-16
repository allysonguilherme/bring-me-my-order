using OrderApplication.DTOs;
using OrderBusiness.Entities;

namespace OrderApplication.Mappings;

public static class DomainDtoMapping
{
    public static Order ToBusinessEntity(this CreateOrderDto dto)
    {
        var orderProducts = new List<OrderProduct>();
        
        dto.OrderProducts.ForEach(p => 
            orderProducts.Add(
                new OrderProduct(p.ProductId, p.Name, p.Price, p.Quantity, p.Description)
                ));
        return new Order(orderProducts);
    }

    public static OrderDto ToDto(this Order order)
    {
        var products = new List<OrderProductDto>();
        order.OrderProducts.ForEach(p =>
        {
            products.Add(new OrderProductDto()
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                Description = p.Description
            });
        });

        return new OrderDto()
        {
            OrderNumber = order.Id,
            TotalPrice = order.TotalPrice,
            Products = products
        };
    }
}