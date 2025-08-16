using InventoryApplication.DTOs;

namespace InventoryUnitTests;

public class CreateProductDtoTests
{
    [Fact]
    public void CreateProductDtoShouldAddNotifications()
    {
        var createProductDto = new CreateProductDto()
        {
            Name = "",
            Stock = 0,
            Price = 0,
        };
        createProductDto.Validate();
        
        Assert.False(createProductDto.IsValid);
        Assert.True(createProductDto.Notifications.Count == 3);
    }
}