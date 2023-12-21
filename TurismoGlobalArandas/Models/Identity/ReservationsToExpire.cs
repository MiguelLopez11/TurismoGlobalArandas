using Microsoft.EntityFrameworkCore;

namespace TurismoGlobalArandas.Models.Identity
{
    [Keyless]
    public class ReservationsToExpire
    {
        public string? Origin { get; set; }
        public string?  Invoice { get; set; }
    }
}
