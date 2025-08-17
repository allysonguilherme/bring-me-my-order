using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryInfraData.Message;

public class MessageConsumer: IMessageConsumer
{
    private const string Host = "localhost";
    
    public async Task ConsumeMessage<T>(string queueName, Action<T> action)
    {
        try
        {
            var factory = new ConnectionFactory(){HostName =  Host};
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
            consumer.ReceivedAsync += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var obj = JsonSerializer.Deserialize<T>(body);
                
                action(obj);
                return Task.CompletedTask;
            };
            
            await channel.BasicConsumeAsync(queueName, true, consumer);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }     
    }
}