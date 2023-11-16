using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationHotelGroup
    {
        [Key]
        public int ReservationHotelGroupId { get; set; }
        public string? GroupName { get; set; }
        public string? ConfirmationKey { get; set; }
        public DateTime? DateArrival { get; set; }
        public string? Coordinator { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RangePublicClient { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RangeJunior { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? RangeMinor { get; set; }
        public int? NightsNumber { get; set; }
        
        public int? ReservationHotelId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ReservationHotelId")]
        public ReservationHotel? reservationHotel { get; set; }
    }
}
