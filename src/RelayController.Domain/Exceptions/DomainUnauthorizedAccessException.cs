namespace RelayController.Domain.Exceptions;

public class DomainUnauthorizedAccessException : DomainException
{
    private const string DefaultMessage = "Access to this resource is unauthorized.";

    public DomainUnauthorizedAccessException() : base(DefaultMessage)
    {
    }

    public DomainUnauthorizedAccessException(string message) : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message)
    {
    }
}