using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class Habitations
    {
        [Key]
        public int HabitationId { get; set; }
        public string? Invoice { get; set; }
        public string? TypeHabitation { get; set; }
        public decimal? AdvancePayment { get; set; }
        public string? Ages { get; set; }
        public int? Adults { get; set; }
        public int? Minors { get; set; }
        //public DateTime? PurchaseDate { get; set; }
        public double?  Cost { get; set; }
        public string? Observations { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
