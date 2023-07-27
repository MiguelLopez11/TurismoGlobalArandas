using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Habitations
    {
        [Key]
        public int HabitationId { get; set; }
        [Required]
        public string Invoice { get; set; }
        [Required]
        public string Ages { get; set; }

        [Required]
        public int Adults { get; set; }
        [Required]
        public int Minors { get; set; }
        public DateTime SaleDate { get; set; }
        public double?  Cost { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
