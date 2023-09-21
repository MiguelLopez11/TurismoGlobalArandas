using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class StatusFlight
    {
        [Key]
        public int StatusFlightId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
    }
}
