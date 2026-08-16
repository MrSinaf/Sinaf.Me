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

    public virtual DbSet<BattleUnitsCharacter> BattleUnitsCharacters { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<CharacterDetail> CharacterDetails { get; set; }

    public virtual DbSet<Citation> Citations { get; set; }

    public virtual DbSet<Clan> Clans { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Paint> Paints { get; set; }

    public virtual DbSet<PaintSubType> PaintSubTypes { get; set; }

    public virtual DbSet<PaintType> PaintTypes { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_uca1400_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Army>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("armies");

            entity.HasIndex(e => e.GameId, "armies_games_id_fk");

            entity.HasIndex(e => e.RaceId, "armies_races_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.GameId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game_id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RaceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("race_id");

            entity.HasOne(d => d.Game).WithMany(p => p.Armies)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("armies_games_id_fk");

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
                .HasDefaultValueSql("curtime()")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.GameId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("game_id");

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

            entity.HasIndex(e => e.ArmyId, "battles__players_armies_id_fk");

            entity.HasIndex(e => e.ClanId, "battles__players_clans_id_fk");

            entity.HasIndex(e => e.PlayerId, "battles__players_players_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ArmyId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("army_id");
            entity.Property(e => e.BattleId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("battle_id");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
            entity.Property(e => e.PlayerId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("player_id");

            entity.HasOne(d => d.Battle).WithMany(p => p.BattlePlayers)
                .HasForeignKey(d => d.BattleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_players_battles_id_fk");
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
            entity.Property(e => e.Scores)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("scores");

            entity.HasOne(d => d.BattlePlayer).WithMany(p => p.BattleUnits)
                .HasForeignKey(d => d.BattlePlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle_units_battle_players_id_fk");
        });

        modelBuilder.Entity<BattleUnitsCharacter>(entity =>
        {
            entity.HasKey(e => new { e.CharacterId, e.BattleUnitId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("battle-units__characters");

            entity.HasIndex(e => e.BattleUnitId, "battle-units__characters_battle_units_id_fk");

            entity.Property(e => e.CharacterId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("character_id");
            entity.Property(e => e.BattleUnitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("battle_unit_id");
            entity.Property(e => e.IsDead)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)")
                .HasColumnName("is_dead");
            entity.Property(e => e.KillsParticipating)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("kills_participating");

            entity.HasOne(d => d.BattleUnit).WithMany(p => p.BattleUnitsCharacters)
                .HasForeignKey(d => d.BattleUnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle-units__characters_battle_units_id_fk");

            entity.HasOne(d => d.Character).WithMany(p => p.BattleUnitsCharacters)
                .HasForeignKey(d => d.CharacterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("battle-units__characters_characters_id_fk");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("characters");

            entity.HasIndex(e => e.ClanId, "characters_clans_id_fk");

            entity.HasIndex(e => e.UnitId, "characters_units_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
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
            entity.Property(e => e.ThumbnailS)
                .HasDefaultValueSql("'100'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("thumbnail_s");
            entity.Property(e => e.ThumbnailX)
                .HasColumnType("int(11)")
                .HasColumnName("thumbnail_x");
            entity.Property(e => e.ThumbnailY)
                .HasColumnType("int(11)")
                .HasColumnName("thumbnail_y");
            entity.Property(e => e.UnitId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("unit_id");

            entity.HasOne(d => d.Clan).WithMany(p => p.Characters)
                .HasForeignKey(d => d.ClanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_clans_id_fk");

            entity.HasOne(d => d.Unit).WithMany(p => p.Characters)
                .HasForeignKey(d => d.UnitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("characters_units_id_fk");
        });

        modelBuilder.Entity<CharacterDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("character-details");

            entity.Property(e => e.ClanId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("clan_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(512)
                .HasColumnName("description")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.OrderType)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("order_type");
            entity.Property(e => e.OrderUnit)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("order_unit");
            entity.Property(e => e.ThumbnailS)
                .HasDefaultValueSql("'100'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("thumbnail_s");
            entity.Property(e => e.ThumbnailX)
                .HasColumnType("int(11)")
                .HasColumnName("thumbnail_x");
            entity.Property(e => e.ThumbnailY)
                .HasColumnType("int(11)")
                .HasColumnName("thumbnail_y");
            entity.Property(e => e.Unit)
                .HasMaxLength(64)
                .HasColumnName("unit")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.UnitType)
                .HasMaxLength(32)
                .HasColumnName("unit_type")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
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

            entity.HasIndex(e => e.UniqueName, "clans_pk").IsUnique();

            entity.HasIndex(e => e.RaceId, "clans_races_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(128)
                .HasDefaultValueSql("''")
                .HasColumnName("currency")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.RaceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("race_id");
            entity.Property(e => e.UniqueName)
                .HasMaxLength(64)
                .HasColumnName("unique_name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");

            entity.HasOne(d => d.Race).WithMany(p => p.Clans)
                .HasForeignKey(d => d.RaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("clans_races_id_fk");
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

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("images");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("curtime()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.GridX)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("grid-x");
            entity.Property(e => e.GridY)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("grid-y");
            entity.Property(e => e.Path)
                .HasMaxLength(64)
                .HasColumnName("path")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<Paint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paints");

            entity.HasIndex(e => e.SubTypeId, "paints_paint_sub_types_id_fk");

            entity.HasIndex(e => e.TypeId, "peints_peint_types_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Hex)
                .HasMaxLength(6)
                .HasColumnName("hex")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.SubTypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("sub_type_id");
            entity.Property(e => e.TypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("type_id");

            entity.HasOne(d => d.SubType).WithMany(p => p.Paints)
                .HasForeignKey(d => d.SubTypeId)
                .HasConstraintName("paints_paint_sub_types_id_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Paints)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("peints_peint_types_id_fk");
        });

        modelBuilder.Entity<PaintSubType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paint_sub_types");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
        });

        modelBuilder.Entity<PaintType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paint_types");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
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

            entity.HasIndex(e => e.RaceId, "units_races_id_fk");

            entity.HasIndex(e => e.TypeId, "units_unit_types_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Number)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("number");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.RaceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("race_id");
            entity.Property(e => e.TypeId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("type_id");

            entity.HasOne(d => d.Race).WithMany(p => p.Units)
                .HasForeignKey(d => d.RaceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("units_races_id_fk");

            entity.HasOne(d => d.Type).WithMany(p => p.Units)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("units_unit_types_id_fk");
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("unit_types");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name")
                .UseCollation("utf8mb3_uca1400_ai_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("'1'")
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("order");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
