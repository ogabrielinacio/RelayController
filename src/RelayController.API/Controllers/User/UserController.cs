using MediatR;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.User.LoginUser;
using RelayController.API.Controllers.User.RegisterUser;
using RelayController.Application.UseCases.Commands.UserCommands.CreateUser;
using RelayController.Application.UseCases.Commands.UserCommands.LoginUser;

namespace RelayController.API.Controllers.User;

[Route("user")]
public class UserController(ISender sender): AppController
{
   [HttpPost("register")]
   public async Task<IActionResult> RegisterUser( RegisterUserRequest newUser , CancellationToken cancellationToken)
   {
       
       var command = new CreateUserCommand
       {
            Name = newUser.Name,
            Email = newUser.Email,
            Password = newUser.Password
       }; 
       
       var response = await sender.Send(command, cancellationToken);
       
      return Ok(response);
   }
   
   [HttpPost("login")]
   public async Task<IActionResult> LoginUser( LoginUserRequest user , CancellationToken cancellationToken)
   {
       
       var command = new LoginUserCommand 
       {
            Email = user.Email,
            Password = user.Password
       }; 
       
       var response = await sender.Send(command, cancellationToken);
       
      return Ok(response);
   }
}