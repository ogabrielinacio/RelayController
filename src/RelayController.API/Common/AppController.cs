using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace RelayController.API.Common;

[Microsoft.AspNetCore.Components.Route("api/[controller]")]
[ApiController]
public class AppController : ControllerBase
{
       protected Guid GetCurrentUserId() =>
           Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var id)
               ? id
               : throw new NullReferenceException("UserId claim not found or is not a valid GUID.");

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult Ok<T>(T data) where T : class =>
        base.Ok(new ApiResponseWithData<T> { Data = data, Success = true });
    
    protected IActionResult Ok(string message ) =>
            base.Ok(new ApiResponse{  Message = message, Success = true });

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult OkPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalCount = pagedList.TotalCount,
                Success = true
            }); 
}