using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class GroupRate
    {
        [Key]
        public int GroupRateId { get; set; }
        public string? NamesCompanions { get; set; }
        public int? Adults { get; set; }
        public int? Juniors { get; set; }
        public int? MinorsCharge { get; set; }
        public int? MinorsWithoutCharge { get; set; }
        public string? AgesMinors { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RangeNight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RangeTotal { get; set; }
        public string? Observations { get; set; }
        public int ReservationHotelGroupId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelGroupId")]
        public ReservationHotelGroup? ReservationHotelGroup { get; set; }
    }
}
