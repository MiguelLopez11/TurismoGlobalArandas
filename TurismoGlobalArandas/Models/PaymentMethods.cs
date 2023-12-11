using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class PaymentMethods
    {
        [Key] 
        public int PaymentMethodId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
