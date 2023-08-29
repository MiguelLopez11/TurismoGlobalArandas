using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class GroupRate
    {
        [Key]
        public int GroupRateId { get; set; }
        public int ReservationHotelId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? IndividualHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? DoubleHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TripleHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? QDPLHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? JuniorHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinorHabitation { get; set; }
        public int? AgesMinors { get; set; }
        public int? AgesJuniors { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
    }
}
