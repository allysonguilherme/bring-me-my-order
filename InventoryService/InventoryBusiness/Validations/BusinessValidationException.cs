namespace InventoryBusiness.Validations;

public class BusinessValidationException(string errorMessage) : ArgumentException(errorMessage)
{
    public static void When(bool condition, string message)
    {
        if(condition) throw new BusinessValidationException(message);
    }
}