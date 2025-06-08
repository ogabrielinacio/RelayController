namespace RelayController.API.Controllers.User.UpdatePassword;

public record UpdatePasswordRequest()
{
    public string Password { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}