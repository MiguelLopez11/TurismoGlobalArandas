using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class ServicesProvider
    {
        [Key]
        public int ServiceId { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; }
        public string? Color { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CommissionAgency { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CommissionClient { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CommissionEmployee { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountExtra { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
