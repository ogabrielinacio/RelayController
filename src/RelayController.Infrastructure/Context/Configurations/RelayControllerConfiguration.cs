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
        
        builder.Property(b => b.Repeat)
            .HasColumnName("repeat")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(b => b.DayOfWeek)
            .HasColumnName("day_of_week")
            .HasConversion<string>();

        builder.Property(b => b.DayOfMonth)
            .HasColumnName("day_of_month");
        
        builder.OwnsOne(b => b.StartTime, b =>
        {
            b.Property(p => p.Hour)
                .HasColumnName("start_hour")
                .IsRequired();

            b.Property(p => p.Minute)
                .HasColumnName("start_minute")
                .IsRequired();

            b.Property(p => p.Second)
                .HasColumnName("start_second")
                .IsRequired();
        });

        builder.OwnsOne(b => b.EndTime, b =>
        {
            b.Property(p => p.Hour)
                .HasColumnName("end_hour");

            b.Property(p => p.Minute)
                .HasColumnName("end_minute");

            b.Property(p => p.Second)
                .HasColumnName("end_second");
        });

        builder.ConfigureAuditableEntity();
    }
}