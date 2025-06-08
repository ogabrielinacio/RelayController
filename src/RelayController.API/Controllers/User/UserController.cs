using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RelayController.API.Common;
using RelayController.API.Controllers.User.LoginUser;
using RelayController.API.Controllers.User.RegisterUser;
using RelayController.API.Controllers.User.UpdatePassword;
using RelayController.Application.UseCases.Commands.UserCommands.CreateUser;
using RelayController.Application.UseCases.Commands.UserCommands.DeleteUser;
using RelayController.Application.UseCases.Commands.UserCommands.LoginUser;
using RelayController.Application.UseCases.Commands.UserCommands.UpdateEmail;
using RelayController.Application.UseCases.Commands.UserCommands.UpdateName;
using RelayController.Application.UseCases.Commands.UserCommands.UpdatePassword;
using RelayController.Application.UseCases.Queries.User.GetUser;

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

   [HttpGet("profile")]
   [Authorize]
   public async Task<IActionResult> GetProfile(CancellationToken cancellationToken)
   {
       var userId = GetCurrentUserId();
       var query = new GetUserQuery
       {
          Id = userId, 
       };
       var response = await sender.Send(query, cancellationToken );
       return Ok(response);
   }
   
   [HttpPut("update-password")]
   [Authorize]
   public async Task<IActionResult> UpdatePassword(UpdatePasswordRequest request, CancellationToken cancellationToken)
   {
       var userId = GetCurrentUserId();
       var command = new UpdatePasswordCommand
       {
           Id = userId,
           Password = request.Password,
           NewPassword = request.NewPassword
       };

       var response = await sender.Send(command, cancellationToken);
       return Ok("password updated");
   }
   
   
   [HttpPut("update-email")]
   [Authorize]
   public async Task<IActionResult> UpdateEmail(string newEmail, CancellationToken cancellationToken)
   {
       var userId = GetCurrentUserId();
       var command = new UpdateEmailCommand
       {
           Id = userId,
           NewEmail = newEmail
       };

       var response = await sender.Send(command, cancellationToken);
       return Ok("email updated");
   }
   
   [HttpPut("update-name")]
   [Authorize]
   public async Task<IActionResult> UpdateName(string newName  ,CancellationToken cancellationToken)
   {
       var userId = GetCurrentUserId();
       var command = new UpdateNameCommand 
       {
          Id = userId, 
          NewName = newName
       };
       var response = await sender.Send(command, cancellationToken );
       return Ok("name updated");
   }

   [HttpDelete("delete-account")]
   [Authorize]
   public async Task<IActionResult> DeleteAccount(CancellationToken cancellationToken)
   {
       
       var userId = GetCurrentUserId();
       var command = new DeleteUserCommand
       {
           Id = userId,
       };
       var response = await sender.Send(command, cancellationToken );
       return Ok("Account deleted");
   }

   
}