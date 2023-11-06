using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Expenses
    {
        [Key]
        public int ExpenseId { get; set; }
        public string Invoice { get; set;}
        public string Name { get; set;}
        public string Description { get; set;}
        public decimal Cost { get; set;}
        public bool IsExpenseFixed { get; set;}
        public bool IsDeleted { get; set; }

    }
}
