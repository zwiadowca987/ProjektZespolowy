﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjektZespolowy.Data;

#nullable disable

namespace ProjektZespolowy.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OrderDetails", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Costs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Period")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.ToTable("Costs");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Employee", b =>
                {
                    b.Property<int>("PracownikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PracownikID"));

                    b.Property<string>("AdresEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdresZamieszkania")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stanowisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PracownikID");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.OrderWarehouse", b =>
                {
                    b.Property<int>("ZamMag")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZamMag"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int>("DoZamowienia")
                        .HasColumnType("int");

                    b.Property<int>("DostawcaID")
                        .HasColumnType("int");

                    b.Property<string>("NazwaProduktu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProduktId")
                        .HasColumnType("int");

                    b.Property<string>("StatusDostawy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uwagi")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<decimal>("WartoscZamowienia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ZamMag");

                    b.HasIndex("DostawcaID");

                    b.HasIndex("ProduktId");

                    b.ToTable("OrderWarehouses");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Product", b =>
                {
                    b.Property<int>("ProduktId")
                        .HasColumnType("int");

                    b.Property<decimal>("Cena")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("NazwaProduktu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Opis")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Producent")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProduktId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Raport", b =>
                {
                    b.Property<int>("RaportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RaportId"));

                    b.Property<DateTime>("DataWygenerowania")
                        .HasColumnType("datetime2");

                    b.Property<string>("Typ")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Uwagi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RaportId");

                    b.ToTable("Raports");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Supplier", b =>
                {
                    b.Property<int>("DostawcaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DostawcaID"));

                    b.Property<string>("Adres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Kontakt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NazwaDostawcy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DostawcaID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Warehouse", b =>
                {
                    b.Property<int>("ProduktId")
                        .HasColumnType("int");

                    b.Property<int>("DostepnaIlosc")
                        .HasColumnType("int");

                    b.Property<string>("NazwaProduktu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Pojemnosc")
                        .HasColumnType("int");

                    b.HasKey("ProduktId");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("RaportItem", b =>
                {
                    b.Property<int>("RaportItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RaportItemId"));

                    b.Property<int>("Ilosc")
                        .HasColumnType("int");

                    b.Property<string>("NazwaProduktu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProduktId")
                        .HasColumnType("int");

                    b.Property<int>("RaportId")
                        .HasColumnType("int");

                    b.HasKey("RaportItemId");

                    b.HasIndex("RaportId");

                    b.ToTable("RaportItems");
                });

            modelBuilder.Entity("OrderDetails", b =>
                {
                    b.HasOne("ProjektZespolowy.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjektZespolowy.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Order", b =>
                {
                    b.HasOne("ProjektZespolowy.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.OrderWarehouse", b =>
                {
                    b.HasOne("ProjektZespolowy.Models.Supplier", "Supplier")
                        .WithMany("OrderWarehouses")
                        .HasForeignKey("DostawcaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjektZespolowy.Models.Warehouse", "Warehouse")
                        .WithMany("OrderWarehouses")
                        .HasForeignKey("ProduktId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Supplier");

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Warehouse", b =>
                {
                    b.HasOne("ProjektZespolowy.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProduktId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("RaportItem", b =>
                {
                    b.HasOne("ProjektZespolowy.Models.Raport", "Raport")
                        .WithMany("RaportItems")
                        .HasForeignKey("RaportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Raport");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Raport", b =>
                {
                    b.Navigation("RaportItems");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Supplier", b =>
                {
                    b.Navigation("OrderWarehouses");
                });

            modelBuilder.Entity("ProjektZespolowy.Models.Warehouse", b =>
                {
                    b.Navigation("OrderWarehouses");
                });
#pragma warning restore 612, 618
        }
    }
}
