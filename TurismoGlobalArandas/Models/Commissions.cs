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
        public decimal? CommissionAgency { get; set; }
        public decimal? CommissionClient { get; set; }
        public decimal? CommissionEmployee { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
