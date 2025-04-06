﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Ticketing_.Infrastructure.DBContext;

#nullable disable

namespace Ticketing_.Infrastructure.Migrations
{
    [DbContext(typeof(TicketingDBContext))]
    [Migration("20250406163641_bookings-uppated")]
    partial class bookingsuppated
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ticketing.Core.Entities.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"));

                    b.Property<DateTime>("FinalizationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReservedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("eventId")
                        .HasColumnType("int");

                    b.Property<string>("status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("transactionRef")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("userEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookingId");

                    b.ToTable("booking", t =>
                        {
                            t.HasCheckConstraint("CK_Booking_Status_Valid", "[Status] IN ('Confirmed', 'Reserved', 'Failed')");
                        });
                });

            modelBuilder.Entity("Ticketing.Core.Entities.BookingTicketTier", b =>
                {
                    b.Property<int>("BookingTicketTierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingTicketTierId"));

                    b.Property<int>("BookingId")
                        .HasColumnType("int");

                    b.Property<string>("ReservedQuantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TierName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BookingTicketTierId");

                    b.HasIndex("BookingId");

                    b.ToTable("bookingTicketTiers");
                });

            modelBuilder.Entity("Ticketing.Core.Entities.EventTicketInventory", b =>
                {
                    b.Property<int>("EventTicketInventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EventTicketInventoryId"));

                    b.Property<int>("available")
                        .HasColumnType("int");

                    b.Property<string>("eventDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("eventId")
                        .HasColumnType("int");

                    b.Property<string>("eventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("pricePerTicket")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ticketTier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("total")
                        .HasColumnType("int");

                    b.HasKey("EventTicketInventoryId");

                    b.ToTable("ticketingInventory");
                });

            modelBuilder.Entity("Ticketing.Core.Entities.BookingTicketTier", b =>
                {
                    b.HasOne("Ticketing.Core.Entities.Booking", null)
                        .WithMany("reservationData")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ticketing.Core.Entities.Booking", b =>
                {
                    b.Navigation("reservationData");
                });
#pragma warning restore 612, 618
        }
    }
}
