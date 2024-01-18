using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentProviders
    {
        [Key]
        public int PaymentProviderId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountMissing { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now;
        public int? ReservationHotelId { get; set; }
        public int? ReservationTourId { get; set; }
        public int? ReservationFlightId { get; set; }
        public int? ReservationVehicleId { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PaymentConceptId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? ReservationHotels { get; set; }
        [ForeignKey("ReservationTourId")]
        public ReservationTours? ReservationTours { get; set; }
        [ForeignKey("ReservationVehicleId")]
        public ReservationVehicle? ReservationVehicles { get; set; }
        [ForeignKey("ReservationFlightId")]
        public ReservationFlight? reservationFlight { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethods? PaymentMethod { get; set; }
        [ForeignKey("PaymentConceptId")]
        public PaymentConcept? PaymentConcept { get; set; }
    }
}
