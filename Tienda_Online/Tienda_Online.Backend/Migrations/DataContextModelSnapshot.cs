﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tienda_Online.Backend.Data;

#nullable disable

namespace Tienda_Online.Backend.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.CarritoDeCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadProducto")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<double>("ValorCantidadProducto")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("CarritosDeCompra");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.Factura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CodigoCliente")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFactura")
                        .HasColumnType("datetime2");

                    b.Property<double>("IVA")
                        .HasColumnType("float");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Facturas");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.InformeProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadPedidas")
                        .HasColumnType("int");

                    b.Property<int>("CodigoProducto")
                        .HasColumnType("int");

                    b.Property<double>("GanaciasTotal")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("informesProducto");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.PromocionProducto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("PrecioOferta")
                        .HasColumnType("float");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("PromocionesProducto");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.CarritoDeCompra", b =>
                {
                    b.HasOne("Tienda_Online.Shared.Entidades.Producto", "Producto")
                        .WithMany("CarritosDeCompra")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.PromocionProducto", b =>
                {
                    b.HasOne("Tienda_Online.Shared.Entidades.Producto", "Producto")
                        .WithMany("Promociones")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Tienda_Online.Shared.Entidades.Producto", b =>
                {
                    b.Navigation("CarritosDeCompra");

                    b.Navigation("Promociones");
                });
#pragma warning restore 612, 618
        }
    }
}
