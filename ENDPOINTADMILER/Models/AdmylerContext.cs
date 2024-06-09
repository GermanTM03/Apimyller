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

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<ReporteMantenimiento> ReporteMantenimientos { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Pkcita).HasName("PK__Citas__90724C3A6EB0F333");

            entity.Property(e => e.Pkcita).HasColumnName("PKCita");
            entity.Property(e => e.Fksucursal).HasColumnName("FKSucursal");
            entity.Property(e => e.Persona)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Razon)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Pkempleado).HasName("PK__Empleado__5C345F214D34C89F");

            entity.Property(e => e.Pkempleado).HasColumnName("PKEmpleado");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CorreoEmpresarial)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Fksucursal).HasColumnName("FKSucursal");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Rol)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.HasKey(e => e.Pkproducto).HasName("PK__Inventar__DA8DC6828BE11B09");

            entity.ToTable("Inventario");

            entity.Property(e => e.Pkproducto).HasColumnName("PKProducto");
            entity.Property(e => e.Costo).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fksucursal).HasColumnName("FKSucursal");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ReporteMantenimiento>(entity =>
        {
            entity.HasKey(e => e.Pkreporte).HasName("PK__ReporteM__45678A94BED5D096");

            entity.ToTable("ReporteMantenimiento");

            entity.Property(e => e.Pkreporte).HasColumnName("PKReporte");
            entity.Property(e => e.Color)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Combustible)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DetallesVisuales)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaIngreso).HasColumnType("datetime");
            entity.Property(e => e.Fkusuario).HasColumnName("FKUsuario");
            entity.Property(e => e.Marca)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroDeSerie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ObjetosDeValor)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Placas)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.Pksucursal).HasName("PK__Sucursal__E791EC9960D636F8");

            entity.ToTable("Sucursal");

            entity.Property(e => e.Pksucursal).HasColumnName("PKSucursal");
            entity.Property(e => e.CorreoS)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.Fkusuario).HasColumnName("FKUsuario");
            entity.Property(e => e.NombreNegocio)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.NumeroTelefono)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Pkusuario).HasName("PK__Usuarios__2BC9AA5873AAB83A");

            entity.Property(e => e.Pkusuario).HasColumnName("PKUsuario");
            entity.Property(e => e.ApellidoM)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoP)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Contra)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Pkventa).HasName("PK__Ventas__B2526D716612DF01");

            entity.Property(e => e.Pkventa).HasColumnName("PKVenta");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Fksucursal).HasColumnName("FKSucursal");
            entity.Property(e => e.Producto)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Vendedor)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Vienda)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
