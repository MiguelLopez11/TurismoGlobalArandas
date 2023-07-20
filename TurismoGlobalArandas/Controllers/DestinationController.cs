using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public DestinationController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Destinations>> getDestinations()
        {
            var destinations = await _context.Destinations
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(destinations);
        }
        [HttpGet("{DestinationId}")]
        public async Task<ActionResult> getDestinationById(int DestinationId)
        {
            var destination = await _context.Destinations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.DestinationId == DestinationId);
            if (destination == null)
            {
                return NotFound();
            }
            return Ok(destination);
        }
        [HttpPost]
        public async Task<ActionResult<Destinations>> PostDestination(Destinations destination)
        {
            _context.Destinations.Add(destination);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getDestinationById", new { DestinationId = destination.DestinationId }, destination);
        }
        [HttpPut("{DestinationId}")]
        public async Task<ActionResult> PutDestination(int DestinationId, Destinations destination)
        {
            if (destination.DestinationId != DestinationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var destinationOld = await _context.Destinations
                .FirstOrDefaultAsync(f => f.DestinationId == DestinationId);

            if (destinationOld == null)
            {
                return BadRequest($"La habitacion con el ID {DestinationId} no existe");
            }
            destinationOld.DestinationId = destination.DestinationId;
            destinationOld.Name = destination.Name;
            destinationOld.Description = destination.Description;
            destinationOld.IsDeleted = destination.IsDeleted;

            _context.Destinations.Update(destinationOld);
            await _context.SaveChangesAsync();
            return Ok("Destino actualizado correctamente");
        }
        [HttpDelete("{DestinationId}")]
        public async Task<IActionResult> DeleteDestination(int DestinationId)
        {
            var destination = await _context.Destinations
                .FirstOrDefaultAsync(f => f.DestinationId == DestinationId);
            if (destination == null)
            {
                return NotFound();
            }

            destination.IsDeleted = true;
            _context.Destinations.Update(destination);
            await _context.SaveChangesAsync();
            return Ok("destino archivado");
        }
    }
}
