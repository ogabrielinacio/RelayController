namespace RelayController.Infrastructure.Context.Models;

public abstract class EfCoreConfiguration
{
    public bool EnableSensitiveDataLogging { get; init; }
    public bool RunMigrations { get; init; }
    public bool EnableLog { get; init; }
}