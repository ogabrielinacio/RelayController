namespace RelayController.Domain.Messaging;

public interface IMessageBusService
{
    Task Publish(object data, string routingKey);
    Task Subscribe(string routingKey, Func<string, Task> onMessageReceived, CancellationToken cancellationToken = default);
}