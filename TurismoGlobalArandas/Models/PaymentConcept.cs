using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class PaymentConcept
    {
        [Key]
        public int PaymentConceptId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
