using InventoryBusiness.Validations;

namespace InventoryBusiness.Entities;

public class Product
{
    public virtual int Id { get; protected set; }
    public virtual string Name { get; protected set; }
    public virtual string? Description { get; protected set; }
    
    public virtual int Stock { get; protected set; }
    public virtual decimal Price { get;  protected set; }
    
    protected Product() { }

    public Product(string name, int stock, decimal price, string? description = null)
    {
       BusinessValidationException.When(string.IsNullOrEmpty(name), "Product name cannot be empty");
       BusinessValidationException.When(name.Length > 256, "Product name cannot be longer than 256 characters");
       BusinessValidationException.When(stock < 0, "Product stock cannot be negative");
       BusinessValidationException.When(price < 0, "Product price cannot be negative");
       
       Name = name;
       Description = description;
       Stock = stock;
       Price = price;
    }

    public virtual void AddStock(int quantity)
    {
        BusinessValidationException.When(quantity < 0, "Quantity cannot be negative");
        Stock += quantity;
    }

    public virtual void WithdrawStock(int quantity)
    {
        BusinessValidationException.When(quantity < 0, "Quantity cannot be negative");
        BusinessValidationException.When(Stock == 0, "No stock available");
        BusinessValidationException.When(quantity > Stock, "There is not enough stock");
        Stock -= quantity;
    }
}