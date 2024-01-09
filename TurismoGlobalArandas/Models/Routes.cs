using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Routes
    {
        [Key]
        public int RouteId { get; set; }
        public string RouteName { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

    }
}
