using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentsRelationReservations
    {
        [Key]
        public int PaymentReservationId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountTotal { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountMissing { get; set; }
        public int? ReservationHotelId { get; set; }
        public int? ReservationTourId { get; set; }
        public int? ReservationVehicleId { get; set; }
        public int? StatusPaymentRelationId { get; set; }
        public bool IsDeleted { get; set; }


        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? ReservationHotels { get; set; }
        [ForeignKey("ReservationTourId")]
        public ReservationTours? ReservationTours { get; set; }
        [ForeignKey("ReservationVehicleId")]
        public ReservationVehicle? ReservationVehicles { get; set; }
        [ForeignKey("StatusPaymentRelationId")]
        public StatusPaymentRelations? StatusPaymentRelations { get; set; }

    }
}
