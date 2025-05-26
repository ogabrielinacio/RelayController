using RelayController.Domain.Aggregates.RelayControllerAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RelayController.Infrastructure.Context.Configurations;

public class RelayControllerConfiguration : IEntityTypeConfiguration<RelayControllerBoard>
{
    public void Configure(EntityTypeBuilder<RelayControllerBoard> builder)
    {
        builder.ToTable("relay_controller_boards");

        builder.Property(b => b.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
        
        builder.Property(b => b.IsEnable)
            .HasColumnName("is_enable")
            .IsRequired();
        
        builder.HasMany(b => b.Routines)
            .WithOne() 
            .HasForeignKey(r => r.RelayControllerBoardId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.ConfigureAuditableEntity();

        builder.ConfigureAuditableEntity();
    }
}