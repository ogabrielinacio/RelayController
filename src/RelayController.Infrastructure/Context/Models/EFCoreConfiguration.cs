namespace RelayController.Infrastructure.Context.Models;

public class EfCoreConfiguration
{
    public bool EnableSensitiveDataLogging { get; init; }
    public bool RunMigrations { get; init; }
    public bool EnableLog { get; init; }
}