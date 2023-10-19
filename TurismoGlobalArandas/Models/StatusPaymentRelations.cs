using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class StatusPaymentRelations
    {
        [Key]
        public int StatusId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
