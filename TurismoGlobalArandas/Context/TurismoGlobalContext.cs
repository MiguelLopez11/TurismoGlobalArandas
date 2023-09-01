using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Models;
using UConnect.Entities;
using System.Reflection.Emit;

namespace TurismoGlobalArandas.Context
{
    public class TurismoGlobalContext : IdentityDbContext<User>
    {
        public TurismoGlobalContext(DbContextOptions<TurismoGlobalContext> options) : base(options)
        {
        }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Habitations> Habitations { get; set; }
        public DbSet<HabitationsReservation> HabitationsReservations { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<Providers> Providers { get; set; }
        public DbSet<ReservationHotel> ReservationHotels { get; set; }
        public DbSet<GroupRate> GroupRates { get; set; }
        public DbSet<IndividualRate> IndividualRates { get; set; }
        public DbSet<TypeReservation> TypeReservations { get; set; }
        public DbSet<DiscountsProvider> DiscountProviders { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 1, Name = "Individual", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 2, Name = "Grupal", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 3, Name = "Grupo", Description = null, IsDeleted = false });

        }
    }

}
