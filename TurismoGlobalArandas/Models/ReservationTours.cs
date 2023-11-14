using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationTours
    {
        [Key]
        public int ReservationTourId { get; set; }
        public string? TourName { get; set; }
        public int? DestinationId { get; set; }
        public DateTime? DateSale { get; set; }
        public DateTime? DateActivity { get; set; }
        public string? OwnerName { get; set; }
        public int? NumberAdults { get; set; }
        public int? NumberMinors { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AdultCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinorCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PublicRate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NetPrice { get; set; }
        public string? ExchangeRate { get; set; }
        public bool IsInternational { get; set; }
        public bool IsNational { get; set; }
        public bool IncludeTransportation { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("DestinationId")]
        public Destinations? Destinations { get; set; }

    }
}
