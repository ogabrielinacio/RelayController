namespace RelayController.API.Controllers.UserBoard.ChangeCustomName;

public class ChangeCustomNameRequest
{
    public Guid BoardId { get; set; }
    public string NewName { get; set; } = string.Empty;
}