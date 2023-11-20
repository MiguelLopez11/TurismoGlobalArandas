using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models.Identity
{
    [Keyless]
    public class ReservationsByEmployeeView
    {
        public string? Origin { get; set; }
        public decimal? TotalCost { get; set; }
        public string? Invoice { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsSoldOut { get; set; }
        public DateTime? DateSale { get; set; }
    }
}
