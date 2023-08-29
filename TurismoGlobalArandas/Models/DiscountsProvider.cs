using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class DiscountsProvider
    {
        [Key]
        public int DiscountProviderId { get; set; }
        public string Color { get; set; }
        public double Discount { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
    