using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RelayController.Domain.Messaging;

namespace RelayController.Infrastructure.Messaging;

public class RabbitMqService : IMessageBusService
{
    private readonly RabbitMqConnection _rabbitMqConnection;
    private readonly string _exchangeName = "amq.topic";
    private IChannel? _channel;

    public RabbitMqService (RabbitMqConnection rabbitMqConnection) {
        _rabbitMqConnection = rabbitMqConnection;
    }
    
    public async Task Publish(object data, string routingKey)
    {
        var connection = await _rabbitMqConnection.GetConnection();
        
        _channel ??= await connection.CreateChannelAsync();
        
        var type = data.GetType();
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
        };
        var payload = JsonSerializer.Serialize(data, options);
        var byteArray = Encoding.UTF8.GetBytes(payload);
        await _channel.BasicPublishAsync(exchange: _exchangeName, routingKey: routingKey, body: byteArray);
        
    }

    public async Task Subscribe(string routingKey, Func<string, Task> onMessageReceived, CancellationToken cancellationToken = default)
    {
        var connection = await _rabbitMqConnection.GetConnection();
        
        _channel ??= await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        var queueName = await _channel.QueueDeclareAsync(queue:"dotnet-subscriber" ,exclusive: false, durable: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
        
        await _channel.QueueBindAsync(queue: queueName, exchange: _exchangeName, routingKey: routingKey, cancellationToken: cancellationToken);
        
        await _channel.QueuePurgeAsync(queueName, cancellationToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.ReceivedAsync  += async (sender, eventsArgs) =>
        {
            var body = eventsArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            
            try
            {
                await onMessageReceived(message);
                await _channel.BasicAckAsync(eventsArgs.DeliveryTag, multiple: false, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message Error Rabbitmq: {ex.Message}");
                await _channel.BasicNackAsync(eventsArgs.DeliveryTag, multiple: false, requeue: false, cancellationToken: cancellationToken);
            }
            
            await onMessageReceived(message);
        };

        await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);
    }
}