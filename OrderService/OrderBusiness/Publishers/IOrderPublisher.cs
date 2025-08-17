using OrderBusiness.Entities;

namespace OrderBusiness.Publishers;

public interface IOrderPublisher
{
    Task OrderCreatedPublish(Order order);
    Task OrderCancelledPublish(Order order);
}