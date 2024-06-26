﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ComputerClub.Model;

public partial class ComputerClubContext : DbContext
{
    public ComputerClubContext()
    {
    }

    public ComputerClubContext(DbContextOptions<ComputerClubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Computer> Computers { get; set; }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<Rate> Rates { get; set; }

    public virtual DbSet<Rent> Rents { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=HOME-PC;Initial Catalog=ComputerClub;Integrated Security=True;Encrypt=True;TrustServerCertificate=True")
            .EnableSensitiveDataLogging(true)
            .EnableDetailedErrors(true);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clubs__3213E83F9C5F08A6");

            entity.ToTable("clubs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Balance)
                .HasColumnType("money")
                .HasColumnName("balance");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(32)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Computer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__computer__3213E83F569F54ED");

            entity.ToTable("computers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.RateName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("rate_name");

            entity.HasOne(d => d.Club).WithMany(p => p.Computers)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__computers__clubI__5812160E");

            entity.HasOne(d => d.RateNameNavigation).WithMany(p => p.Computers)
                .HasForeignKey(d => d.RateName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__computers__rate___40058253");
        });

        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__incomes__3213E83F4D2993B9");

            entity.ToTable("incomes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.RateName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("rate_name");

            entity.HasOne(d => d.Club).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__incomes__clubId__3C69FB99");
        });

        modelBuilder.Entity<Rate>(entity =>
        {
            entity.HasKey(e => e.Name);

            entity.ToTable("rates");

            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
        });

        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rents__3213E83FB4F8AF02");

            entity.ToTable("rents");

            entity.HasIndex(e => e.ComputerId, "UQ__rents__805FE7FEAD83A0B5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComputerId).HasColumnName("computer_id");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("start_time");

            entity.HasOne(d => d.Computer).WithOne(p => p.Rent)
                .HasForeignKey<Rent>(d => d.ComputerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__rents__computer___71D1E811");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PK__sessions__BCAD3DD9F9E28EB8");

            entity.ToTable("sessions");

            entity.Property(e => e.ClubId)
                .ValueGeneratedNever()
                .HasColumnName("club_id");
            entity.Property(e => e.BeginTime)
                .HasColumnType("datetime")
                .HasColumnName("begin_time");
            entity.Property(e => e.UserLogin)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("user_login");

            entity.HasOne(d => d.Club).WithOne(p => p.Session)
                .HasForeignKey<Session>(d => d.ClubId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__sessions__club_i__59904A2C");

            entity.HasOne(d => d.UserLoginNavigation).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserLogin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sessions__user_l__5A846E65");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Login).HasName("PK_staff");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "unique_login").IsUnique();

            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Fullname)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("fullname");
            entity.Property(e => e.PassHash)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("pass_hash");
            entity.Property(e => e.Role)
                .IsRequired()
                .HasMaxLength(10)
                .HasColumnName("role");

            entity.HasMany(d => d.Clubs).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserClub",
                    r => r.HasOne<Club>().WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__UserClub__club_i__382F5661"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserLogin")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__UserClub__user_l__373B3228"),
                    j =>
                    {
                        j.HasKey("UserLogin", "ClubId").HasName("PK__UserClub__156B667372CB8E98");
                        j.ToTable("UserClub");
                        j.IndexerProperty<string>("UserLogin")
                            .HasMaxLength(50)
                            .HasColumnName("user_login");
                        j.IndexerProperty<int>("ClubId").HasColumnName("club_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
