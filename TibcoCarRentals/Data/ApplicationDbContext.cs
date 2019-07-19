using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TibcoCarRentals.Models;

namespace TibcoCarRentals.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TibcoCarRentals.Models.Car> Car { get; set; }
        public DbSet<TibcoCarRentals.Models.CarBooking> CarBooking { get; set; }
        public DbSet<TibcoCarRentals.Models.Bus> Bus { get; set; }
        public DbSet<TibcoCarRentals.Models.Contact> Contact { get; set; }
        public DbSet<TibcoCarRentals.Models.BusBooking> BusBooking { get; set; }
    }
}
