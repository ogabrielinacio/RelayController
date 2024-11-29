using RelayController.Infrastructure.Context.Models;
using RelayController.Infrastructure.Interceptors;
using RelayController.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace RelayController.Infrastructure.Context;

public static class DependencyInjection
{
    internal static IServiceCollection AddEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        var efConfiguration = configuration.GetRequiredSection(nameof(EfCoreConfiguration)).Get<EfCoreConfiguration>()!;
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddDbContext<IUnitOfWork, RelayControllerContext>((svcProvider, options) =>
        {
            options.AddInterceptors(svcProvider.GetServices<ISaveChangesInterceptor>())
                .UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

            if (efConfiguration.EnableLog)
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole(cLoggerOptions =>
                    {
                        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                        cLoggerOptions.FormatterName = env == "Production" ? "json" : "simple";
                    })
                ));

            if (efConfiguration.EnableSensitiveDataLogging)
                options.EnableSensitiveDataLogging();
        });
        
        var serviceProvider = services.BuildServiceProvider();
        RunMigrations(serviceProvider, configuration);
        
        return services;
    }
    public static void RunMigrations(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var efConfiguration = configuration.GetRequiredSection(nameof(EfCoreConfiguration)).Get<EfCoreConfiguration>();
        if (efConfiguration is null || !efConfiguration.RunMigrations) return;
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetService<RelayControllerContext>()!;
        dbContext.Database.Migrate();
    }
}