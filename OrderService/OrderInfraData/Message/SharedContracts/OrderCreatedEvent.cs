namespace Shared.Contracts;

public class OrderCreatedEvent
{
    public int OrderNumber { get; set; }
    public List<OrderProductDto> Products { get; set; }
    public DateTime OrderCreated { get; set; } = DateTime.UtcNow;
}

public class OrderProductDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }  
}