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

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<ClanCharacter> ClanCharacters { get; set; }

    public virtual DbSet<Faction> Factions { get; set; }

    public virtual DbSet<GameMode> GameModes { get; set; }

    public virtual DbSet<ListingCharacter> ListingCharacters { get; set; }

    public virtual DbSet<ListingClan> ListingClans { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitCharacter> UnitCharacters { get; set; }

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
        });

        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clans");

            entity.HasIndex(e => e.FactionId, "clans_factions_id_fk");

            entity.HasIndex(e => e.Name, "clans_pk").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.FactionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("faction_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Faction).WithMany(p => p.Clans)
                .HasForeignKey(d => d.FactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clans_factions_id_fk");
        });

        modelBuilder.Entity<ClanCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clan__characters");

            entity.HasIndex(e => e.Id, "clan__characters_pk").IsUnique();

            entity.HasIndex(e => new { e.CharacterId, e.ClanId }, "clan__characters_pk_2").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CharacterId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("character_id");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");

            entity.HasOne(d => d.Character).WithMany(p => p.ClanCharacters)
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("clan_characters_characters_id_fk");

            entity.HasOne(d => d.Clan).WithMany(p => p.ClanCharacters)
                .HasForeignKey(d => d.ClanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clan_characters_clans_id_fk");
        });

        modelBuilder.Entity<Faction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("factions");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<GameMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("game-mode");

            entity.HasIndex(e => e.FactionId, "game-mode_factions_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.FactionId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("faction_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Faction).WithMany(p => p.GameModes)
                .HasForeignKey(d => d.FactionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("game-mode_factions_id_fk");
        });

        modelBuilder.Entity<ListingCharacter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("listing-characters");

            entity.Property(e => e.CharacterId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("character_id");
            entity.Property(e => e.Clan)
                .HasMaxLength(64)
                .HasColumnName("clan")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
            entity.Property(e => e.GameMode)
                .HasMaxLength(64)
                .HasColumnName("game-mode")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.GameModeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game-mode_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UnitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("unit_id");
            entity.Property(e => e.UnitName)
                .HasMaxLength(64)
                .HasColumnName("unit_name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<ListingClan>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("listing-clans");

            entity.Property(e => e.CharactersN)
                .HasColumnType("bigint(21)")
                .HasColumnName("characters_n");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
            entity.Property(e => e.Faction)
                .HasMaxLength(64)
                .HasColumnName("faction")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("units");

            entity.HasIndex(e => e.GameModeId, "units_game-mode_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.GameModeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game-mode_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Number)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("number");
            entity.Property(e => e.Points)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("points");

            entity.HasOne(d => d.GameMode).WithMany(p => p.Units)
                .HasForeignKey(d => d.GameModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("units_game-mode_id_fk");
        });

        modelBuilder.Entity<UnitCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unit__characters");

            entity.HasIndex(e => e.CharacterId, "unit__characters_characters_id_fk");

            entity.HasIndex(e => e.UnitId, "unit__characters_units_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CharacterId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("character_id");
            entity.Property(e => e.UnitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("unit_id");

            entity.HasOne(d => d.Character).WithMany(p => p.UnitCharacters)
                .HasForeignKey(d => d.CharacterId)
                .HasConstraintName("unit__characters_characters_id_fk");

            entity.HasOne(d => d.Unit).WithMany(p => p.UnitCharacters)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("unit__characters_units_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
