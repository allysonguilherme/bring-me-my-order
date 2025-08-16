namespace OrderBusiness.Entities;

public class Order
{
    public int Id { get; init; }
    public List<OrderProduct> OrderProducts { get; init; }
    public decimal TotalPrice { get; init; }
    
    //parameterless constructor used by EF Core
    private Order() { }

    public Order(List<OrderProduct> orderProducts)
    {
        if (orderProducts is null or { Count: 0 })
        {
            throw new ArgumentException("Cannot create order without any products");
        }
        OrderProducts = orderProducts;
        TotalPrice = orderProducts.Sum(p => p.GetTotalPrice());
    }
}
