namespace InventoryInfraData.Message;

public interface IMessageConsumer
{
    Task ConsumeMessage<T>(string queueName, Action<T> action);
}