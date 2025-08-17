using OrderBusiness.Entities;

namespace OrderTests;

public class OrderTests
{
    [Test]
    public void OrderShouldNotHaveEmptyProducts()
    {
        var exception = Assert.Throws<ArgumentException>((() =>
        {
            var order = new Order(new List<OrderProduct>());
        }));
        
        Assert.That(exception.Message, Is.EqualTo("Cannot create order without any products"));
    }

    [Test]
    public void OrderShouldHaveCorrectTotalPrice()
    {
        var products = new List<OrderProduct>()
        {
            new OrderProduct(5, "Honey Pot", 5, 2),
            new OrderProduct(1, "IPhone", 4000, 1)
        };
        
        var order = new Order(products);
        
        Assert.That(order.TotalPrice, Is.EqualTo(4010));
    }
}