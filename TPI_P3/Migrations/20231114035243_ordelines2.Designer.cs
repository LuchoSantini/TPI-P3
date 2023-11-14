﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TPI_P3.Data;

#nullable disable

namespace TPI_P3.Migrations
{
    [DbContext(typeof(TPIContext))]
    [Migration("20231114035243_ordelines2")]
    partial class ordelines2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.13");

            modelBuilder.Entity("ColourProduct", b =>
                {
                    b.Property<int>("ColoursId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ColoursId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ColoursProducts", (string)null);
                });

            modelBuilder.Entity("Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ProductSize", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SizesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "SizesId");

                    b.HasIndex("SizesId");

                    b.ToTable("SizesProducts", (string)null);
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.Colour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ColourName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Colours");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ColourName = "Azul"
                        },
                        new
                        {
                            Id = 2,
                            ColourName = "Rojo"
                        });
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Amount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sizes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            SizeName = "L"
                        },
                        new
                        {
                            Id = 2,
                            SizeName = "XL"
                        });
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Name = "Seba",
                            Password = "123456",
                            Status = true,
                            UserName = "SebaR",
                            UserType = "Admin"
                        });
                });

            modelBuilder.Entity("ColourProduct", b =>
                {
                    b.HasOne("TPI_P3.Data.Entities.Colour", null)
                        .WithMany()
                        .HasForeignKey("ColoursId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProductSize", b =>
                {
                    b.HasOne("Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TPI_P3.Data.Entities.Size", null)
                        .WithMany()
                        .HasForeignKey("SizesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.Order", b =>
                {
                    b.HasOne("TPI_P3.Data.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.OrderLine", b =>
                {
                    b.HasOne("TPI_P3.Data.Entities.Order", null)
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.Order", b =>
                {
                    b.Navigation("OrderLines");
                });

            modelBuilder.Entity("TPI_P3.Data.Entities.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}