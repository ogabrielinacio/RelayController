using MediatR;
using RelayController.Domain.ValueObjects;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.HasSpecificPermission;

public record HasSpecificPermissionQuery(Guid UserId, Guid BoardId, Role Role) : IRequest<bool>;