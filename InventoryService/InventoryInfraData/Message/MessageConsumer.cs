using System.Text;
using System.Text.Json;
using InventoryInfraData.Message.Interfaces;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryInfraData.Message;

public class MessageConsumer: IMessageConsumer
{
    private string connectionString;

    public MessageConsumer(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("RabbitMQ") ?? "amqp://guest:guest@localhost:5672";
    }
    
    public async Task ConsumeMessage<T>(string queueName, Func<T, Task> function, CancellationToken cancellationToken)
    {
        try
        {
            var factory = new ConnectionFactory(){Uri =  new Uri(connectionString) };
            await using var connection = await factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<T>(body);
                
                await function(obj);
            };
            
            await channel.BasicConsumeAsync(queueName, true, consumer);
            
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }     
    }
}