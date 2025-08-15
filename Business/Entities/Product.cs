using Business.Validations;

namespace Business.Entities;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int Stock { get; private set; }
    public decimal Price { get; private set; }

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

    public void AddStock(int quantity)
    {
        BusinessValidationException.When(quantity < 0, "Quantity cannot be negative");
        Stock += quantity;
    }

    public void WithdrawStock(int quantity)
    {
        BusinessValidationException.When(quantity < 0, "Quantity cannot be negative");
        BusinessValidationException.When(Stock == 0, "No stock available");
        BusinessValidationException.When(quantity > Stock, "There is not enough stock");
        Stock -= quantity;
    }
}