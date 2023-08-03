using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateTypeController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public RateTypeController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<RateType>> getRateTypes()
        {
            var RateTypes = await _context.RateTypes
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(RateTypes);
        }
        [HttpGet("{RateTypeId}")]
        public async Task<ActionResult> getRateTypeById(int RateTypeId)
        {
            var rateType = await _context.RateTypes
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.RateTypeId == RateTypeId);
            if (rateType == null)
            {
                return NotFound();
            }
            return Ok(rateType);
        }
        [HttpPost]
        public async Task<ActionResult<RateType>> PostRateType(RateType rateType)
        {
            _context.RateTypes.Add(rateType);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getRateTypeById", new { RateTypeId = rateType.RateTypeId }, rateType);
        }
        [HttpPut("{RateTypeId}")]
        public async Task<ActionResult> PutRate(int RateTypeId, RateType rateType)
        {
            if (rateType.RateTypeId != RateTypeId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var RateTypeOld = await _context.RateTypes
                .FirstOrDefaultAsync(f => f.RateTypeId == RateTypeId);
            if (RateTypeOld == null)
            {
                return BadRequest($"La categoría con el ID {RateTypeId} no existe");
            }
            RateTypeOld.RateTypeId = rateType.RateTypeId;
            RateTypeOld.Name = rateType.Name;
            RateTypeOld.Description = rateType.Description;
            RateTypeOld.IsDeleted = rateType.IsDeleted;

            _context.RateTypes.Update(RateTypeOld);
            await _context.SaveChangesAsync();
            return Ok("La categoría tarifa se actualizo correctamente");
        }
        [HttpDelete("{RateTypeId}")]
        public async Task<IActionResult> DeleteRateType(int RateTypeId)
        {
            var rateType = await _context.RateTypes
                .FirstOrDefaultAsync(f => f.RateTypeId == RateTypeId);
            if (rateType == null)
            {
                return NotFound();
            }

            rateType.IsDeleted = true;
            _context.RateTypes.Update(rateType);
            await _context.SaveChangesAsync();
            return Ok("Tipo de tarifa archivado");
        }
    }
}
