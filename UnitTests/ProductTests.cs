using Business.Entities;
using Business.Validations;

namespace UnitTests;

public class ProductTests
{
    [Fact]
    public void ProductNameShouldNotBeNull()
    {
        var emptyNameException = Assert.Throws<BusinessValidationException>(() => new Product("", 10, 12.5m));
        Assert.Equal("Product name cannot be empty", emptyNameException.Message);
    }
    
    [Fact]
    public void ProductStockShouldNotBeNegative()
    {
        var exception = Assert.Throws<BusinessValidationException>(() => new Product("TShirt", -8, 12.5m));
        Assert.Equal("Product stock cannot be negative", exception.Message);
    }
    
    [Fact]
    public void ProductPriceShouldNotBeNegative()
    {
        var exception = Assert.Throws<BusinessValidationException>(() => new Product("TShirt", 8, -12.5m));
        Assert.Equal("Product price cannot be negative", exception.Message);
    }

    [Fact]
    public void ShouldNotAddNegativeStock()
    {
        var product = new Product("TShirt", 8, 12.5m);
        var exception = Assert.Throws<BusinessValidationException>(() => product.AddStock(-1));
        
        Assert.Equal("Quantity cannot be negative", exception.Message);
    }
}