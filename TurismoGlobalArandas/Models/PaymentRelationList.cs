using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentRelationList
    {
        [Key]
        public int PaymentId { get; set; }
        public string Invoice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Observations { get; set; }
        public int PaymentReservationHotelId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PaymentReservationHotelId")]
        public PaymentsRelationReservationHotels? PaymentsReservationHotels { get; set; }
    }
}
