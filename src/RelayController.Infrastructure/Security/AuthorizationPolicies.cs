namespace RelayController.Infrastructure.Security;

public static class AuthorizationPolicies
{
    public const string RequireConfirmEmail = "RequireConfirmEmailToken";
    public const string RequireResetPassword = "RequireResetPasswordToken";
    public const string RequireAuthentication = "RequireAuthToken";
}