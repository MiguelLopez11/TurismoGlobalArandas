using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class PaymentRelationList
    {
        [Key]
        public int PaymentId { get; set; }
        public string? Invoice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountReceivedClient { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AmountReturnedClient { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observations { get; set; }
        public int PaymentMethodId { get; set; }
        public string? DetailsPayment { get; set; }
        public int? PaymentReservationId { get; set; }
        public int? EmployeeId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PaymentMethodId")]
        public PaymentMethods? PaymentMethods { get; set; }
        [ForeignKey("PaymentReservationId")]
        public PaymentsRelationReservations? PaymentsRelationReservations { get; set; }
        [ForeignKey("EmployeeId")]
        public Employees? Employees { get; set; }
    }
}
