using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.ValueObjects;

namespace RelayController.Domain.Aggregates.UserAggregates.Entities;

public sealed class UserBoardsRoles : AuditableEntity
{
    public Guid UserId { get; set; }
    public Guid RelayControllerBoardId { get; set; }
    
    public string CustomName { get; set; } = string.Empty;

    public Role Role { get; set; }
}