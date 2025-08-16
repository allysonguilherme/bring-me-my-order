namespace OrderBusiness.Entities;

public class ProductOrder
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public int OrderId { get; set; } 
    public Order Order { get; set; }
    
    //parameterless constructor used by EF Core
    private ProductOrder() { }

    public ProductOrder(int productId, string name, decimal price, int quantity, string? description = null)
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