namespace RelayController.API.Controllers.UserBoard.DeleteUserFromDevice;

public sealed record DeleteUserFromDeviceRequest
{
   public string Email { get; init; }   = string.Empty;
   public Guid BoardId { get; init; } 
}