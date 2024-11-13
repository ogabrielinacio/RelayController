namespace RelayController.Domain.Exceptions;

public class DomainRuleViolationException : DomainException
{
    private const string DefaultMessage = "A domain rule was violated.";

    public DomainRuleViolationException() : base(DefaultMessage)
    {
    }

    public DomainRuleViolationException(string message) : base(string.IsNullOrWhiteSpace(message) ? DefaultMessage : message)
    {
    }
}