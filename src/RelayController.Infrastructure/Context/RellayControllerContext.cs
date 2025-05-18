using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Common;
using Microsoft.EntityFrameworkCore;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;

namespace RelayController.Infrastructure.Context
{
    public class RelayControllerContext : DbContext, IUnitOfWork
    {
        public RelayControllerContext(DbContextOptions<RelayControllerContext> options) : base(options) { }

        public DbSet<RelayControllerBoard> RelayControllerBoards => Set<RelayControllerBoard>();
        
        public DbSet<UserBoardsRoles> UserBoardsRoles => Set<UserBoardsRoles>();
        public DbSet<User> Users => Set<User>();
        
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