using Business.Entities;
using Business.Validations;

namespace UnitTests;

public class ProductTests
{
    [Fact]
    public void ProductNameShouldNotBeNull()
    {
        var emptyNameException = Assert.Throws<BusinessValidationException>(() => new Product("", null, 10, 12.5m));
        Assert.Equal("Product name cannot be empty", emptyNameException.Message);
    }
    
    [Fact]
    public void ProductStockShouldNotBeNegative()
    {
        var exception = Assert.Throws<BusinessValidationException>(() => new Product("TShirt", null, -8, 12.5m));
        Assert.Equal("Stock cannot be negative", exception.Message);
    }
    
    [Fact]
    public void ProductPriceShouldNotBeNegative()
    {
        var exception = Assert.Throws<BusinessValidationException>(() => new Product("TShirt", null, 8, 12.5m));
        Assert.Equal("Stock cannot be negative", exception.Message);
    }
}