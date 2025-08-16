namespace OrderBusiness.Entities;

public class OrderProduct
{
    public int Id { get; init; }
    public int ProductId { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
    
    public int OrderId { get; init; } 
    public Order Order { get; init; }
    
    //parameterless constructor used by EF Core
    private OrderProduct() { }

    public OrderProduct(int productId, string name, decimal price, int quantity, string? description = null)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Quantity must be greater than 0");
        }
        
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }

    public decimal GetTotalPrice()
    {
        return Price * Quantity;
    }
}