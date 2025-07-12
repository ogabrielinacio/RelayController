using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivateRoutine;

public sealed record ToggleActivateRoutineCommand : IRequest
{
    public Guid BoardId { get; init; } 
    public Guid RoutineId { get; init; } 
    public bool IsActive { get; init; }
}