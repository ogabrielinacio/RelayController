namespace RelayController.Domain.Exceptions;

public class DomainValidationException : DomainException
{
    private const string DefaultMessage = "One or more validation rules were violated.";

    public DomainValidationException() : base(DefaultMessage)
    {
    }

    public DomainValidationException(string message) : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message)
    {
    }
}