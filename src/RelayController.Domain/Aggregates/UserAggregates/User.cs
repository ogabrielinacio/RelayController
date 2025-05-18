using System.Security.Cryptography;
using System.Text;
using RelayController.Domain.Aggregates.UserAggregates.Entities;
using RelayController.Domain.Common;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.UserAggregates;
public class User : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    
    public byte[] PasswordHash { get; private set; }
    
    public byte[] PasswordSalt { get; private set; }

    private readonly List<UserBoardsRoles> _devicesRoles = new();
    public IReadOnlyCollection<UserBoardsRoles> DevicesRoles => _devicesRoles.AsReadOnly();

    protected User() {}

    public User(string name, string email)
    {
        Name = name;
        Email = email;
    }
    
    public void SetPassword(byte[] newHash, byte[] newSalt)
    {
        PasswordHash = newHash;
        PasswordSalt = newSalt;
    }    
    
    public void BecomeOwner(Guid boardId, Guid? newUserId = null)
    {
        Guid userId = newUserId ?? Id;

        if (_devicesRoles.Any(r => r.RelayControllerBoardId == boardId && r.Role == Role.Owner))
            throw new InvalidOperationException("This device already has an owner.");

        if (IsOwner(userId, boardId))
            return;

        _devicesRoles.Add(new UserBoardsRoles
        {
            UserId = userId,
            RelayControllerBoardId = boardId,
            Role = Role.Owner
        });
    }

    private bool IsOwner(Guid userId, Guid boardId)
    {
        return _devicesRoles.Any(u =>
            u.UserId == userId &&
            u.RelayControllerBoardId == boardId &&
            u.Role == Role.Owner);
    }

    public void RevokeOwner(Guid userId, Guid boardId)
    {
        if (!IsOwner(userId, boardId))
            throw new InvalidOperationException("User is not the owner of this device.");

        _devicesRoles.RemoveAll(r =>
            r.UserId == userId &&
            r.RelayControllerBoardId == boardId &&
            r.Role == Role.Owner);
    }

    public void ChangeOwner(Guid ownerId, Guid newOwnerId, Guid boardId)
    {
        if (!IsOwner(ownerId, boardId))
            throw new InvalidOperationException("Only the current owner can transfer ownership.");

        RevokeOwner(ownerId, boardId);
        BecomeOwner(boardId, newOwnerId);
    }

    public void AssignUser(Guid userId, Role role, Guid boardId)
    {
        if (!IsOwner(Id, boardId))
            throw new InvalidOperationException("You're not allowed to assign a user to this board.");

        if (_devicesRoles.Any(r => r.UserId == userId && r.RelayControllerBoardId == boardId))
            return;

        _devicesRoles.Add(new UserBoardsRoles
        {
            UserId = userId,
            RelayControllerBoardId = boardId,
            Role = role
        });
    }
}
