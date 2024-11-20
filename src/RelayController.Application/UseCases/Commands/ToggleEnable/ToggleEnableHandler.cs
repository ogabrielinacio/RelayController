using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Exceptions;
using MediatR;

namespace RelayController.Application.UseCases.Commands.ToggleEnable;

public class ToggleEnableHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<ToggleEnableCommand>
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
        

        //TODO:  mqtt  command.IsEnable
        
        relayControllerBoardRepository.Update(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}