using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivateRoutine;

public class ToggleActivateRoutineHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivateRoutineCommand>
{
    public async Task Handle(ToggleActivateRoutineCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.BoardId, cancellationToken)
            ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");
        
        var routine = relayControllerBoard.GetRoutineById(command.RoutineId) ??
                      throw new DomainNotFoundException("Routine Id was not found."); 

        if (command.IsActive)
        {
            relayControllerBoard.ActivateRoutine(command.RoutineId);
        }
        else
        {
            relayControllerBoard.DeactivateRoutine(command.RoutineId);
        }
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}