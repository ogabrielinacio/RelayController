using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.BoardCommands.AddRoutine;

public class AddRoutineHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<AddRoutineCommand, bool>
{
    public async Task<bool> Handle(AddRoutineCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.Routine.RelayControllerBoardId, cancellationToken)
                                   ?? throw new DomainNotFoundException("RelayControllerBoard was not found."); 
        relayControllerBoard.AddRoutine(command.Routine);
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return true; 
    }
}