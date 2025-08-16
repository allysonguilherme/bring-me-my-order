namespace OrderBusiness.Entities;

public class Order
{
    public int Id { get; set; }
    public List<OrderProduct> OrderProducts { get; set; }
    public decimal TotalPrice { get; set; }
    
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