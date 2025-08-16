namespace OrderApplication.DTOs;

public class OrderDto
{
    public int OrderNumber { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderProductDto> Products { get; set; }
}