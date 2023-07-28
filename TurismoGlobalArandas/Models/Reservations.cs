using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public string TypeReservationId {get;set;}
        [Required]
        public string ReservationInvoice {get;set;}
        public int CustomerId { get;set; }
        public int HotelId { get; set; }
        public DateTime TravelDate { get; set; }
        public string TypeHabitation { get; set; }
        public string Observations { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        [ForeignKey("HotelId")]
        public Hotels? Hotels { get; set; }
        [ForeignKey("TypeReservationId")]
        public TypeReservation? TypeReservation { get; set; }
    }
}
