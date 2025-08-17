using System.Text;
using System.Text.Json;
using OrderInfraData.Message;
using RabbitMQ.Client;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace OrderTests.IntegrationTests;

[TestFixture]
public class MessagePublisherIntegrationTests
{
    private const string QueueName = "TestQueue";
    private MessagePublisher _messagePublisher;
    private ConnectionFactory _connectionFactory;

    [SetUp]
    public void Setup()
    {
        _messagePublisher = new MessagePublisher();
        _connectionFactory = new ConnectionFactory() { HostName = "localhost" };
    }

    [Test]
    public async Task PublishMessageShouldSendMessageToQueue()
    {
        var message = new {Id = 287, Name = "Test" };
        
        await _messagePublisher.PublishMessage(message, QueueName);
        
        var factory = new ConnectionFactory() { HostName = "localhost" };
        await  using var connection = await factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();
        
        var result = await channel.BasicGetAsync(QueueName, autoAck: true);
        Assert.NotNull(result, "Message was not sent");
        
        var body = Encoding.UTF8.GetString(result.Body.ToArray());
        var deserialized = JsonSerializer.Deserialize<Dictionary<string, object>>(body);

        var deserializedId = ((JsonElement)deserialized!["Id"]).GetInt32();
        var deserializedName = ((JsonElement)deserialized!["Name"]).GetString();
        
        Assert.That(deserializedId, Is.EqualTo(287));
        Assert.That(deserializedName, Is.EqualTo("Test"));
    }
}