﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SquillerWebshop;
using SquillerWebshop.Models;
using System;

namespace SquillerWebshop.Migrations
{
    [DbContext(typeof(WebshopContext))]
    partial class WebshopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("SquillerWebshop.Models.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Administrators");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adress");

                    b.Property<string>("City");

                    b.Property<string>("Email");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PostalCode");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.HasKey("Id");

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Order", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("ProductsId");

                    b.Property<int>("Amount");

                    b.Property<DateTime>("orderDate");

                    b.Property<int>("orderStatus");

                    b.HasKey("CustomerId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmountId");

                    b.Property<int?>("BrandId");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("DateAdded");

                    b.Property<string>("Description");

                    b.Property<int>("Extra");

                    b.Property<int>("Gender");

                    b.Property<string>("Name");

                    b.Property<double>("Price");

                    b.HasKey("Id");

                    b.HasIndex("AmountId");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SquillerWebshop.Models.ShoppingCart", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Amount");

                    b.HasKey("CustomerId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("SquillerWebshop.Models.Order", b =>
                {
                    b.HasOne("SquillerWebshop.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SquillerWebshop.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SquillerWebshop.Models.Product", b =>
                {
                    b.HasOne("SquillerWebshop.Models.Inventory", "Amount")
                        .WithMany()
                        .HasForeignKey("AmountId");

                    b.HasOne("SquillerWebshop.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId");

                    b.HasOne("SquillerWebshop.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("SquillerWebshop.Models.ShoppingCart", b =>
                {
                    b.HasOne("SquillerWebshop.Models.Customer", "Customer")
                        .WithMany("Products")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SquillerWebshop.Models.Product", "Product")
                        .WithMany("Customers")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
