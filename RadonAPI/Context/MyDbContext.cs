using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using RadonAPI.Entities;

namespace RadonAPI.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<LogInside> LogInsides { get; set; }

    public virtual DbSet<LogOutside> LogOutsides { get; set; }

    public virtual DbSet<RadonLogger> RadonLoggers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=radon3w1b.database.windows.net;database=radon3w1b;user id=radon3w1b@radon3w1b;password=P@ssword123++;trusted_connection=true;TrustServerCertificate=True;integrated security=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3213E83F27CE33BE");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
            entity.Property(e => e.RadonLoggerId).HasColumnName("radonLoggerId");

            entity.HasOne(d => d.RadonLogger).WithMany(p => p.Locations)
                .HasForeignKey(d => d.RadonLoggerId)
                .HasConstraintName("FK__Location__radonL__6754599E");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3213E83F6BDD0119");

            entity.ToTable("Log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RadonLoggerId).HasColumnName("radonLoggerId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.RadonLogger).WithMany(p => p.Logs)
                .HasForeignKey(d => d.RadonLoggerId)
                .HasConstraintName("FK__Log__radonLogger__5EBF139D");
        });

        modelBuilder.Entity<LogInside>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogInsid__3213E83FEA25AFC8");

            entity.ToTable("LogInside");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.LogId).HasColumnName("logId");
            entity.Property(e => e.Radon).HasColumnName("radon");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Log).WithMany(p => p.LogInsides)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("FK__LogInside__logId__6477ECF3");
        });

        modelBuilder.Entity<LogOutside>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogOutsi__3213E83F449DFAAA");

            entity.ToTable("LogOutside");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.LogId).HasColumnName("logId");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

            entity.HasOne(d => d.Log).WithMany(p => p.LogOutsides)
                .HasForeignKey(d => d.LogId)
                .HasConstraintName("FK__LogOutsid__logId__619B8048");
        });

        modelBuilder.Entity<RadonLogger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RadonLog__3213E83F759EAEF5");

            entity.ToTable("RadonLogger");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ip");
            entity.Property(e => e.Password)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
