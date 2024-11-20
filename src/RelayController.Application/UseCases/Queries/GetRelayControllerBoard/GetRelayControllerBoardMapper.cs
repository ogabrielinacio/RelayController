using RelayController.Domain.Aggregates.RelayControllerAggregates;
using Riok.Mapperly.Abstractions;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

[Mapper]
public static partial class GetRelayControllerBoardMapper
{
    public static partial GetRelayControllerBoardResponse ToGetRelayControllerBoardResponse(this RelayControllerBoard relayControllerBoard);
}