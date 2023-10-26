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
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
        [ForeignKey("AditionalServiceId")]
        public AditionalServices? AditionalServices { get; set; }
    }
}
