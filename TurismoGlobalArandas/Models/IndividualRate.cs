using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class IndividualRate
    {
        [Key]
        public int IndividualRateId { get; set; }
        public int ReservationHotelId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public double PublicRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public double ClientRate { get; set; }
        public int ExtraDiscount { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
    }
}
