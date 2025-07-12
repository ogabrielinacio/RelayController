using MediatR;
using RelayController.Domain.Enums;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleMode;

public sealed record ToggleModeCommand : IRequest
{
    public Guid Id { get; init; }
    public Mode Mode { get; init; }
}