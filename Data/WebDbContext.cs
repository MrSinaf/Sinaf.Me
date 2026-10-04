using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sinaf.Me.Data.Web;

namespace Sinaf.Me.Data;

public partial class WebDbContext : DbContext
{
    public WebDbContext(DbContextOptions<WebDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<LoginAttemp> LoginAttemps { get; set; }

    public virtual DbSet<Presence> Presences { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectLink> ProjectLinks { get; set; }

    public virtual DbSet<ProjectRepository> ProjectRepositories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("blogs");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.PublishAt)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("publish_at");
            entity.Property(e => e.Published).HasColumnName("published");
            entity.Property(e => e.Title)
                .HasMaxLength(64)
                .HasColumnName("title");
        });

        modelBuilder.Entity<LoginAttemp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("login-attemps");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Ip)
                .HasMaxLength(32)
                .HasColumnName("ip");
            entity.Property(e => e.Success).HasColumnName("success");
        });

        modelBuilder.Entity<Presence>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("presences");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Libelle)
                .HasMaxLength(128)
                .HasColumnName("libelle");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.HasIndex(e => e.UniqueName, "projects_pk").IsUnique();

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.Order)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(10) unsigned")
                .HasColumnName("order");
            entity.Property(e => e.Public).HasColumnName("public");
            entity.Property(e => e.UniqueName)
                .HasMaxLength(32)
                .HasColumnName("unique_name");
        });

        modelBuilder.Entity<ProjectLink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project__links");

            entity.HasIndex(e => e.ProjectId, "project__links_projects_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.IsIntern).HasColumnName("isIntern");
            entity.Property(e => e.Name)
                .HasMaxLength(32)
                .HasColumnName("name");
            entity.Property(e => e.Priority)
                .HasColumnType("tinyint(3) unsigned")
                .HasColumnName("priority");
            entity.Property(e => e.ProjectId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("project_id");
            entity.Property(e => e.Url)
                .HasMaxLength(256)
                .HasColumnName("url");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectLinks)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project__links_projects_id_fk");
        });

        modelBuilder.Entity<ProjectRepository>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("project-repositories");

            entity.HasIndex(e => e.ProjectId, "project-repositories_projects_id_fk");

            entity.Property(e => e.Id)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("id");
            entity.Property(e => e.Added)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("added");
            entity.Property(e => e.Branch)
                .HasMaxLength(32)
                .HasColumnName("branch");
            entity.Property(e => e.Commit)
                .HasMaxLength(128)
                .HasColumnName("commit");
            entity.Property(e => e.CommitId)
                .HasMaxLength(64)
                .HasColumnName("commit_id");
            entity.Property(e => e.Modified)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("modified");
            entity.Property(e => e.ProjectId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("project_id");
            entity.Property(e => e.Removed)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("removed");
            entity.Property(e => e.Repository)
                .HasMaxLength(128)
                .HasColumnName("repository");
            entity.Property(e => e.Update)
                .HasColumnType("datetime")
                .HasColumnName("update");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectRepositories)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("project-repositories_projects_id_fk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
