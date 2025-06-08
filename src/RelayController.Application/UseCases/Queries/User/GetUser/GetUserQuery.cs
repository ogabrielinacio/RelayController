using MediatR;

namespace RelayController.Application.UseCases.Queries.User.GetUser;

public record GetUserQuery() : IRequest<GetUserResponse>
{
    public Guid Id { get; init; }
}