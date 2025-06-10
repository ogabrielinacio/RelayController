using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Infrastructure.Context;
using RelayController.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Common;
using RelayController.Domain.Messaging;
using RelayController.Infrastructure.BackgroundServices;
using RelayController.Infrastructure.Email;
using RelayController.Infrastructure.Messaging;
using RelayController.Infrastructure.Security;

namespace RelayController.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddEfCore(configuration);
        services.AddSecurityAuthentication(configuration);
        services.AddEmail(configuration);
        services.AddMessageBus(configuration);
        services.AddBackgroundService(); 
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRelayControllerBoardRepository, RelayControllerBoardRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserBoardsRolesRepository, UserBoardsRolesRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        return services;
    }
    
    private static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitConfig = configuration.GetSection("RabbitMq");
        services.AddSingleton(new RabbitMqConnection(
            rabbitConfig["HostName"] ?? string.Empty,
            rabbitConfig["UserName"] ?? string.Empty,
            rabbitConfig["Password"] ?? string.Empty
        ));
        services.AddSingleton<IMessageBusService, RabbitMqService>();
        return services;
    }
    
    private static IServiceCollection AddBackgroundService(this IServiceCollection services)
    {
        services.AddHostedService<RelayControllerBackgroundService>();
        services.AddHostedService<RabbitMqBackgroundService>();
        return services;
    }
    
}