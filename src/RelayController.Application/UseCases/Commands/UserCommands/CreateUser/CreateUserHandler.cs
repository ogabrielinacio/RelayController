using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using MediatR;

namespace RelayController.Application.UseCases.Commands.UserCommands.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
   public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
   {
       var user = new User(
           command.Name, 
           command.Email 
       );
       
       await userRepository.AddAsync(user, command.Password, cancellationToken);
       await unitOfWork.CommitAsync(cancellationToken);
       return new CreateUserResponse(user.Id);
   }
}