using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class ExpensesFixed
    {
        [Key] public int ExpensesFixedId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public bool IsDeleted { get; set; }
    }
}
