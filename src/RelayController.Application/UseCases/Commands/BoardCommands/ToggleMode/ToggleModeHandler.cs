using MediatR;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.Exceptions;
using RelayController.Domain.Messaging;

namespace RelayController.Application.UseCases.Commands.BoardCommands.ToggleMode;

    public class ToggleModeHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<ToggleModeCommand>
{
    public async Task Handle(ToggleModeCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(command.Id, cancellationToken)
            ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");

        if (command.Mode == Mode.Auto)
        {
           relayControllerBoard.ActivateAutoMode();
        } else if (command.Mode == Mode.Manual)
        {
           relayControllerBoard.ActivateManualMode();
        }
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}