using System.Text;
using System.Text.Json;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using InventoryInfraData.Message;
using RabbitMQ.Client;
using Xunit.Abstractions;

namespace InventoryUnitTests.IntegrationTests;

public class MessageConsumerIntegrationTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public MessageConsumerIntegrationTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private const string QueueName = "TestQueue";
    private const string Host = "localhost";

    [Fact]
    public async Task ConsumerMessageShouldInvokeActionWhenMessageIsReceived()
    {
        var consumer = new MessageConsumer();
        var receivedEvent = new ManualResetEvent(false);
        TestMessage? receivedMessage = null;
        
        Action<TestMessage> action = (TestMessage message) =>
        {
            receivedMessage = message;
            receivedEvent.Set();
        };
        
        _ = consumer.ConsumeMessage<TestMessage>(QueueName, action);
        
        var factory = new ConnectionFactory() { HostName = Host };
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