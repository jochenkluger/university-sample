﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniSample.Courses.Service.DataAccess;

#nullable disable

namespace UniSample.Courses.Service.Migrations
{
    [DbContext(typeof(CourseDbContext))]
    partial class CourseDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniSample.Courses.Service.Model.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ProfName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StudentsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("856ee11c-7a17-4247-9864-46a3d24a9d23"),
                            Name = "Verteilte Systeme",
                            ProfName = "Jochen Kluger",
                            StudentsCount = 0
                        },
                        new
                        {
                            Id = new Guid("100ee899-cd2f-4120-a7c0-d6df9341908e"),
                            Name = "Cloud Native",
                            ProfName = "Jochen Kluger",
                            StudentsCount = 0
                        });
                });

            modelBuilder.Entity("UniSample.Courses.Service.Model.CourseStudent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CourseId")
                        .HasColumnType("uuid");

                    b.Property<int?>("Grade")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("CourseStudents");
                });

            modelBuilder.Entity("UniSample.Courses.Service.Model.CourseStudent", b =>
                {
                    b.HasOne("UniSample.Courses.Service.Model.Course", null)
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UniSample.Courses.Service.Model.Course", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}