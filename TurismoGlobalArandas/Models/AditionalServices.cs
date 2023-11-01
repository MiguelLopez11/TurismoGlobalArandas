using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class AditionalServices
    {
        [Key]
        public int AditionalServiceId { get; set; }
        public string Name { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Cost { get; set; }
        public string? Description { get; set; }
        
        public bool IsDeleted { get; set; }


        
    }
}
