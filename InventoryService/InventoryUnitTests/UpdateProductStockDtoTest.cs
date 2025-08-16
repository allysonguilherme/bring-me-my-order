using InventoryApplication.DTOs;

namespace InventoryUnitTests;

public class UpdateProductStockDtoTest
{
    [Fact]
    public void CreateProductDtoShouldAddNotifications()
    {
        var dto = new UpdateProductStockDto()
        {
            Quantity = 0,
        };
        dto.Validate();
        
        Assert.False(dto.IsValid);
        Assert.True(dto.Notifications.Count == 1);
    }
}