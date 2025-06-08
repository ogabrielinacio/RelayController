using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.UpdateEmail;

public record UpdateEmailCommand : IRequest<bool>
{
    public Guid Id { get; init; }
    public string NewEmail { get; init; } = string.Empty;
}
