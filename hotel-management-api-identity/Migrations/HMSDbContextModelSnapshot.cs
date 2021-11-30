﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using hotel_management_api_identity.Core.Storage;

#nullable disable

namespace hotel_management_api_identity.Migrations
{
    [DbContext(typeof(HMSDbContext))]
    partial class HMSDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AmountPaid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTimeOffset>("CheckOutDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("HasDiscount")
                        .HasColumnType("bit");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RoomId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AmountPaid");

                    b.HasIndex("CheckOutDate");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("CustomerId");

                    b.HasIndex("HasDiscount");

                    b.HasIndex("Id");

                    b.HasIndex("RoomId");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("Email");

                    b.HasIndex("Id");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<int>("IdType")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email");

                    b.HasIndex("Id");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Login", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("Id");

                    b.HasIndex("Password");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Category");

                    b.HasIndex("Id");

                    b.HasIndex("Item");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Room", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.HasIndex("Name");

                    b.HasIndex("Price");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Sales", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<string>("CreatedById")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("CreatedOn")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Item")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedById")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("CreatedOn");

                    b.HasIndex("Id");

                    b.HasIndex("Price");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Booking", b =>
                {
                    b.HasOne("hotel_management_api_identity.Core.Storage.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("hotel_management_api_identity.Core.Storage.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId");

                    b.Navigation("Customer");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("hotel_management_api_identity.Core.Storage.Models.Login", b =>
                {
                    b.HasOne("hotel_management_api_identity.Core.Storage.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");

                    b.Navigation("Employee");
                });
#pragma warning restore 612, 618
        }
    }
}
