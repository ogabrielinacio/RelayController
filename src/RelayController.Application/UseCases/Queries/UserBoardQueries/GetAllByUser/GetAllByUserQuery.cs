using MediatR;
using RelayController.Domain.Aggregates.UserAggregates.Entities;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.GetAllByUser;

public record GetAllByUserQuery: IRequest<GetAllByUserResponse>
{
   public Guid UserId { get; init; }
}