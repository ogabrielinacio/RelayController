using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.UserAggregates;

public interface IUserBoardsRolesRepository
{
    Task AddAsync(UserBoardsRoles role, CancellationToken cancellationToken);
    Task<bool> IsOwner(Guid userId, Guid boardId, CancellationToken cancellationToken);
    Task<bool> AlreadyHasAOwner(Guid boardId, CancellationToken cancellationToken);
    Task<bool> HasAnyPermission(Guid userId, Guid boardId, CancellationToken cancellationToken);
    Task<bool> HasSpecificPermission(Guid userId, Guid boardId, Role role, CancellationToken cancellationToken);
    Task<List<UserBoardsRoles>> GetAllByUserId(Guid userId, CancellationToken cancellationToken);
    Task RemoveAsync(UserBoardsRoles role, CancellationToken cancellationToken);
}