namespace RelayController.API.Controllers.UserBoard.BecomeOwner;

public record BecomeOwnerRequest(Guid BoardId, string CustomName);