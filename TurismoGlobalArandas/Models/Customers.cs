using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Customers
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string? Nationality { get; set; }
        public DateTime? DateBirth { get; set; }
        public bool IsDeleted { get; set; }


    }
}
