﻿// <auto-generated />
using System;
using EventTicketing.Infrastructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventTicketing.Infrastructure.Migrations
{
    [DbContext(typeof(EventDBContext))]
    [Migration("20250406012028_newChange")]
    partial class newChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EventTicketing.Core.Entities.Event", b =>
                {
                    b.Property<int>("EventId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventId"));

                    b.Property<int>("VenueId")
                        .HasColumnType("int");

                    b.Property<DateTime>("endDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("eventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isÀctive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("startDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("totalQuantity")
                        .HasColumnType("int");

                    b.HasKey("EventId");

                    b.HasIndex("VenueId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("EventTicketing.Core.Entities.PricingTier", b =>
                {
                    b.Property<int>("PricingTierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PricingTierId"));

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("tierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("totalTicket")
                        .HasColumnType("int");

                    b.HasKey("PricingTierId");

                    b.HasIndex("EventId");

                    b.ToTable("PricingTier");
                });

            modelBuilder.Entity("EventTicketing.Core.Entities.Venue", b =>
                {
                    b.Property<int>("VenueId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VenueId"));

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("zipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VenueId");

                    b.ToTable("Venues");
                });

            modelBuilder.Entity("EventTicketing.Core.Entities.Event", b =>
                {
                    b.HasOne("EventTicketing.Core.Entities.Venue", "venue")
                        .WithMany()
                        .HasForeignKey("VenueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("venue");
                });

            modelBuilder.Entity("EventTicketing.Core.Entities.PricingTier", b =>
                {
                    b.HasOne("EventTicketing.Core.Entities.Event", null)
                        .WithMany("pricingTier")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("EventTicketing.Core.Entities.Event", b =>
                {
                    b.Navigation("pricingTier");
                });
#pragma warning restore 612, 618
        }
    }
}
