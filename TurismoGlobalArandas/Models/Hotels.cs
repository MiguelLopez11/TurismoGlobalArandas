using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Hotels
    {
        [Key]
        public int HotelId { get; set; }
        public string Name { get; set; }
        public int DestinationId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

    }
}
