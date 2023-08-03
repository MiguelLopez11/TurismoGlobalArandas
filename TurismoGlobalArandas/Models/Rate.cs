using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Rate
    {
        [Key]
        public int RateId { get; set; }
        public int RateTypeId { get; set; }
        public decimal IndividualHabitation { get; set; }
        public decimal DoubleHabitation { get; set; }
        public decimal TripleHabitation { get; set; }
        public decimal QDPLHabitation { get; set; }
        public decimal JuniorHabitation { get; set; }
        public decimal MinorHabitation { get; set; }
        public int AgesMinors { get; set; }
        public int AgesJuniors { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("RateTypeId")]
        public RateType? RateTypes { get; set; }
    }
}
