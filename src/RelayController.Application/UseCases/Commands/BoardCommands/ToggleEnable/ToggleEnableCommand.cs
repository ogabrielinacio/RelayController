using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;

public sealed record ToggleEnableCommand : IRequest
{
    public Guid Id { get; init; }
    public bool IsEnable { get; init; }
}