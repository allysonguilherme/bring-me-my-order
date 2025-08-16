using Flunt.Notifications;
using Flunt.Validations;

namespace OrderApplication.DTOs;

public class CreateOrderDto: Notifiable<Notification>, IValidatableDto
{
    public List<OrderProductDto> OrderProducts { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .IsNotNull(OrderProducts, "Order Products", "Order must contain products")
                .IsNotEmpty(OrderProducts, "Order Products", "Order must contain products")
                .IsFalse(OrderProducts.Any(p => string.IsNullOrWhiteSpace(p.Name)), "Product Name", "Product name is required")
                .IsFalse(OrderProducts.Any(p => p.ProductId <= 0), "Product Id", "Product Id is required")
                .IsFalse(OrderProducts.Any(p => p.Price <= 0), "Product Price", "Product price must be greater than 0")
                .IsFalse(OrderProducts.Any(p => p.Quantity <= 0), "Product Quantity", "Product quantity must be greater than 0")
            );    
    }
}