using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.HasPermission;

public class HasPermissionHandler(IUserBoardsRolesRepository userBoardsRolesRepository, IUnitOfWork unitOfWork) : IRequestHandler<HasPermissionQuery, bool>
{
    public async Task<bool> Handle(HasPermissionQuery query, CancellationToken cancellationToken)
    {
        return await userBoardsRolesRepository.HasAnyPermission(query.UserId, query.BoardId, cancellationToken);
    }
}