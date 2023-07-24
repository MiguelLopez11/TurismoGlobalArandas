using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Reservations
    {
        [Key]
        public int ReservationId { get; set; }
        [Required]
        public string reservationInvoice {get;set;}
        public int CustomerId { get;set; }
        public string Hotel { get; set; }
        public string Destination { get; set; }
        public DateTime TravelDate { get; set; }
        public string Observation { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
    }
}
