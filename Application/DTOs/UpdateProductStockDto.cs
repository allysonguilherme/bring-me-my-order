using System.ComponentModel.DataAnnotations;
using Application.DTOs.interfaces;
using Flunt.Notifications;
using Flunt.Validations;

namespace Application.DTOs;

public class UpdateProductStockDto: Notifiable<Notification>, IValidatableDto 
{
    public int Quantity { get; set; }


    public void Validate()
    {
        AddNotifications(new Contract<Notification>()
            .IsGreaterThan(Quantity, 0, "Quantity", "Quantity must be greater than 0"));
    }
}