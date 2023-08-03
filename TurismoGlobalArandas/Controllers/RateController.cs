using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public RateController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Rate>> getRates()
        {
            var Rates = await _context.Rates
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(Rates);
        }
        [HttpGet("{RateId}")]
        public async Task<ActionResult> getRateById(int RateId)
        {
            var rate = await _context.Rates
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.RateId == RateId);
            if (rate == null)
            {
                return NotFound();
            }
            return Ok(rate);
        }
        [HttpPost]
        public async Task<ActionResult<Rate>> PostRate(Rate rate)
        {
            _context.Rates.Add(rate);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getRateById", new { RateId = rate.RateId }, rate);
        }
        [HttpPut("{RateId}")]
        public async Task<ActionResult> PutRate(int RateId, Rate rate)
        {
            if (rate.RateId != RateId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var rateOld = await _context.Rates
                .FirstOrDefaultAsync(f => f.RateId == RateId);
            if (rateOld == null)
            {
                return BadRequest($"La categoría con el ID {RateId} no existe");
            }
            rateOld.RateId = rate.RateId;
            rateOld.RateTypeId = rate.RateTypeId;
            rateOld.IndividualHabitation = rate.IndividualHabitation;
            rateOld.DoubleHabitation = rate.DoubleHabitation;
            rateOld.TripleHabitation = rate.TripleHabitation;
            rateOld.QDPLHabitation = rate.QDPLHabitation;
            rateOld.JuniorHabitation = rate.JuniorHabitation;
            rateOld.MinorHabitation = rate.MinorHabitation;
            rateOld.AgesMinors = rate.AgesMinors;
            rateOld.AgesJuniors = rate.AgesJuniors;
            rateOld.IsDeleted = rate.IsDeleted;

            _context.Rates.Update(rateOld);
            await _context.SaveChangesAsync();
            return Ok("La tarifa se actualizo correctamente");
        }
        [HttpDelete("{RateId}")]
        public async Task<IActionResult> DeleteRate(int RateId)
        {
            var rate = await _context.Rates
                .FirstOrDefaultAsync(f => f.RateId == RateId);
            if (rate == null)
            {
                return NotFound();
            }

            rate.IsDeleted = true;
            _context.Rates.Update(rate);
            await _context.SaveChangesAsync();
            return Ok("Tarifa archivado");
        }
    }
}
