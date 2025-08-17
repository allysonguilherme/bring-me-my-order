using OrderBusiness.Entities;
using OrderBusiness.Publishers;
using OrderInfraData.Message.Interfaces;

namespace OrderInfraData.Message;

public class OrderPublisher (IMessagePublisher messagePublisher) : IOrderPublisher
{
    private const string OrderCreatedQueue = "OrderCreated";
    
    public async Task OrderCreatedPublish(Order order)
    {
        try
        {
            await messagePublisher.PublishMessage(order, OrderCreatedQueue);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}