using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusFlightController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public StatusFlightController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<StatusFlight>> getStatusFlights()
        {
            var statusFligths = await _context.StatusFlight
                .ToListAsync();
            return Ok(statusFligths);
        }
        [HttpGet("{StatusFlightId}")]
        public async Task<ActionResult> getStatusFlightById(int StatusFlightId)
        {
            var statusFlight = await _context.StatusFlight
                .FirstOrDefaultAsync(f => f.StatusFlightId == StatusFlightId);
            if (statusFlight == null)
            {
                return NotFound();
            }
            return Ok(statusFlight);
        }
        [HttpPost]
        public async Task<ActionResult<StatusFlight>> PostStatusFlight(StatusFlight statusFlight)
        {
            _context.StatusFlight.Add(statusFlight);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getStatusFlightById", new { StatusFlightId = statusFlight.StatusFlightId }, statusFlight);
        }
        [HttpPut("{StatusFlightId}")]
        public async Task<ActionResult> PutStatusFlight(int StatusFlightId, StatusFlight statusFlight)
        {
            if (statusFlight.StatusFlightId != StatusFlightId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var statusFlightOld = await _context.StatusFlight
                .FirstOrDefaultAsync(f => f.StatusFlightId == StatusFlightId);

            if (statusFlightOld == null)
            {
                return BadRequest($"La habitacion con el ID {StatusFlightId} no existe");
            }
            statusFlightOld.StatusFlightId = statusFlight.StatusFlightId;
            statusFlightOld.Name = statusFlight.Name;
            statusFlightOld.Description = statusFlight.Description;
            statusFlightOld.IsDeleted = statusFlight.IsDeleted;

            _context.StatusFlight.Update(statusFlightOld);
            await _context.SaveChangesAsync();
            return Ok("status actualizado correctamente");
        }
        [HttpDelete("{StatusFlightId}")]
        public async Task<IActionResult> DeleteStatusFlight(int StatusFlightId)
        {
            var statusFlight = await _context.StatusFlight
                .FirstOrDefaultAsync(f => f.StatusFlightId == StatusFlightId);
            if (statusFlight == null)
            {
                return NotFound();
            }

            _context.StatusFlight.Remove(statusFlight);
            await _context.SaveChangesAsync();
            return Ok("status eliminado");
        }
    }
}
