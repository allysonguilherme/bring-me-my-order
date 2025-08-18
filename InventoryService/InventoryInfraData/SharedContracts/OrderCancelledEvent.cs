namespace Shared.Contracts;

public class OrderCancelledEvent
{
    public int OrderNumber { get; set; }
    public List<OrderProductDto> Products { get; set; }
    public DateTime OrderCreated { get; set; } = DateTime.UtcNow; 
}