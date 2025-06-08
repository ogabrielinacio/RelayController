using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.BoardCommands.RemoveExpiredRoutines;

public class RemoveExpiredRoutinesHandler (IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveExpiredRoutinesCommand>
{
    public async Task Handle(RemoveExpiredRoutinesCommand command, CancellationToken cancellationToken)
    {
        var datetime = DateTime.Now;
        
       var board =  await relayControllerBoardRepository.GetByIdAsync(command.Id, cancellationToken)
                ?? throw new DomainNotFoundException("RelayControllerBoard was not found."); 
       
        board.RemoveExpiredRoutines(datetime);
        relayControllerBoardRepository.Update(board, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}