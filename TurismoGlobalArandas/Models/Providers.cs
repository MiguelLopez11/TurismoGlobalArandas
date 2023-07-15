using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Providers
    {
        [Key]
        public int ProviderId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool isDeleted { get; set; }
    }
}
