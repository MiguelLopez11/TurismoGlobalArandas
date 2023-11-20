using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TurismoGlobalArandas.Models;
using TurismoGlobalArandas.Models.Identity;
using UConnect.Entities;

namespace TurismoGlobalArandas.Context
{
    public class TurismoGlobalContext : IdentityDbContext<User>
    {
        public TurismoGlobalContext(DbContextOptions<TurismoGlobalContext> options) : base(options)
        {
        }
        public DbSet<ReservationsByEmployeeView> ReservationsByEmployeeView { get; set; }
        public DbSet<AditionalServices> AditionalServices { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Destinations> Destinations { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<ExpensesEventual> ExpensesEventuals { get; set; }
        public DbSet<ExpensesFixed> ExpensesFixeds { get; set; }
        public DbSet<GroupRate> GroupRates { get; set; }
        public DbSet<Habitations> Habitations { get; set; }
        public DbSet<HabitationsReservation> HabitationsReservations { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<IndividualRate> IndividualRates { get; set; }
        public DbSet<Itinerary> Itineraries { get; set; }
        public DbSet<PaymentConcept> PaymentConcepts { get; set; }
        public DbSet<PaymentMethods> PaymentMethods { get; set; }
        public DbSet<PaymentRelationList> PaymentRelationLists { get; set; }
        public DbSet<PaymentsRelationReservations> PaymentsRelationReservations { get; set; }
        public DbSet<Providers> Providers { get; set; }
        public DbSet<ReservationHotel> ReservationHotels { get; set; }
        public DbSet<ReservationFlight> ReservationFlights { get; set; }
        public DbSet<ReservationHotelGroup> ReservationHotelGroups { get; set; }
        public DbSet<ReservationHotelsServicesAditionals> ReservationHotelsServicesAditionals { get; set; }
        public DbSet<ReservationTours> ReservationTours { get; set; }
        public DbSet<ReservationVehicle> ReservationVehicles { get; set; }
        public DbSet<Routes> Routes { get; set; }
        public DbSet<ServicesProvider> ServicesProviders { get; set; }
        public DbSet<StatusFlight> StatusFlight { get; set; }
        public DbSet<StatusPaymentRelations> StatusPaymentRelations { get; set; }
        public DbSet<TypeReservation> TypeReservations { get; set; }
        public DbSet<TypeReservationGrupal> TypeReservationGrupals { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ReservationsByEmployeeView>().HasNoKey().ToView(null);
            builder.Entity<AditionalServices>().HasData(new AditionalServices { AditionalServiceId = 1, Name = "Servicio de reservación de vuelo", Description = null, IsDeleted = false });
            builder.Entity<AditionalServices>().HasData(new AditionalServices { AditionalServiceId = 2, Name = "Servicio de reservación de vehiculo", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 1, Name = "Individual", Description = null, IsDeleted = false });
            builder.Entity<TypeReservation>().HasData(new TypeReservation { TypeReservationId = 2, Name = "Grupal", Description = null, IsDeleted = false });
            builder.Entity<TypeReservationGrupal>().HasData(new TypeReservationGrupal { TypeReservationGrupalId = 1, Name = "Grupo", Description = null, IsDeleted = false });
            builder.Entity<TypeReservationGrupal>().HasData(new TypeReservationGrupal { TypeReservationGrupalId = 2, Name = "Individual", Description = null, IsDeleted = false });
            builder.Entity<StatusPaymentRelations>().HasData(new StatusPaymentRelations { StatusId = 1, Name = "Pendiente", Description = null, IsDeleted = false });
            builder.Entity<StatusPaymentRelations>().HasData(new StatusPaymentRelations { StatusId = 2, Name = "Liquidado", Description = null, IsDeleted = false });
            builder.Entity<Employees>().HasData(new Employees { EmployeeId = 1, Name = "Miguel", Lastname = "López Ortega", WorkStation="Developer", Address=null, PhoneNumber="3911001273", IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 1, Name = "Record", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 2, Name = "Check In", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 3, Name = "Imacop", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 4, Name = "Promoser", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 5, Name = "Megatravel", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 6, Name = "Europamundo", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 7, Name = "Chartes", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 8, Name = "Israel", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<Providers>().HasData(new Providers { ProviderId = 9, Name = "Copper Canyon", Email = null, PhoneNumber = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 1, Name = "Hotelería", ProviderId = 1, Color = null, CommissionAgency = 15, CommissionClient = 10, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 2, Name = "Traslados", ProviderId = 1, Color = null, CommissionAgency = 15, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 3, Name = "Hoteleria", ProviderId = 2, Color = null, CommissionAgency = 15, CommissionClient = 10, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 4, Name = "Renta de autos", ProviderId = 2, Color = null, CommissionAgency = 10, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 5, Name = "Hoteleria", ProviderId = 3, Color = null, CommissionAgency = 18, CommissionClient = 13, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 6, Name = "Circuitos", ProviderId = 3, Color = null, CommissionAgency = 18, CommissionClient = 10, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 7, Name = "Entradas a disney", ProviderId = 3, Color = null, CommissionAgency = 05, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 8, Name = "Parques", ProviderId = 4, Color = null, CommissionAgency = 15, CommissionClient = 08, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 9, Name = "Hoteleria", ProviderId = 4, Color = null, CommissionAgency = 17, CommissionClient = 12, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 10, Name = "", ProviderId = 5, Color = null, CommissionAgency = 10, CommissionClient = 05, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 11, Name = "", ProviderId = 6, Color = null, CommissionAgency = 10, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 12, Name = "", ProviderId = 7, Color = null, CommissionAgency = 20, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 13, Name = "Traslados", ProviderId = 8, Color = null, CommissionAgency = 05, CommissionClient = 00, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<ServicesProvider>().HasData(new ServicesProvider { ServiceId = 14, Name = "", ProviderId = 9, Color = null, CommissionAgency = 15, CommissionClient = 10, CommissionEmployee = 05, Description = null, IsDeleted = false });
            builder.Entity<StatusFlight>().HasData(new StatusFlight { StatusFlightId = 1, Name = "Activo", Description = null, IsDeleted = false });
            builder.Entity<StatusFlight>().HasData(new StatusFlight { StatusFlightId = 2, Name = "Cancelado", Description = null, IsDeleted = false });
        }

    }
}
