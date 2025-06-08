namespace RelayController.Application.UseCases.Queries.User.GetUser;

public record GetUserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
}