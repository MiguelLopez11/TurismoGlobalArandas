using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ReservationFlightDestinations
    {
        [Key]
        public int ReservationFlightDestinationId { get; set; }
        public int? RouteDepartureAirportId { get; set; }
        public int? RouteArrivalAirportId { get; set; }
        public int? AirlineId { get; set; }
        public int? ReservationFlightId { get; set; }
        public DateTime? DateTravel { get; set; } = null;
        [ForeignKey("RouteDepartureAirportId")]
        public Routes? DepartureAirport { get; set; }

        [ForeignKey("RouteArrivalAirportId")]
        public Routes? ArrivalAirport { get; set; }

        [ForeignKey("AirlineId")]
        public Airline? Airline { get; set; }

        [ForeignKey("ReservationFlightId")]
        public ReservationFlight? ReservationFlight { get; set; }
    }
}
