using OrderBusiness.Entities;

namespace OrderUnitTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void ProductOrderQuantityShouldBeGreaterThanZero()
    {
        var productWithZeroQuantity = Assert.Throws<ArgumentException>(() =>
        {
            var productOrder = new ProductOrder(5, "Honey Pot", 52, 0);
        });
        
        Assert.That(productWithZeroQuantity.Message, Does.StartWith("Quantity must be greater than 0"));
    }
}