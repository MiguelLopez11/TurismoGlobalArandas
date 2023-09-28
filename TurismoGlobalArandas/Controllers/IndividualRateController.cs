using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndividualRateController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public IndividualRateController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IndividualRate>> getIndividualRate()
        {
            var GroupRates = await _context.IndividualRates
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(GroupRates);
        }
        [HttpGet("{IndividualRateId}")]
        public async Task<ActionResult> getIndividualRateId(int IndividualRateId)
        {
            var IndividualRate = await _context.IndividualRates
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.IndividualRateId == IndividualRateId);
            if (IndividualRate == null)
            {
                return NotFound();
            }
            return Ok(IndividualRate);
        } 
        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<ActionResult> getIndividualRatebyReservationHotel(int ReservationHotelId)
        {
            var IndividualRate = await _context.IndividualRates
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            return Ok(IndividualRate);
        }
        [HttpPost]
        public async Task<ActionResult<IndividualRate>> PostIndividualRate(IndividualRate individualRate)
        {
            _context.IndividualRates.Add(individualRate);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getIndividualRateId", new { IndividualRateId = individualRate.IndividualRateId }, individualRate);
        }
        [HttpPut("{IndividualRateId}")]
        public async Task<ActionResult> PutIndividualRate(int IndividualRateId, IndividualRate individualRate)
        {
            if (individualRate.IndividualRateId != IndividualRateId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var individualRateOld = await _context.IndividualRates
                .FirstOrDefaultAsync(f => f.IndividualRateId == IndividualRateId);
            if (individualRateOld == null)
            {
                return BadRequest($" con el ID {IndividualRateId} no existe");
            }
            individualRateOld.IndividualRateId = individualRate.IndividualRateId;
            individualRateOld.ReservationHotelId = individualRate.ReservationHotelId;
            individualRateOld.PublicRate = individualRate.PublicRate;
            individualRateOld.ClientRate = individualRate.ClientRate;
            individualRateOld.ExtraDiscount = individualRate.ExtraDiscount;
            individualRateOld.IsDeleted = individualRate.IsDeleted;

            _context.IndividualRates.Update(individualRateOld);
            await _context.SaveChangesAsync();
            return Ok("El registro se actualizo correctamente");
        }
        [HttpDelete("{IndividualRateId}")]
        public async Task<IActionResult> DeleteIndividualRate(int IndividualRateId)
        {
            var individualRate = await _context.IndividualRates
                .FirstOrDefaultAsync(f => f.IndividualRateId == IndividualRateId);
            if (individualRate == null)
            {
                return NotFound();
            }

            individualRate.IsDeleted = true;
            _context.IndividualRates.Update(individualRate);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");

        }
    }
}
