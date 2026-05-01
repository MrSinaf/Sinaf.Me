using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data.Warhammer;

namespace Sinaf.Me.Data;

public partial class WarhammerDbContext : DbContext
{
    public WarhammerDbContext(DbContextOptions<WarhammerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Character> Characters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("characters");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Commentary)
                .HasMaxLength(256)
                .HasColumnName("commentary")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
