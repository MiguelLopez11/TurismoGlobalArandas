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
                .OrderByDescending(o => o.NotificationId)
                .ToListAsync();
            return Ok(notifications);
        }
        [HttpPut("Readed/{NotificationId}")]
        public async Task<ActionResult> NotificationReaded(int NotificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(f => f.NotificationId == NotificationId);

            if (notification == null)
            {
                return NotFound();
            }
            notification.IsReaded = true;
            _context.Notifications.Update(notification);
            await _context.SaveChangesAsync();
            return Ok("Notificación leída");
        }
    }
}
