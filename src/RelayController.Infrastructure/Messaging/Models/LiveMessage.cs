namespace RelayController.Infrastructure.Messaging.Models;

public class LiveMessage
{
    public Guid Id { get; set; }
    public bool IsLive { get; set; }
    public bool IsEnable { get; set; }
}