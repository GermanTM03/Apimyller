using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ENDPOINTADMILER.Models;

public partial class AdmylerContext : DbContext
{
    public AdmylerContext()
    {
    }

    public AdmylerContext(DbContextOptions<AdmylerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Inventory> Inventorys { get; set; }

    public virtual DbSet<MaintenanceReport> MaintenanceReports { get; set; }

    public virtual DbSet<Branch> Branchs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Pkappointment).HasName("PK__Citas__90724C3A6EB0F333");

            entity.Property(e => e.Pkappointment).HasColumnName("Pkappointment");
            entity.Property(e => e.BranchId).HasColumnName("BranchId");
            entity.Property(e => e.Person)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Empleado__5C345F214D34C89F");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeId");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CompanyEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchId).HasColumnName("BranchId");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Inventar__DA8DC6828BE11B09");

            entity.ToTable("Inventory");

            entity.Property(e => e.ProductId).HasColumnName("ProductId");
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.BranchId).HasColumnName("BranchId");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MaintenanceReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__ReporteM__45678A94BED5D096");

            entity.ToTable("MaintenanceReport");

            entity.Property(e => e.ReportId).HasColumnName("ReportId");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.FuelType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.VisualDetails)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.PkUser).HasColumnName("PkUser");
            entity.Property(e => e.Brand)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ValuableItems)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.LicensePlates)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Branch>(entity =>
        {
            entity.HasKey(e => e.BranchId).HasName("PK__Sucursal__E791EC9960D636F8");

            entity.ToTable("Branch");

            entity.Property(e => e.BranchId).HasColumnName("BranchId");
            entity.Property(e => e.Email)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.PkUser).HasColumnName("PkUser");
            entity.Property(e => e.BusinessName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RFC)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.PkUser).HasName("PK__Usuarios__2BC9AA5873AAB83A");

            entity.Property(e => e.PkUser).HasColumnName("PkUser");
            entity.Property(e => e.FullName)
                .HasMaxLength(40)
                .IsUnicode(false);
            
            entity.Property(e => e.Email)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .IsUnicode(false);
         
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Ventas__B2526D716612DF01");

            entity.Property(e => e.SaleId).HasColumnName("SaleId");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.BranchId).HasColumnName("BranchId");
            entity.Property(e => e.Product)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Seller)
                .HasMaxLength(30)
                .IsUnicode(false);
    
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
