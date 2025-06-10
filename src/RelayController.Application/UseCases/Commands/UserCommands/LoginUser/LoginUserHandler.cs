using MediatR;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Enums;
using RelayController.Domain.Exceptions;

namespace RelayController.Application.UseCases.Commands.UserCommands.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand,LoginUserResponse>
{
    
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public LoginUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

        
   public async Task<LoginUserResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
   {
       var user = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
       if (user is null)
           throw new DomainForbiddenAccessException("Invalid email or password.");
       
       if(user.EmailConfirmed is not true) throw new DomainUnauthorizedAccessException("Email is not confirmed.");

       var isPasswordValid = await _userRepository.VerifyPasswordAsync(user, command.Password, cancellationToken);
       if (!isPasswordValid)
           throw new DomainForbiddenAccessException("Invalid email or password.");

       var token = _jwtTokenGenerator.GenerateToken(user, TokenPurpose.Authentication);

       return new LoginUserResponse(token);

   }
}