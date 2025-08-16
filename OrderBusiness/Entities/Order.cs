namespace OrderBusiness.Entities;

public class Order
{
    public int Id { get; set; }
    public List<ProductOrder> OrderProducts { get; set; }
    public decimal TotalPrice { get; set; }

    public Order(List<ProductOrder> orderProducts)
    {
        if (orderProducts is null or { Count: 0 })
        {
            throw new ArgumentException("Cannot create order without any products");
        }
        OrderProducts = orderProducts;
        TotalPrice = orderProducts.Sum(p => p.GetTotalPrice());
    }
}