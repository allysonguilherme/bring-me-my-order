using OrderApplication.DTOs;
using OrderBusiness.Entities;

namespace OrderUnitTests;

public class CreateOrderDtoTest
{
    [Test]
    public void InvalidCreateOrderDtoShouldHaveNotifications()
    {
        var orderProducts = new List<OrderProductDto>()
        {
            new OrderProductDto()
            {
                ProductId = 0,
                Name = "",
                Quantity = 0,
                Price = 0
            }
        };

        var createOrderDto = new CreateOrderDto()
        {
            OrderProducts = orderProducts,
        };
        
        createOrderDto.Validate();
        Assert.That(createOrderDto.IsValid, Is.False);
        Assert.That(createOrderDto.Notifications, Has.Count.EqualTo(4));
    }
    
    [Test]
    public void ValidCreateOrderDtoShouldNotHaveNotifications()
    {
        var orderProducts = new List<OrderProductDto>()
        {
            new OrderProductDto()
            {
                ProductId = 5,
                Name = "Honey Pot",
                Quantity = 3,
                Price = 25
            }
        };

        var createOrderDto = new CreateOrderDto()
        {
            OrderProducts = orderProducts,
        };
        
        createOrderDto.Validate();
        Assert.That(createOrderDto.IsValid, Is.True);
        Assert.That(createOrderDto.Notifications, Has.Count.EqualTo(0));
    } 
}