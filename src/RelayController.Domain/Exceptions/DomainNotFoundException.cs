namespace RelayController.Domain.Exceptions;

public class DomainNotFoundException : DomainException
{
    private const string DefaultMessage = "The specified data was not found.";

    public DomainNotFoundException() : base(DefaultMessage)
    {
    }
    public DomainNotFoundException(string message) : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message)
    {
    }
}