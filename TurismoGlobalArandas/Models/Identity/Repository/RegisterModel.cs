using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models.Identity.Repository
{
    public class RegisterModel
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? RoleId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
