﻿// <auto-generated />
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
    [Migration("20250405235246_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

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
#pragma warning restore 612, 618
        }
    }
}
