using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Core.Entities;

namespace Ticketing_.Infrastructure.DBContext
{
    public class TicketingDBContext : DbContext
    {

        public DbSet<EventTicketInventory> ticketingInventory { get; set; }

        public DbSet<Booking> booking { get; set; }

        public DbSet<BookingTicketTier> bookingTicketTiers { get; set; }
        public TicketingDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
            .HasCheckConstraint("CK_Booking_Status_Valid",
             "[Status] IN ('Confirmed', 'Reserved', 'Failed')");
        }             
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=TicketingDBContext;User Id=sa;Password=Arora1234!;TrustServerCertificate=True;");
        //}

    }
}
