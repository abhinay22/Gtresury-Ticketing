﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventTicketing.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventTicketing.Infrastructure.DBContext
{
    public class EventDBContext : DbContext
    {
        public EventDBContext(DbContextOptions options) : base(options)
        {

        }
        public  DbSet<Event> Events { get; set; }

        public  DbSet<Venue> Venues { get; set; }

        public  DbSet<PricingTier> PricingTier { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=EventDBContext;User Id=sa;Password=Arora1234!;TrustServerCertificate=True;");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
