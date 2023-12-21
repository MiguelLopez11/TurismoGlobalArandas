using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public AirlineController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Airline>> getAirlines()
        {
            var airlines = await _context.Airlines
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(airlines);
        }
        [HttpGet("{AirlineId}")]
        public async Task<ActionResult> getAirlineById(int AirlineId)
        {
            var airline = await _context.Airlines
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.AirlineId == AirlineId);
            if (airline == null)
            {
                return NotFound();
            }
            return Ok(airline);
        }
        [HttpPost]
        public async Task<ActionResult<Airline>> PostAirline(Airline airline)
        {
            _context.Airlines.Add(airline);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getAirlineById", new { AirlineId = airline.AirlineId }, airline);
        }
        [HttpPut("{AirlineId}")]
        public async Task<ActionResult> PutAirline(int AirlineId, Airline airline)
        {
            if (airline.AirlineId != AirlineId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var airlineOld = await _context
                .Airlines.FirstOrDefaultAsync(f => f.AirlineId == AirlineId);
            if (airlineOld == null)
            {
                return BadRequest($"La aerolinea con el ID {AirlineId} no existe");
            }
            airlineOld.AirlineId = airline.AirlineId;
            airlineOld.Name = airline.Name;
            airlineOld.PhoneNumber = airline.PhoneNumber;
            airlineOld.IsDeleted = airline.IsDeleted;

            _context.Airlines.Update(airlineOld);
            await _context.SaveChangesAsync();
            return Ok("La aerolinea se actualizo correctamente");
        }
        [HttpDelete("{AirlineId}")]
        public async Task<IActionResult> DeleteAirline(int AirlineId)
        {
            var airline = await _context.Airlines
                .FirstOrDefaultAsync(f => f.AirlineId == AirlineId);
            if (airline == null)
            {
                return NotFound();
            }

            airline.IsDeleted = true;
            _context.Airlines.Update(airline);
            await _context.SaveChangesAsync();
            return Ok("Servicio adicional archivado");
        }
    }
}
