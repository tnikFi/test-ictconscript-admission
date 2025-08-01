﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.7");

            modelBuilder.Entity("Domain.Entities.LogbookEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("IsoTime")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Lat")
                        .HasColumnType("REAL");

                    b.Property<double?>("Lon")
                        .HasColumnType("REAL");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("LogbookEntries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "All clear around main gate.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 14, 21, 30, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.150300000000001,
                            Lon = 25.029299999999999,
                            Title = "Night perimeter check"
                        },
                        new
                        {
                            Id = 2,
                            Body = "Changed oil and inspected filters.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 7, 15, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.156100000000002,
                            Lon = 25.024899999999999,
                            Title = "Generator maintenance"
                        },
                        new
                        {
                            Id = 3,
                            Body = "Heavy rainfall expected after 1600 hrs.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 9, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Weather update"
                        },
                        new
                        {
                            Id = 4,
                            Body = "Quad-copter over Vuosaari direction, 200 m AGL.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 12, 3, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.156799999999997,
                            Lon = 25.0472,
                            Title = "Drone sighting"
                        },
                        new
                        {
                            Id = 5,
                            Body = "Ammo and rations accounted for.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 14, 55, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.153399999999998,
                            Lon = 25.0367,
                            Title = "Supply drop received"
                        },
                        new
                        {
                            Id = 6,
                            Body = "Primary HF link down for 12 min — rebooted transceiver.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 16, 22, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Radio outage"
                        },
                        new
                        {
                            Id = 7,
                            Body = "One soldier treated for mild hypothermia.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 18, 5, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.151000000000003,
                            Lon = 25.032399999999999,
                            Title = "Medical check"
                        },
                        new
                        {
                            Id = 8,
                            Body = "Alpha team left for sector Santahamina east shore.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 19, 40, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.1496,
                            Lon = 25.0441,
                            Title = "Patrol departure"
                        },
                        new
                        {
                            Id = 9,
                            Body = "Fox damaged comms cable near OP-2; temporary fix applied.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 15, 20, 18, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Lat = 60.1479,
                            Lon = 25.0198,
                            Title = "Wildlife interference"
                        },
                        new
                        {
                            Id = 10,
                            Body = "Logbook synced over SATCOM. All entries acknowledged.",
                            IsoTime = new DateTimeOffset(new DateTime(2025, 5, 16, 6, 45, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)),
                            Title = "Sync to HQ"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
