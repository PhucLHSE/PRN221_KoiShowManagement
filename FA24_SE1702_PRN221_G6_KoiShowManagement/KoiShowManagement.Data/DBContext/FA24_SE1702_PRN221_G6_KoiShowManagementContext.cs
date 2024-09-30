﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using KoiShowManagement.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KoiShowManagement.Data.DBContext;

public partial class FA24_SE1702_PRN221_G6_KoiShowManagementContext : DbContext
{
    public FA24_SE1702_PRN221_G6_KoiShowManagementContext()
    {
    }

    public FA24_SE1702_PRN221_G6_KoiShowManagementContext(DbContextOptions<FA24_SE1702_PRN221_G6_KoiShowManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<AnimalVariety> AnimalVarieties { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<CompetitionCategory> CompetitionCategories { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<FinalResult> FinalResults { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PointOnProgressing> PointOnProgressings { get; set; }

    public virtual DbSet<Registration> Registrations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public static string GetConnectionString(string connectionStringName)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        string connectionString = config.GetConnectionString(connectionStringName);
        return connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection"));

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=ADMIN-PC;Initial Catalog=FA24_SE1702_PRN221_G6_KoiShowManagement;User ID=sa;Password=12345;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasKey(e => e.AnimalId).HasName("PK__Animals__A21A73074A6C51B4");

            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Variety).WithMany(p => p.Animals)
                .HasForeignKey(d => d.VarietyId)
                .HasConstraintName("FK__Animals__Variety__5BE2A6F2");
        });

        modelBuilder.Entity<AnimalVariety>(entity =>
        {
            entity.HasKey(e => e.VarietyId).HasName("PK__AnimalVa__08E3A068D5D2966C");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.CompetitionId).HasName("PK__Competit__8F32F4D305DAF4E6");

            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<CompetitionCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Competit__19093A0B91584A2A");

            entity.Property(e => e.CategoryName)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.JudgingCriteria).HasMaxLength(255);
            entity.Property(e => e.RequiredHealthStatus).HasMaxLength(100);

            entity.HasOne(d => d.Competition).WithMany(p => p.CompetitionCategories)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Competiti__Compe__5812160E");

            entity.HasOne(d => d.Variety).WithMany(p => p.CompetitionCategories)
                .HasForeignKey(d => d.VarietyId)
                .HasConstraintName("FK__Competiti__Varie__571DF1D5");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__Feedback__6A4BEDD627B0C858");

            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedbackType).HasMaxLength(50);
            entity.Property(e => e.IsAnonymous).HasDefaultValue(false);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.IsResponded).HasDefaultValue(false);
            entity.Property(e => e.Platform).HasMaxLength(50);
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.SeverityLevel).HasMaxLength(50);
            entity.Property(e => e.Status).HasDefaultValue(0);
            entity.Property(e => e.VisibilityLevel).HasMaxLength(50);

            entity.HasOne(d => d.Competition).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Feedbacks__Compe__76969D2E");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Feedbacks__UserI__778AC167");
        });

        modelBuilder.Entity<FinalResult>(entity =>
        {
            entity.HasKey(e => e.CompetitionResultId).HasName("PK__FinalRes__4B2A04176EB109A8");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.FinalResults)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__FinalResu__Categ__6C190EBB");

            entity.HasOne(d => d.Competition).WithMany(p => p.FinalResults)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__FinalResu__Compe__6B24EA82");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38E4C7BE06");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(100);
            entity.Property(e => e.Vatrate).HasColumnName("VATRate");

            entity.HasOne(d => d.Registration).WithMany(p => p.Payments)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__Payments__Regist__6EF57B66");
        });

        modelBuilder.Entity<PointOnProgressing>(entity =>
        {
            entity.HasKey(e => e.PointId).HasName("PK__PointOnP__40A977E151BA99B0");

            entity.ToTable("PointOnProgressing");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Category).WithMany(p => p.PointOnProgressings)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__PointOnPr__Categ__6754599E");

            entity.HasOne(d => d.Jury).WithMany(p => p.PointOnProgressings)
                .HasForeignKey(d => d.JuryId)
                .HasConstraintName("FK__PointOnPr__JuryI__656C112C");

            entity.HasOne(d => d.Registration).WithMany(p => p.PointOnProgressings)
                .HasForeignKey(d => d.RegistrationId)
                .HasConstraintName("FK__PointOnPr__Regis__66603565");
        });

        modelBuilder.Entity<Registration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__Registra__6EF58810C64F7118");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.Animal).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.AnimalId)
                .HasConstraintName("FK__Registrat__Anima__60A75C0F");

            entity.HasOne(d => d.Competition).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.CompetitionId)
                .HasConstraintName("FK__Registrat__Compe__5FB337D6");

            entity.HasOne(d => d.User).WithMany(p => p.Registrations)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Registrat__UserI__619B8048");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AA36570AB");

            entity.Property(e => e.RoleName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDE645E33");

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Username).IsRequired();

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}