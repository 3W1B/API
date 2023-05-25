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

    public virtual DbSet<Logger> Loggers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLogger> UserLoggers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=radon3w1b.database.windows.net;database=radon3w1b;user id=radon3w1b@radon3w1b;password=P@ssword123++;trusted_connection=true;TrustServerCertificate=True;integrated security=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Location__3213E83F43CE18C1");

            entity.ToTable("Location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.LoggerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("loggerId");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Log__3213E83FF52678B3");

            entity.ToTable("Log");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LoggerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("loggerId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<LogInside>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogInsid__3213E83FB608B70A");

            entity.ToTable("LogInside");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.LogId).HasColumnName("logId");
            entity.Property(e => e.RadonLta).HasColumnName("radonLta");
            entity.Property(e => e.RadonSta).HasColumnName("radonSta");
            entity.Property(e => e.Temperature).HasColumnName("temperature");

        });

        modelBuilder.Entity<LogOutside>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__LogOutsi__3213E83F9D155028");

            entity.ToTable("LogOutside");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Humidity).HasColumnName("humidity");
            entity.Property(e => e.LogId).HasColumnName("logId");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
        });

        modelBuilder.Entity<Logger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Logger__3213E83F5887F941");

            entity.ToTable("Logger");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3213E83F1476B331");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<UserLogger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserLogg__3213E83FAF8B5BD7");

            entity.ToTable("UserLogger");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LoggerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("loggerId");
            entity.Property(e => e.UserId).HasColumnName("userId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
