using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Queries.User.GetUser;

public class GetUserHandler(IUserRepository userRepository) : IRequestHandler<GetUserQuery, GetUserResponse>
{
    public async Task<GetUserResponse> Handle(GetUserQuery query, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(query.Id, cancellationToken)
                   ?? throw new DomainNotFoundException("User not found.");
        
        return new GetUserResponse
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email
        };
    }
}