using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using MediatR;
using RelayController.Domain.Messaging;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleEnable;

public class ToggleEnableHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork, IMessageBusService _messageBus) : IRequestHandler<ToggleEnableCommand>
{
    public async Task Handle(ToggleEnableCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");

        if (command.IsEnable)
        {
            relayControllerBoard.Enable();
        }
        else
        {
            relayControllerBoard.Disable();
        }
        
        await _messageBus.Publish(command, "relay.data");
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}