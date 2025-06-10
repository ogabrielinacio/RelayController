using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.User.LoginUser;
using RelayController.API.Controllers.User.RegisterUser;
using RelayController.API.Controllers.User.UpdatePassword;
using RelayController.Application.UseCases.Commands.UserCommands.ConfirmEmail;
using RelayController.Application.UseCases.Commands.UserCommands.CreateUser;
using RelayController.Application.UseCases.Commands.UserCommands.DeleteUser;
using RelayController.Application.UseCases.Commands.UserCommands.LoginUser;
using RelayController.Application.UseCases.Commands.UserCommands.ResetPassword;
using RelayController.Application.UseCases.Commands.UserCommands.SendConfirmEmail;
using RelayController.Application.UseCases.Commands.UserCommands.SendRecoveryPasswordEmail;
using RelayController.Application.UseCases.Commands.UserCommands.UpdateEmail;
using RelayController.Application.UseCases.Commands.UserCommands.UpdateName;
using RelayController.Application.UseCases.Commands.UserCommands.UpdatePassword;
using RelayController.Application.UseCases.Queries.User.GetUser;
using RelayController.Infrastructure.Security;

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
   
    [HttpPost("send-recovery-password")]
    public async Task<IActionResult> SendRecoveryPasswordEmail( string  email , CancellationToken cancellationToken)
    {
       
        var command = new SendRecoveryPasswordEmailCommand 
        {
            Email = email,
        }; 
       
        await sender.Send(command, cancellationToken);
       
        return Ok($"check your email: {email}");
    }
   
    [HttpPost("send-confirm-email")]
    public async Task<IActionResult> SendConfirmEmail( string  email , CancellationToken cancellationToken)
    {
       
        var command = new SendConfirmEmailCommand 
        {
            Email = email,
        }; 
       
        await sender.Send(command, cancellationToken);
       
        return Ok($"check your email: {email}");
    }
   
    [HttpPost("confirm-email")]
    [Authorize(Policy = AuthorizationPolicies.RequireConfirmEmail)]
    public async Task<IActionResult> ConfirmEmail(CancellationToken cancellationToken)
    {
       
        var userId = GetCurrentUserId();
        var command = new ConfirmEmailCommand 
        {
            Id = userId
        }; 
       
        await sender.Send(command, cancellationToken);
       
        return Ok($"email confirmed");
    }
   
    [HttpPost("reset-password")]
    [Authorize(Policy = AuthorizationPolicies.RequireResetPassword)]
    public async Task<IActionResult> ResetPassword(string newPassword, CancellationToken cancellationToken)
    {
       
        var userId = GetCurrentUserId();
        var command = new  ResetPasswordCommand
        {
            Id = userId,
            NewPassword = newPassword
        }; 
       
        await sender.Send(command, cancellationToken);
       
        return Ok($"password was reset");
    }
  
   
   

    [HttpGet("profile")]
    [Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
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
    [Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
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
    [Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
    public async Task<IActionResult> UpdateEmail(string newEmail, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();
        var command = new UpdateEmailCommand
        {
            Id = userId,
            NewEmail = newEmail
        };

        var response = await sender.Send(command, cancellationToken);
        return Ok("email updated, please confirm your new email address");
    }
   
    [HttpPut("update-name")]
    [Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
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
    [Authorize(Policy = AuthorizationPolicies.RequireAuthentication)]
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