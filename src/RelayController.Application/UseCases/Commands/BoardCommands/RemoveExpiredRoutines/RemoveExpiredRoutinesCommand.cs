using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.RemoveExpiredRoutines;

public class RemoveExpiredRoutinesCommand : IRequest
{
    public Guid Id { get; init; } 
}