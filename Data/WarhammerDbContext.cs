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

    public virtual DbSet<Army> Armies { get; set; }

    public virtual DbSet<Battle> Battles { get; set; }

    public virtual DbSet<BattlePlayer> BattlePlayers { get; set; }

    public virtual DbSet<BattleUnit> BattleUnits { get; set; }

    public virtual DbSet<BattleUnitCharacter> BattleUnitCharacters { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<Citation> Citations { get; set; }

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Army>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("armies");

            entity.HasIndex(e => e.RaceId, "armies_races_id_fk");

            entity.HasIndex(e => e.GameId, "factions_game_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.GameId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game_id");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RaceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("race_id");

            entity.HasOne(d => d.Game).WithMany(p => p.Armies)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("factions_game_id_fk");

            entity.HasOne(d => d.Race).WithMany(p => p.Armies)
                .HasForeignKey(d => d.RaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("armies_races_id_fk");
        });

        modelBuilder.Entity<Battle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("battles");

            entity.HasIndex(e => e.GameId, "battles_games_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("curdate()")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.GameId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game_id");
            entity.Property(e => e.Points)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("points");

            entity.HasOne(d => d.Game).WithMany(p => p.Battles)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battles_games_id_fk");
        });

        modelBuilder.Entity<BattlePlayer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("battle_players");

            entity.HasIndex(e => e.BattleId, "battle_players_battles_id_fk");

            entity.HasIndex(e => e.PlayerId, "battle_players_players_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BattleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("battle_id");
            entity.Property(e => e.PlayerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("player_id");

            entity.HasOne(d => d.Battle).WithMany(p => p.BattlePlayers)
                .HasForeignKey(d => d.BattleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_players_battles_id_fk");

            entity.HasOne(d => d.Player).WithMany(p => p.BattlePlayers)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_players_players_id_fk");
        });

        modelBuilder.Entity<BattleUnit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("battle_units");

            entity.HasIndex(e => e.BattlePlayerId, "battle_units_battle_players_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BattlePlayerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("battle_player_id");
            entity.Property(e => e.DamageBlocked)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("damage_blocked");
            entity.Property(e => e.DamageDone)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("damage_done");
            entity.Property(e => e.DamageTaken)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("damage_taken");
            entity.Property(e => e.FailedCharges)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("failed_charges");
            entity.Property(e => e.ImpossibleSaves)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("impossible_saves");
            entity.Property(e => e.Kills)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("kills");
            entity.Property(e => e.Objectives)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("objectives");

            entity.HasOne(d => d.BattlePlayer).WithMany(p => p.BattleUnits)
                .HasForeignKey(d => d.BattlePlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_units_battle_players_id_fk");
        });

        modelBuilder.Entity<BattleUnitCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("battle_unit_characters");

            entity.HasIndex(e => e.BattleUnitId, "battle_unit_characters_battle_units_id_fk");

            entity.HasIndex(e => e.CharacterId, "battle_unit_characters_characters_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.BattleUnitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("battle_unit_id");
            entity.Property(e => e.CharacterId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("character_id");
            entity.Property(e => e.IsDead)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)")
                .HasColumnName("is_dead");
            entity.Property(e => e.KillsParticipating)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("kills_participating");

            entity.HasOne(d => d.BattleUnit).WithMany(p => p.BattleUnitCharacters)
                .HasForeignKey(d => d.BattleUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_unit_characters_battle_units_id_fk");

            entity.HasOne(d => d.Character).WithMany(p => p.BattleUnitCharacters)
                .HasForeignKey(d => d.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_unit_characters_characters_id_fk");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("characters");

            entity.HasIndex(e => e.ClanId, "characters_clans_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
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

            entity.HasOne(d => d.Clan).WithMany(p => p.Characters)
                .HasForeignKey(d => d.ClanId)
                .HasConstraintName("characters_clans_id_fk");
        });

        modelBuilder.Entity<Citation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("citations");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(528)
                .HasColumnName("content")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Source)
                .HasMaxLength(48)
                .HasColumnName("source")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Clan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("clans");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("games");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(128)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("players");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("races");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
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

            entity.HasIndex(e => e.ArmieId, "units_armies_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ArmieId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("armie_id");
            entity.Property(e => e.Cost)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("cost");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Number)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("number");

            entity.HasOne(d => d.Armie).WithMany(p => p.Units)
                .HasForeignKey(d => d.ArmieId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("units_armies_id_fk");

            entity.HasMany(d => d.Characters).WithMany(p => p.Units)
                .UsingEntity<Dictionary<string, object>>(
                    "CharacterUnit",
                    r => r.HasOne<Character>().WithMany()
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("character_units_characters_id_fk"),
                    l => l.HasOne<Unit>().WithMany()
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("character_units_units_id_fk"),
                    j =>
                    {
                        j.HasKey("UnitId", "CharacterId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("character_units");
                        j.HasIndex(new[] { "CharacterId" }, "character_units_characters_id_fk");
                        j.IndexerProperty<uint>("UnitId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("unit_id");
                        j.IndexerProperty<uint>("CharacterId")
                            .HasColumnType("int(10) unsigned")
                            .HasColumnName("character_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
