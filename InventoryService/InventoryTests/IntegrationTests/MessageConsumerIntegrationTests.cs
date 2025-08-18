using System.Text;
using System.Text.Json;
using InventoryInfraData.Message;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Xunit.Abstractions;

namespace InventoryUnitTests.IntegrationTests;

public class MessageConsumerIntegrationTests
{
    private MessageConsumer _consumer;
    public MessageConsumerIntegrationTests(ITestOutputHelper testOutputHelper)
    {
        
        var inMemorySettings = new Dictionary<string, string>
        {
            { "ConnectionStrings:RabbitMQ", "amqp://guest:guest@localhost:5672" },
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings!)
            .Build(); 
        
        
        _consumer = new MessageConsumer(configuration);
    }

    private const string QueueName = "TestQueue";
    private const string Host = "localhost";

    [Fact]
    public async Task ConsumerMessageShouldInvokeActionWhenMessageIsReceived()
    {
        var receivedEvent = new ManualResetEvent(false);
        TestMessage? receivedMessage = null;

        Task UpdateMessage(TestMessage message)
        { 
            receivedMessage = message;
            receivedEvent.Set();
            return Task.CompletedTask;
        }
        _ = _consumer.ConsumeMessage<TestMessage>(QueueName, UpdateMessage, CancellationToken.None);
        
        var factory = new ConnectionFactory() { Uri = new Uri("amqp://guest:guest@localhost:5672") };
        await using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var message = new TestMessage { Id = 1, Name = "Item A" };
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: QueueName,
            mandatory: false,
            body: body);
        
        Assert.True(receivedEvent.WaitOne(TimeSpan.FromSeconds(5)), "Message was not received");
        Assert.Equal(1, receivedMessage!.Id);
        Assert.Equal("Item A", receivedMessage.Name);
    }

    

    private class TestMessage
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}