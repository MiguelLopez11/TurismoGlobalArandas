using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class GroupRate
    {
        [Key]
        public int GroupRateId { get; set; }
        public int Adults { get; set; }
        public int Juniors { get; set; }
        public int MinorsCharge { get; set; }
        public int MinorsWithoutCharge { get; set; }
        public string AgesMinors { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public decimal RangePublicClient { get; set; }
        public decimal RangeJunior { get; set; }
        public decimal RangeMinor { get; set; }
        public int NightsNumber { get; set; }
        public decimal RangeNight { get; set; }
        public decimal RangeTotal { get; set; }
        public string Observations { get; set; } 
        public int ReservationHotelGroupId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelGroupId")]
        public ReservationHotelGroup? ReservationHotelGroup { get; set; }
    }
}
