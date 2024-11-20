using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace RelayController.Infrastructure.Context.Factory;

public class RelayControllerContextFactory : IDesignTimeDbContextFactory<RelayControllerContext>
{
    public RelayControllerContext CreateDbContext(string[] args)
    {
        var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? string.Empty, "RelayController.API");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .Build();

            
        var optionsBuilder = new DbContextOptionsBuilder<RelayControllerContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

        return new RelayControllerContext(optionsBuilder.Options);
    }
}
