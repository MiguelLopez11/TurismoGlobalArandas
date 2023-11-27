using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models.Identity
{
    [Keyless]
    public class ReservationsByEmployeeView
    {
        public string? Origin { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalCost { get; set; }
        public string? Invoice { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsSoldOut { get; set; }
        public DateTime? DateSale { get; set; }
    }
}
