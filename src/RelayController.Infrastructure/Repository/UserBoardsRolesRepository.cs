using Microsoft.EntityFrameworkCore;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.ValueObjects;
using RelayController.Infrastructure.Context;

namespace RelayController.Infrastructure.Repository;

public class UserBoardsRolesRepository : IUserBoardsRolesRepository
{
    private readonly RelayControllerContext _context;

    public UserBoardsRolesRepository(RelayControllerContext context)
    {
        _context = context;
    }

    public async Task<UserBoardsRoles?> GetByUserIdAndBoardId(Guid userId, Guid boardId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles
            .FirstOrDefaultAsync(x => x.UserId == userId && x.RelayControllerBoardId == boardId, cancellationToken);
    } 
    
    public async Task<List<UserBoardsRoles>> GetByBoardId(Guid boardId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles
            .Where(r => r.RelayControllerBoardId == boardId)
            .ToListAsync(cancellationToken);
    } 
    
    public async Task AddAsync(UserBoardsRoles role, CancellationToken cancellationToken)
    {
        await _context.UserBoardsRoles.AddAsync(role, cancellationToken);
    }

    public async Task<bool> IsOwner(Guid userId, Guid boardId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles.AnyAsync(r =>
                r.UserId == userId &&
                r.RelayControllerBoardId == boardId &&
                r.Role.Id == Role.Owner.Id,
            cancellationToken);
    }
    
    public async Task<bool> AlreadyHasAOwner(Guid boardId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles.AnyAsync(r =>
                r.RelayControllerBoardId == boardId  &&
                r.Role.Id == Role.Owner.Id,
            cancellationToken);
    }
    
    public async Task<bool> HasAnyPermission(Guid userId, Guid boardId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles.AnyAsync(r =>
                r.UserId == userId &&
                r.RelayControllerBoardId == boardId,
            cancellationToken);
    }

    public async Task<bool> HasSpecificPermission(Guid userId, Guid boardId, Role role, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles.AnyAsync(r =>
                r.UserId == userId &&
                r.RelayControllerBoardId == boardId &&
                r.Role.Id == role.Id,
            cancellationToken);
    }

    public async Task<List<UserBoardsRoles>> GetAllByUserId(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.UserBoardsRoles
            .Where(r => r.UserId == userId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(UserBoardsRoles role, CancellationToken cancellationToken)
    {
        _context.UserBoardsRoles.Update(role);
    }

    
    public async Task RemoveAsync(UserBoardsRoles role, CancellationToken cancellationToken)
    {
         _context.UserBoardsRoles.Remove(role);
    }
}