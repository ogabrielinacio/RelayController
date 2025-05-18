namespace RelayController.Domain.Common;

public class OperationResult
{
    public bool Succeeded { get; private set; }
    public List<string> Errors { get; } = new();

    private OperationResult(bool succeeded, IEnumerable<string>? errors = null)
    {
        Succeeded = succeeded;

        if (errors != null)
            Errors.AddRange(errors);
    }

    public static OperationResult Success() => new(true);
    public static OperationResult Failure(IEnumerable<string> errors) => new(false, errors);
}