using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class HabitationsReservation
    {
        [Key]
        public int HabitationsReservationId { get; set; }
        public int HabitationId { get; set; }
        public int ReservationHotelId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("HabitationId")]
        public Habitations? Habitations { get; set; }
        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
    }
}
