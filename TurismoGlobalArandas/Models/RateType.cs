using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models
{
    public class RateType
    {
        [Key]
        public int RateTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
