using OrderBusiness.Entities;
using OrderBusiness.Publishers;
using OrderInfraData.Message.Interfaces;
using OrderInfraData.Message.Mappings;

namespace OrderInfraData.Message;

public class OrderPublisher (IMessagePublisher messagePublisher) : IOrderPublisher
{
    private const string OrderCreatedQueue = "OrderCreated";
    
    public async Task OrderCreatedPublish(Order order)
    {
        try
        {
            await messagePublisher.PublishMessage(order.ToCreatedEvent(), OrderCreatedQueue);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}