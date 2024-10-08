﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stx.Database;

#nullable disable

namespace Stx.Migrations
{
    [DbContext(typeof(ProductContext))]
    partial class ProductContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Stx.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Active")
                        .HasColumnType("boolean")
                        .HasColumnName("active");

                    b.Property<string>("Code")
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("code");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<int?>("Rating")
                        .HasColumnType("integer")
                        .HasColumnName("rating");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("releasedate");

                    b.HasKey("Id")
                        .HasName("pk_product");

                    b.ToTable("product", (string)null);
                });

            modelBuilder.Entity("Stx.Models.ProductPriceHistory", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<double?>("Price")
                        .HasColumnType("double precision")
                        .HasColumnName("price");

                    b.Property<double?>("PriceChange")
                        .HasColumnType("double precision")
                        .HasColumnName("pricechange");

                    b.Property<int?>("PriceChangeInt")
                        .HasColumnType("integer")
                        .HasColumnName("pricechangeint");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("productid");

                    b.Property<DateTime>("ValidFrom")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("validfrom");

                    b.HasKey("Id")
                        .HasName("pk_productpricehistory");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("ix_productpricehistory_productid");

                    b.ToTable("productpricehistory", (string)null);
                });

            modelBuilder.Entity("Stx.Models.ProductPriceHistory", b =>
                {
                    b.HasOne("Stx.Models.Product", "Product")
                        .WithMany("PriceHistory")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_productpricehistory_product_productid");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Stx.Models.Product", b =>
                {
                    b.Navigation("PriceHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
