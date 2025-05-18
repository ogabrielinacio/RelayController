using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using MediatR;
using RelayController.Domain.Messaging;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleActivate;

public class ToggleActivateHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork, IMessageBusService _messageBus) : IRequestHandler<ToggleActivateCommand>
{
    public async Task Handle(ToggleActivateCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");

        if (command.IsActive)
        {
            relayControllerBoard.Active();
        }
        else
        {
            relayControllerBoard.Deactivate();
            relayControllerBoard.Disable();
        }
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}