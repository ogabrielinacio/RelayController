namespace RelayController.API.Controllers.UserBoard.AddUserToDevice;

public record AddUserRequest(Guid BoardId, int RoleId ,string Email);