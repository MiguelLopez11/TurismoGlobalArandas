using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public NotificationsController(TurismoGlobalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<Notifications>> getAditionalServices()
        {
            var notifications = await _context.Notifications
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(notifications);
        }
    }
}
