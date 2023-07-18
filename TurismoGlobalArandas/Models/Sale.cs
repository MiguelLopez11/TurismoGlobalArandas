using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Sale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Range(1000, int.MaxValue)]
        public int SaleId { get; set; }
        [Required]
        public string TypeReservation { get; set; }
        public int? NumberHabitations { get; set; }
        [Required]
        public string PhoneContact { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        [Required]
        public string CodeVoucher { get; set; }
        [Required]
        public int PromoterId { get; set; }
        [Required]
        public int ReservationId { get; set; }

        public string Observations { get; set; }

        [ForeignKey("PromoterId")]
        public Employees? Employee { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation? Reservation  { get; set; }
    }
}
