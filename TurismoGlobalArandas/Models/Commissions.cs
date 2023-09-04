using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Commissions
    {
        [Key]
        public int CommissionId { get; set; }
        public int ProviderId { get; set; }
        public string? Color { get; set; }
        public double? CommissionAgency { get; set; }
        public double? CommissionClient { get; set; }
        public double? CommissionEmployee { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
