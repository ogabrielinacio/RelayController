using RelayController.Domain.Common;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.UserAggregates.Entities;

public sealed class UserBoardsRoles : AuditableEntity
{
    public Guid UserId { get; set; }
    public Guid RelayControllerBoardId { get; set; }
    public Role Role { get; set; }
}