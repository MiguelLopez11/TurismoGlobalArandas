using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Itinerary
    {
        [Key]
        public int itineraryId { get; set; }
        public string TableName { get; set; }
        public DateTime CurrentDate { get; set; }
        public string User { get; set; }
        public string Operation { get; set; }
        public string Details { get; set; }
    }
}
