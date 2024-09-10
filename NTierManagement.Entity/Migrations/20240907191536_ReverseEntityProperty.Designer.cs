﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NTierManagement.Entity.Context;

#nullable disable

namespace NTierManagement.Entity.Migrations
{
    [DbContext(typeof(ManagementContext))]
    [Migration("20240907191536_ReverseEntityProperty")]
    partial class ReverseEntityProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("NTierManagement.Entity.Models.Company", b =>
                {
                    b.Property<int>("CompanyID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompanyID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("CeoID")
                        .HasColumnType("int");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CompanyID");

                    b.HasIndex("CeoID");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Department", b =>
                {
                    b.Property<int>("DepartmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DepartmentID"));

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("CompanyID")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("LeaderID")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("DepartmentID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("LeaderID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Person", b =>
                {
                    b.Property<int>("PersonID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonID"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyID")
                        .HasColumnType("int");

                    b.Property<int?>("DepartmentID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("PersonID");

                    b.HasIndex("CompanyID");

                    b.HasIndex("DepartmentID");

                    b.ToTable("People");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Company", b =>
                {
                    b.HasOne("NTierManagement.Entity.Models.Person", "Ceo")
                        .WithMany()
                        .HasForeignKey("CeoID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ceo");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Department", b =>
                {
                    b.HasOne("NTierManagement.Entity.Models.Company", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("CompanyID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("NTierManagement.Entity.Models.Person", "Leader")
                        .WithMany()
                        .HasForeignKey("LeaderID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Leader");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Person", b =>
                {
                    b.HasOne("NTierManagement.Entity.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyID");

                    b.HasOne("NTierManagement.Entity.Models.Department", "Department")
                        .WithMany("People")
                        .HasForeignKey("DepartmentID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Company");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Company", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("NTierManagement.Entity.Models.Department", b =>
                {
                    b.Navigation("People");
                });
#pragma warning restore 612, 618
        }
    }
}
