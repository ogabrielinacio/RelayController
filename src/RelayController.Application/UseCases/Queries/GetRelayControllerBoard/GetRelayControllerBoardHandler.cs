using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Exceptions;
using MediatR;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

public class GetRelayControllerBoardHandler(IRelayControllerBoardRepository relayControllerBoardRepository): IRequestHandler<GetRelayControllerBoardQuery, GetRelayControllerBoardResponse>
{
    public async Task<GetRelayControllerBoardResponse> Handle(GetRelayControllerBoardQuery request,
        CancellationToken cancellationToken)
    {
        var relayControllerBoard = await relayControllerBoardRepository.GetByIdAsync(request.Id, cancellationToken)
                     ?? throw new DomainNotFoundException("RelayControllerBoard was not found.");

        var response =  relayControllerBoard.ToGetRelayControllerBoardResponse();
        
        return response;
    }
}