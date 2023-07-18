using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Habitation
    {
        [Key]
        public int HabitationId { get; set; }
        [Required]
        public string Invoice { get; set; }
        [Required]
        public int Ages { get; set; }

        [Required]
        public int Adults { get; set; }
        [Required]
        public int Minors { get; set; }
        public DateTime SaleDate { get; set; }
        public double Cost { get; set; }
        
    }
}
