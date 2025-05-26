using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RelayController.Domain.Aggregates.RelayControllerAggregates;

namespace RelayController.Infrastructure.Context.Configurations;


public class RoutineConfiguration : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder.ToTable("relay_controller_routines");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.RelayControllerBoardId)
            .HasColumnName("relay_controller_id")
            .IsRequired();

        builder.Property(r => r.Repeat)
            .HasColumnName("repeat")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(r => r.DayOfWeek)
            .HasColumnName("day_of_week")
            .HasConversion<string>();

        builder.Property(r => r.DayOfMonth)
            .HasColumnName("day_of_month");

        builder.OwnsOne(r => r.StartTime, b =>
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

        builder.OwnsOne(r => r.EndTime, b =>
        {
            b.Property(p => p.Hour).HasColumnName("end_hour");
            b.Property(p => p.Minute).HasColumnName("end_minute");
            b.Property(p => p.Second).HasColumnName("end_second");
        });

        builder.ConfigureAuditableEntity(); 
    }
}
