using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Destinations
    {
        [Key]
        public int DestinationId { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

    }
}
