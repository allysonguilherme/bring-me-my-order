using System.ComponentModel.DataAnnotations;
using Flunt.Notifications;
using Flunt.Validations;
using InventoryApplication.DTOs.interfaces;

namespace InventoryApplication.DTOs;

public class UpdateProductStockDto: Notifiable<Notification>, IValidatableDto 
{
    public int Quantity { get; set; }


    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .IsGreaterThan(Quantity, 0, "Quantity", "Quantity must be greater than 0"));
    }
}