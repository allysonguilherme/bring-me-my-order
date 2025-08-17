namespace InventoryInfraData.Message.Interfaces;

public interface IMessageConsumer
{
    Task ConsumeMessage<T>(string queueName, Func<T, Task> function, CancellationToken cancellationToken);
}