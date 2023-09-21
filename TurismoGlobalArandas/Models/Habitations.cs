using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Habitations
    {
        [Key]
        public int HabitationId { get; set; }
        public string? Invoice { get; set; }
        public string? TypeHabitation { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? AdvancePayment { get; set; }
        public string? Ages { get; set; }
        public int? Adults { get; set; }
        public int? Minors { get; set; }
        public double?  Cost { get; set; }
        public string? Observations { get; set; }
        public bool IsDeleted { get; set; }
        
    }
}
