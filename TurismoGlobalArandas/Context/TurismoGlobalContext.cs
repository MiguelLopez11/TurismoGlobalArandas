using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Models;
using UConnect.Entities;

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
        public DbSet<Reservations> Reservations { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<TypeReservation> TypeReservations { get; set; }
        public DbSet<CategoryReservations> CategoryReservations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

}
