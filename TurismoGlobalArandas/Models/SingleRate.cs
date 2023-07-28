using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class SingleRate
    {
        [Key] public int SingleRateId { get; set; }
        public decimal PublicRate { get; set; }
        public double ExtraDiscount { get; set; }
        public decimal RateDiscounted { get; set; }
        public decimal RateClient { get; set; }
        public bool IsDeleted { get; set; }
    }
}
