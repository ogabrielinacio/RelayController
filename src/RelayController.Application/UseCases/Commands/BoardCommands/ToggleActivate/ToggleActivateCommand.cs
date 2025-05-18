using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivate;

public sealed record ToggleActivateCommand : IRequest
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
}