using Booking.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Context
{
    public class ManageBookingContext: DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public ManageBookingContext(DbContextOptions<ManageBookingContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ManageBookingContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
