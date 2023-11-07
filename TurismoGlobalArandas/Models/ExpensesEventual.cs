using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class ExpensesEventual
    {
        [Key]
        public int ExpenseId { get; set; }
        public string Name { get; set;}
        public string? Description { get; set;}
        public decimal Cost { get; set;}
        public bool IsDeleted { get; set; }

    }
}
