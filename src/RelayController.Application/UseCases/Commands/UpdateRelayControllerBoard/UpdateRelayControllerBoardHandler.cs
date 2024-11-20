using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.Exceptions;
using MediatR;

namespace RelayController.Application.UseCases.Commands.UpdateRelayControllerBoard;

public class UpdateRelayControllerBoardHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<UpdateRelayControllerBoardCommand>
{
    public async Task Handle(UpdateRelayControllerBoardCommand command, CancellationToken cancellationToken)
    {
        var dateTime = DateTime.Now;
        var startTime = command.StartTime ?? dateTime;

        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.Id, cancellationToken)
                     ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");

        relayControllerBoard.Update(startTime, (Repeat)command.Repeat, command.EndTime, command.IsEnable, command.IsActive);
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}