using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class TypeReservation
    {
        [Key]
        public int TypeReservationId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
