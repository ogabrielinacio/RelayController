using MediatR;

namespace RelayController.Application.UseCases.Queries.UserBoardQueries.HasPermission;

public sealed record HasPermissionQuery : IRequest<bool>
{
    public Guid UserId { get; init; }
    public Guid BoardId { get; init; }
}