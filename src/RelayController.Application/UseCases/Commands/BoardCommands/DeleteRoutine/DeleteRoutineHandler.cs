using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.BoardCommands.DeleteRoutine;

public class DeleteRoutineHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoutineCommand>
{
    public async Task Handle(DeleteRoutineCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.BoardId, cancellationToken)
                                   ?? throw new DomainNotFoundException("RelayControllerBoard was not found."); 
        
        var routine = relayControllerBoard.GetRoutineById(command.RoutineId) ??
               throw new DomainNotFoundException("Routine Id was not found."); 
        
        relayControllerBoard.RemoveRoutine(routine.Id);
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        
        
    }
}