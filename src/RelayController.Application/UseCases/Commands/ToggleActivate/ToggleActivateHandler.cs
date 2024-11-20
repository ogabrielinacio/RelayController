using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using MediatR;

namespace RelayController.Application.UseCases.Commands.ToggleActivate;

public class ToggleActivateHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivateCommand>
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
            if (relayControllerBoard.IsEnable)
            {
                //TODO: mqtt Turn Off
            }
        }
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}