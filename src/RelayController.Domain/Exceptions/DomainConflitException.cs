namespace RelayController.Domain.Exceptions;

public class DomainConflictException : DomainException
{
    private const string DefaultMessage = "A conflict occurred with the current state of the resource.";

    public DomainConflictException() : base(DefaultMessage)
    {
    }

    public DomainConflictException(string message) : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message)
    {
    }
}