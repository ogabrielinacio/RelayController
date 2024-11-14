﻿using RelayController.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RelayController.Infrastructure.Context.Configurations;

public static class AuditableEntityConfiguration
{
    public static void ConfigureAuditableEntity<TBuilder>(this EntityTypeBuilder<TBuilder> builder,
        bool keyDefault = true) where TBuilder : AuditableEntity
    {
        if (keyDefault)
            builder.HasKey(e => e.Id);

        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedNever();
        builder.Property(e => e.Created).HasColumnName("created_date").IsRequired();
        builder.Property(e => e.Updated).HasColumnName("updated_date");

        builder.HasIndex(p => p.Created).IsUnique(false);
        builder.HasIndex(p => p.Updated).IsUnique(false);
    }
}