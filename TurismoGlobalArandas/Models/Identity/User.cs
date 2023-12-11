using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Entities
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public int? EmployeeId { get; set; } = null;

        [ForeignKey("EmployeeId")]
        public Employees? Employee { get; set; }
    }
}
