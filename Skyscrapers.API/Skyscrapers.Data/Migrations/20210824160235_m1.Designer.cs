﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Skyscrapers.Data;

namespace Skyscrapers.Data.Migrations
{
    [DbContext(typeof(SkyscrapersDbContext))]
    [Migration("20210824160235_m1")]
    partial class m1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Skyscrapers.Data.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CountryId = 1,
                            Name = "New York City"
                        },
                        new
                        {
                            Id = 2,
                            CountryId = 1,
                            Name = "Chicago"
                        });
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "United States"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Canada"
                        });
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.Skyscraper", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Built")
                        .HasColumnType("int");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("NrOfFloors")
                        .HasColumnType("int");

                    b.Property<int>("OfficialHeightInMeters")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Skyscrapers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Built = 1870,
                            CityId = 1,
                            NrOfFloors = 8,
                            OfficialHeightInMeters = 43,
                            Status = "destroyed",
                            Title = "Equitable Life Building"
                        },
                        new
                        {
                            Id = 2,
                            Built = 1889,
                            CityId = 2,
                            NrOfFloors = 17,
                            OfficialHeightInMeters = 82,
                            Status = "standing",
                            Title = "Auditorium Building"
                        },
                        new
                        {
                            Id = 3,
                            Built = 1890,
                            CityId = 1,
                            NrOfFloors = 20,
                            OfficialHeightInMeters = 94,
                            Status = "demolished",
                            Title = "New York World Building"
                        });
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.City", b =>
                {
                    b.HasOne("Skyscrapers.Data.Models.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.Skyscraper", b =>
                {
                    b.HasOne("Skyscrapers.Data.Models.City", "City")
                        .WithMany("Skyscrapers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.City", b =>
                {
                    b.Navigation("Skyscrapers");
                });

            modelBuilder.Entity("Skyscrapers.Data.Models.Country", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}