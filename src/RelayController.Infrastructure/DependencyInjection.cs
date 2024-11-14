using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Infrastructure.Context;
using RelayController.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RelayController.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureDependencies(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddRepositories();
        services.AddEfCore(configuration);
        return services;
    }
    
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRelayControllerBoardRepository, RelayControllerBoardRepository>();
        return services;
    }
    
}