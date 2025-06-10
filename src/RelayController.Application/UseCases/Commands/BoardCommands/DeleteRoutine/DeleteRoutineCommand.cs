using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.DeleteRoutine;

public record DeleteRoutineCommand() : IRequest
{
   public Guid BoardId { get; init; } 
   public Guid RoutineId { get; init; } 
}