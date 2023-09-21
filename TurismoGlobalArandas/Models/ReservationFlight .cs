using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationFlight
    {
        [Key] 
        public int FlightId { get; set; } 
        public string? Invoice { get; set; }
        public DateTime? TravelDateStart { get; set; } = null;
        public DateTime? TravelDateEnd { get; set; } = null;
        public DateTime? DateSale { get; set; } = null;
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public int? CustomerId { get; set; } = null;
        public string? Airline { get; set; }
        public string? ConfirmationKey { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PriceNeto { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PricePublic { get; set; }
        public bool isSimpleFlight { get; set; }
        public bool isRoundFlight { get; set; }
        public bool isMultidestinationFlight { get; set; }
        public string? PaymentMethodAgency { get; set; }
        public string? PaymentMethodClient { get; set; }
        public string? ContactPhone { get; set; }
        public int? StatusFlightId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("CustomerId")]
        public Customers? Customers { get; set; }

        [ForeignKey("StatusFlightId")]
        public StatusFlight? StatusFlight { get; set; }
    }
}
