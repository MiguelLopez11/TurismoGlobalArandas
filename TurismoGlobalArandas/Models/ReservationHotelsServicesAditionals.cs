using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationHotelsServicesAditionals
    {
        [Key]
        public int Id { get; set; }
        public int ReservationHotelId { get; set; } 
        public int AditionalServiceId { get; set; }
        public int? ReservationFlightId { get; set; } = null;
        public int? ReservationVehicleId { get; set; } = null;
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
        [ForeignKey("AditionalServiceId")]
        public AditionalServices? AditionalServices { get; set; }
        [ForeignKey("ReservationFlightId")]
        public ReservationFlight? ReservationFlight { get; set; }

        [ForeignKey("ReservationVehicleId")]
        public ReservationVehicle? ReservationVehicle { get; set; }
    }
}
