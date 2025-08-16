using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using InventoryApplication.DTOs.interfaces;

namespace InventoryApplication.DTOs;

public class CreateProductDto:  Notifiable<Notification>, IValidatableDto
{
    [Required] public string Name { get; set; }
    [Required] public int Stock { get; set; }
    public string? Description { get; set; }
    [Required] public decimal Price { get; set; }
    
    public void Validate()
    {
        AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, nameof(Name), $"Product name is required")
                .IsLowerOrEqualsThan(Name.Length, 256, nameof(Name), $"Product name can't be more than 256 characters")
                .IsGreaterThan(Stock, 0, nameof(Stock), $"Product stock can't be less than 0")
                .IsGreaterThan(Price, 0, nameof(Price), $"Product price can't be less than 0")
            );     
    }
}