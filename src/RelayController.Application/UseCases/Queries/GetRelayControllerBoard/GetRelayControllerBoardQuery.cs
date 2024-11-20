using MediatR;

namespace RelayController.Application.UseCases.Queries.GetRelayControllerBoard;

public sealed class GetRelayControllerBoardQuery : IRequest<GetRelayControllerBoardResponse>
{
    public Guid Id { get; init; }
}