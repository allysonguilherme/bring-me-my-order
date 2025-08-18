using System.Text;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using OrderInfraData.Message.Interfaces;
using RabbitMQ.Client;

namespace OrderInfraData.Message;

public class MessagePublisher: IMessagePublisher
{
    private string ConnectionString { get; set; }
    public MessagePublisher(IConfiguration configuration)
    {
       ConnectionString = configuration.GetConnectionString("RabbitMQ") ?? "amqp://guest:guest@localhost:5672";
    }

    public async Task PublishMessage<T>(T message, string queueName)
    {
        try
        {
            var factory = new ConnectionFactory(){Uri =  new Uri(ConnectionString)};
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var messageAsJson = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(messageAsJson);
            
            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);
            Console.WriteLine($"Published {messageAsJson} to {queueName}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}