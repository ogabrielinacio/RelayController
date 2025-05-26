using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelayController.Domain.Aggregates.RelayControllerAggregates;
using RelayController.Domain.Aggregates.UserAggregates;
using RelayController.Domain.Aggregates.UserAggregates.Entities;

namespace RelayController.Infrastructure.Context.Configurations;

public class UserBoardsRolesConfiguration : IEntityTypeConfiguration<UserBoardsRoles> 
{
    public void Configure(EntityTypeBuilder<UserBoardsRoles> builder)
    {
        builder.ToTable("user_boards_roles");
        
        builder.Property(p => p.UserId )
            .HasColumnName("user_id")
            .IsRequired();
        
        builder.Property(p => p.RelayControllerBoardId)
            .HasColumnName("relay_controller_board_id")
            .IsRequired();
        
        builder.HasOne<RelayControllerBoard>() 
            .WithMany()
            .HasForeignKey(p => p.RelayControllerBoardId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.OwnsOne(p => p.Role, role =>
        {
            role.Property(r => r.Id)
                .HasColumnName("role_id")
                .IsRequired();

            role.Property(r => r.Name)
                .HasColumnName("role_name")
                .IsRequired();
        });
        
        builder.HasIndex(p => new { p.UserId, p.RelayControllerBoardId }).IsUnique();
        
        builder.ConfigureAuditableEntity();
    }
}
