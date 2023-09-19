using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TurismoGlobalArandas.Models
{
    public class Commissions
    {
        [Key]
        public int CommissionId { get; set; }
        public int ProviderId { get; set; }
        public string? Color { get; set; }
        public string? CommissionAgency { get; set; }
        public string? CommissionClient { get; set; }
        public string? CommissionEmployee { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("ProviderId")]
        public Providers? Providers { get; set; }
    }
}
