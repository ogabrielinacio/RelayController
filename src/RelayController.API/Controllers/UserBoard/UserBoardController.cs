using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RelayController.API.Common;
using RelayController.API.Controllers.UserBoard.BecomeOwner;
using RelayController.Application.UseCases.Commands.UserBoardCommands.BecomeOwner;

namespace RelayController.API.Controllers.UserBoard;

[Authorize]
[Route("user-board")]
public class UserBoardController(ISender sender) : AppController
{
    [HttpPost("add")]
    public async Task<IActionResult> BecomeOwner([FromBody] BecomeOwnerRequest request, CancellationToken cancellationToken)
    {
        var userId = GetCurrentUserId();

        var command = new BecomeOwnerCommand
        {
            UserId = userId,
            BoardId = request.BoardId
        };
        
        var response = await sender.Send(command, cancellationToken);

        return Ok(response);
    }

 
}
    