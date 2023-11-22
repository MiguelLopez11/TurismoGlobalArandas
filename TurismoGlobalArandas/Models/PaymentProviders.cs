using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentProviders
    {
        [Key]
        public int PaymentId { get; set; }
        public string Invoice { get; set; }
        public DateTime PaymentDate { get; set; }
        public int ReservationHotelId { get; set; }
        public int ReservationTourId { get; set;}
        public int ReservationFlightId { get; set; }
        public int ReservationVehicleId { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? ReservationHotels { get; set; }
        [ForeignKey("ReservationTourId")]
        public ReservationTours? ReservationTours { get; set; }
        [ForeignKey("ReservationVehicleId")]
        public ReservationVehicle? ReservationVehicles { get; set; }
        [ForeignKey("ReservationFlightId")]
        public ReservationFlight? reservationFlight { get; set; }
    }
}
