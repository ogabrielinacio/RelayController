namespace RelayController.Domain.Exceptions;

public class DomainForbiddenAccessException : DomainException
{
    private const string DefaultMessage = "User does not have access to this operation.";

    public DomainForbiddenAccessException(): base(DefaultMessage)
    {
    }

    public DomainForbiddenAccessException(string message) : base(string.IsNullOrWhiteSpace(message)
        ? DefaultMessage
        : message)
    {
    }
}