using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.HasSpecificPermission;

public class HasSpecificPermissionHandler(IUserBoardsRolesRepository userBoardsRolesRepository) : IRequestHandler<HasSpecificPermissionQuery, bool>
{
    public async Task<bool> Handle(HasSpecificPermissionQuery query, CancellationToken cancellationToken)
    {
        return await userBoardsRolesRepository.HasSpecificPermission(query.UserId, query.BoardId, query.Role, cancellationToken);
    }
}