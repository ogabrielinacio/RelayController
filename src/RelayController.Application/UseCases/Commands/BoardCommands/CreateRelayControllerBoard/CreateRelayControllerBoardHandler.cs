using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using MediatR;

namespace RelayController.Application.UseCases.Commands.BoardCommands.CreateRelayControllerBoard;

public class CreateRelayControllerHandler(IRelayControllerBoardRepository relayControllerBoardRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateRelayControllerBoardCommand, CreateRelayControllerBoardResponse>
{
    public async Task<CreateRelayControllerBoardResponse> Handle(CreateRelayControllerBoardCommand command, CancellationToken cancellationToken)
    {
        var relayControllerBoard = new RelayControllerBoard(
            command.IsActive, 
            command.IsEnable, 
            command.StartTime, 
            (Repeat)command.Repeat,
            command.EndTime
            );
        
        await relayControllerBoardRepository.AddAsync(relayControllerBoard, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
        return new CreateRelayControllerBoardResponse(relayControllerBoard.Id);
    }
}