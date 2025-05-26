using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using MediatR;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.CreateUser;

public class CreateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand, CreateUserResponse>
{
   public async Task<CreateUserResponse> Handle(CreateUserCommand command, CancellationToken cancellationToken)
   {
       var exisitingUser = await userRepository.GetByEmailAsync(command.Email, cancellationToken);
       
       if (exisitingUser != null)
       {
           throw new DomainConflictException($"User with email {command.Email} already exists"); 
       }
       
       var user = new User(
           command.Name, 
           command.Email 
       );
       
       await userRepository.AddAsync(user, command.Password, cancellationToken);
       await unitOfWork.CommitAsync(cancellationToken);
       return new CreateUserResponse(user.Id);
   }
}