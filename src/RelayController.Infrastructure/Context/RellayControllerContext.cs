using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace RelayController.Infrastructure.Context
{
    public class RelayControllerContext : DbContext, IUnitOfWork
    {
        public RelayControllerContext(DbContextOptions<RelayControllerContext> options) : base(options) { }

        public DbSet<RelayControllerBoard> RelayControllerBoards => Set<RelayControllerBoard>();
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RelayControllerContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}