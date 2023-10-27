using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TurismoGlobalArandas.Migrations;

namespace TurismoGlobalArandas.Models
{
    public class ReservationHotel
    {
        [Key]
        public int ReservationHotelId { get; set; }
        public string? ReservationInvoice { get; set; } = null;
        public DateTime? TravelDateStart { get; set; } = null;
        public DateTime? TravelDateEnd { get; set; } = null;
        public string? TypeHabitation { get; set; } = null;
        public int? NumberHabitations { get; set; } = null;
        public string? PhoneContact { get; set; } = null;
        public string? Observations { get; set; } = null;
        public string? GroupCoordinator { get; set; } = null;
        public string? PhoneCoordinator { get; set; } = null;
        public DateTime? DateSale { get; set; } = null;
        public string? Agent { get; set; } = null;
        public string? PaymentPeriod { get; set; } = null;
        public string? CancellationPolicy { get; set; } = null;
        public string? CodeVoicher { get; set; } = null;

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCost { get; set; } = null;
        public DateTime? PaymentLimitDate { get; set; } = null;
        public DateTime? PaymentLimitDateProvider { get; set; } = null;
        public int? TypeReservationId { get; set; } = null;
        public int? TypeReservationGroupId { get; set; } = null;
        public int? EmployeeId { get; set; } = null;
        public int? CustomerId { get; set; } = null;
        public int? HotelId { get; set; } = null;
        public int? DestinationId { get; set; } = null;
        public int? ProviderId { get; set; } = null;
        public int? ReservationFlightId { get; set; } = null;
        public int? ReservationVehicleId { get; set; } = null;
        public bool IsDeleted { get; set; }

        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }

        [ForeignKey("HotelId")]
        public Hotels? Hotels { get; set; }

        [ForeignKey("DestinationId")]
        public Destinations? Destinations { get; set; }

        [ForeignKey("TypeReservationId")]
        public TypeReservation? TypeReservation { get; set; }

        [ForeignKey("TypeReservationGroupId")]
        public TypeReservationGrupal? TypeReservationGrupal { get; set; }

        [ForeignKey("ReservationFlightId")]
        public ReservationFlight? ReservationFlight { get; set; }

        [ForeignKey("ReservationVehicleId")]
        public ReservationVehicle? ReservationVehicle { get; set; }


        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
