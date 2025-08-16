using OrderBusiness.Entities;

namespace OrderUnitTests;

public class OrderTests
{
    [Test]
    public void OrderShouldNotHaveEmptyProducts()
    {
        var exception = Assert.Throws<ArgumentException>((() =>
        {
            var order = new Order(new List<ProductOrder>());
        }));
        
        Assert.That(exception.Message, Is.EqualTo("Cannot create order without any products"));
    }

    [Test]
    public void OrderShouldHaveCorrectTotalPrice()
    {
        var products = new List<ProductOrder>()
        {
            new ProductOrder(5, "Honey Pot", 5, 2),
            new ProductOrder(1, "IPhone", 4000, 1)
        };
        
        var order = new Order(products);
        
        Assert.That(order.TotalPrice, Is.EqualTo(4010));
    }
}