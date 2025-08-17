namespace OrderInfraData.Message.Interfaces;

public interface IMessagePublisher
{
    Task PublishMessage<T>(T message,  string queueName);
}