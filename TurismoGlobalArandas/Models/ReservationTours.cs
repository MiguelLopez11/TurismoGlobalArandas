using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationTours
    {
        [Key]
        public int ReservationTourId { get; set; }
        public string? Invoice { get; set; } = null;
        public string? TourName { get; set; } = null;
        public int? CustomerId { get; set; } = null;
        public int? DestinationId { get; set; } = null;
        public DateTime? DateSale { get; set; } = null;
        public DateTime? DateActivity { get; set; } = null;
        public DateTime? PaymentLimitDate { get; set; } = null;
        public DateTime? PaymentLimitDateProvider { get; set; } = null;
        public string? OwnerName { get; set; } = null;
        public int? NumberAdults { get; set; } = null;
        public int? NumberMinors { get; set; } = null;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AdultCost { get; set; } = null;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? MinorCost { get; set; } = null;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PublicRate { get; set; } = null;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NetPrice { get; set; } = null;
        public string? ExchangeRate { get; set; } = null;
        public bool IsInternational { get; set; }
        public bool IsNational { get; set; }
        public bool IncludeTransportation { get; set; }
        public bool IsSoldOut { get; set; }
        public int? EmployeeId { get; set; } = null;
        public int? ProviderId { get; set; } = null;
        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }
        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }

        [ForeignKey("DestinationId")]
        public Destinations? Destinations { get; set; }
        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }

    }
}
