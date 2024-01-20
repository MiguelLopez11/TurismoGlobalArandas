using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentProviderList
    {
        [Key]
        public int PaymentId { get; set; }
        public string? Invoice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? PaymentConceptId { get; set; }
        public DateTime? PaymentDate { get; set; } = DateTime.UtcNow;
        public int PaymentProviderId { get; set; }
        public string? Observations { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PaymentProviderId")]
        public PaymentProviders? PaymentProviders { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethods? PaymentMethods { get; set; }
        [ForeignKey("PaymentConceptId")]
        public PaymentConcept? PaymentConcept { get; set; }
    }
}
