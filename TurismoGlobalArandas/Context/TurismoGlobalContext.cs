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
        public DbSet<Commissions> Commissions { get; set; }
        public DbSet<ReservationFlight> ReservationFlights { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 1, Name = "Individual", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 2, Name = "Grupal", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 3, Name = "Grupo", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 1, Name = "Record Hoteleria", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 1, ProviderId = 1, Color = null, CommissionAgency = "0.15", CommissionClient = "0.10", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 2, Name = "Record Traslados", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 2, ProviderId = 2, Color = null, CommissionAgency = "0.15", CommissionClient = "0.00", CommissionEmployee = "0.15", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 3, Name = "Check In Hoteleria", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 3, ProviderId = 3, Color = null, CommissionAgency = "0.15", CommissionClient = "0.10", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 4, Name = "Check In renta de autos", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 4, ProviderId = 4, Color = null, CommissionAgency = "0.10", CommissionClient = "0.00", CommissionEmployee = "0.10", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 5, Name = "Imacop", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 5, ProviderId = 5, Color = null, CommissionAgency = "0.18", CommissionClient = "0.13", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 6, Name = "Imacop circuitos", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 6, ProviderId = 6, Color = null, CommissionAgency = "0.18", CommissionClient = "0.10", CommissionEmployee = "0.08", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 7, Name = "Imacop traslados", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 7, ProviderId = 7, Color = null, CommissionAgency = "0.10", CommissionClient = "0.00", CommissionEmployee = "0.10", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 8, Name = "Imacop entradas a disney", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 8, ProviderId = 8, Color = null, CommissionAgency = "0.05", CommissionClient = "0.00", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 9, Name = "Promoser parques", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 9, ProviderId = 9, Color = null, CommissionAgency = "0.15", CommissionClient = "0.08", CommissionEmployee = "0.07", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 10, Name = "Promoser hoteleria", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 10, ProviderId = 10, Color = null, CommissionAgency = "0.17", CommissionClient = "0.12", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 11, Name = "Megatravel", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 11, ProviderId = 11, Color = null, CommissionAgency = "0.05", CommissionClient = "0.05", CommissionEmployee =  "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 12, Name = "Europamundo", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 12, ProviderId = 12, Color = null, CommissionAgency = "0.10", CommissionClient = "0.00", CommissionEmployee = "0.10", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 13, Name = "Chartes", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 13, ProviderId = 13, Color = null, CommissionAgency ="0.20", CommissionClient = "0.00", CommissionEmployee ="0.20", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 14, Name = "Israel traslados", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 14, ProviderId = 14, Color = null, CommissionAgency = "0.05", CommissionClient = "0.00", CommissionEmployee = "0.05", Description = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 15, Name = "Copper canyon", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Commissions>().HasData(new Commissions { CommissionId = 15, ProviderId = 15, Color = null, CommissionAgency = "0.15", CommissionClient = "0.10", CommissionEmployee = "0.05", Description = null, IsDeleted = false });

        }
    }

}
