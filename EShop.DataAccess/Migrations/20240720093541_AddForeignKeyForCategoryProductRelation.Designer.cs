﻿// <auto-generated />
using EShop.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EShop.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240720093541_AddForeignKeyForCategoryProductRelation")]
    partial class AddForeignKeyForCategoryProductRelation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EShop.Models.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Designers"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Clothing"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Shoes"
                        });
                });

            modelBuilder.Entity("EShop.Models.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "A classic denim jacket in excellent condition.",
                            ListPrice = 15000m,
                            Size = "M",
                            Title = "Vintage Denim Jacket"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 3,
                            Description = "Sturdy leather boots with minimal wear.",
                            ListPrice = 20000m,
                            Size = "10",
                            Title = "Leather Boots"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Light and airy dress perfect for summer.",
                            ListPrice = 12000m,
                            Size = "L",
                            Title = "Summer Dress"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            Description = "Cozy wool sweater to keep you warm.",
                            ListPrice = 18000m,
                            Size = "S",
                            Title = "Wool Sweater"
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            Description = "Elegant shirt for formal occasions.",
                            ListPrice = 10000m,
                            Size = "M",
                            Title = "Formal Shirt"
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 1,
                            Description = "Comfortable jacket for sports and outdoor activities.",
                            ListPrice = 25000m,
                            Size = "L",
                            Title = "Sports Jacket"
                        });
                });

            modelBuilder.Entity("EShop.Models.Models.Product", b =>
                {
                    b.HasOne("EShop.Models.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
