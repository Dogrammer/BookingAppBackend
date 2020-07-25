using BookingDomain;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BookingInfrastructure
{
    public class ApplicationDbContext : IdentityDbContext<User,Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> AuthUsers { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentGroup> ApartmentGroups { get; set; }
        public DbSet<ApartmentType> ApartmentTypes { get; set; }
        public DbSet<PricingPeriod> PricingPeriods { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<PricingPeriodDetail> PricingPeriodDetails { get; set; }
        public DbSet<Image> Images { get; set; }


        //public DbSet<UserApartmentGroup> UserApartmentGroups { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.SetDefaultValueForId();
            builder.LoadAllEntityConfigurations();
            builder.RemoveCascadeDelete();
        }

        

        




    }
}
