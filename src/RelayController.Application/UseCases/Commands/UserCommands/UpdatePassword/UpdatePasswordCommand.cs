using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdatePassword;

public record UpdatePasswordCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string Password { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}