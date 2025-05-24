using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.GetAllByUser;

public class GetAllByUserHandler(IUserBoardsRolesRepository boardsRolesRepository): IRequestHandler<GetAllByUserQuery,GetAllByUserResponse>
{
    public async Task<GetAllByUserResponse> Handle(GetAllByUserQuery query, CancellationToken cancellationToken) {
        var response = await boardsRolesRepository.GetAllByUserId(query.UserId, cancellationToken);
        return new GetAllByUserResponse
        {
            UserId = query.UserId,
            Boards = response.Select(x => new BoardInfo
            {
                RelayControllerBoardId = x.RelayControllerBoardId,
                CustomName = x.CustomName,
                Role = x.Role.Name
            }).ToList()
        };
    } 
}