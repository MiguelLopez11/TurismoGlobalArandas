using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationVehicle
    {
        [Key]
        public int ReservationVehicleId { get; set; }
        public string? Invoice { get; set; }
        public DateTime? DateSale { get; set; }
        public DateTime? DateTravel { get; set; }
        public string? DepartureLocation { get; set; }
        public string? ArrivalLocation { get; set; }
        public int? ProviderId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PricePublic { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PriceNeto { get; set; }
        public string? Description { get; set; }
        public bool IsSoldOut { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
