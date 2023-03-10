﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UniSample.Library.Service.DataAccess;

#nullable disable

namespace UniSample.Library.Service.Migrations
{
    [DbContext(typeof(LibraryDbContext))]
    partial class LibraryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("UniSample.Library.Service.Model.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Available")
                        .HasColumnType("boolean");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("6eee2d39-041a-4ffd-9a27-d175c2a35794"),
                            Author = "Maarten van Steen, Andrew S. Tanenbaum",
                            Available = true,
                            ISBN = "978-1543057386",
                            Title = "Distributed Systems"
                        },
                        new
                        {
                            Id = new Guid("cb37c704-9ac3-4436-b609-648b711d5938"),
                            Author = "Günther Bengel",
                            Available = true,
                            ISBN = "978-3834816702",
                            Title = "Grundkurs Verteilte Systeme"
                        });
                });

            modelBuilder.Entity("UniSample.Library.Service.Model.Lending", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LibraryUserId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ReturnTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("LibraryUserId");

                    b.ToTable("Lendings");
                });

            modelBuilder.Entity("UniSample.Library.Service.Model.LibraryUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("StudentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("LibraryUsers");
                });

            modelBuilder.Entity("UniSample.Library.Service.Model.Lending", b =>
                {
                    b.HasOne("UniSample.Library.Service.Model.Book", "Book")
                        .WithMany("Lendings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UniSample.Library.Service.Model.LibraryUser", "LibraryUser")
                        .WithMany("Lendings")
                        .HasForeignKey("LibraryUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("LibraryUser");
                });

            modelBuilder.Entity("UniSample.Library.Service.Model.Book", b =>
                {
                    b.Navigation("Lendings");
                });

            modelBuilder.Entity("UniSample.Library.Service.Model.LibraryUser", b =>
                {
                    b.Navigation("Lendings");
                });
#pragma warning restore 612, 618
        }
    }
}
