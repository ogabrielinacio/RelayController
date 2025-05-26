using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;

namespace RelayController.Application.UseCases.Commands.BoardCommands.AddRoutine;

public sealed record AddRoutineCommand : IRequest<bool>
{
    public Routine Routine { get; init; } 
}