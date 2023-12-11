using System.ComponentModel.DataAnnotations;

namespace TurismoGlobalArandas.Models.Identity.Repository
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
    }
}
