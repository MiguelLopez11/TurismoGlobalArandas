using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TurismoGlobalArandas.Models
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        [Required]
        [StringLength(50)]
        public string? Lastname { get; set; }
        [Required]
        [StringLength(50)]
        public string? WorkStation { get; set; }
        [StringLength(50)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string PhoneNumber { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Salary { get; set; }
        public bool IsDeleted { get; set; } =  false;
    }
}
