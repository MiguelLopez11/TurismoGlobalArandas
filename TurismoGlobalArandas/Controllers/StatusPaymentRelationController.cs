using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusPaymentRelationController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public StatusPaymentRelationController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<StatusPaymentRelations>> getStatusPayment()
        {
            var statusPayment = await _context.StatusPaymentRelations
                .ToListAsync();
            return Ok(statusPayment);
        }
        [HttpGet("{StatusFlightId}")]
        public async Task<ActionResult> getStatusPaymentById(int StatusId)
        {
            var statusPayment = await _context.StatusPaymentRelations
                .FirstOrDefaultAsync(f => f.StatusId == StatusId);
            if (statusPayment == null)
            {
                return NotFound();
            }
            return Ok(statusPayment);
        }
        [HttpPost]
        public async Task<ActionResult<StatusPaymentRelations>> PostStatusPayment(StatusPaymentRelations statusPayment)
        {
            _context.StatusPaymentRelations.Add(statusPayment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getStatusPaymentById", new { StatusId = statusPayment.StatusId }, statusPayment);
        }
        [HttpPut("{StatusFlightId}")]
        public async Task<ActionResult> PutStatusPayment(int StatusId, StatusPaymentRelations statusPayment)
        {
            if (statusPayment.StatusId != StatusId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var statusPaymentOld = await _context.StatusPaymentRelations
                .FirstOrDefaultAsync(f => f.StatusId == StatusId);

            if (statusPaymentOld == null)
            {
                return BadRequest($"La habitacion con el ID {StatusId} no existe");
            }
            statusPaymentOld.StatusId = statusPayment.StatusId;
            statusPaymentOld.Name = statusPayment.Name;
            statusPaymentOld.Description = statusPayment.Description;
            statusPaymentOld.IsDeleted = statusPayment.IsDeleted;

            _context.StatusPaymentRelations.Update(statusPaymentOld);
            await _context.SaveChangesAsync();
            return Ok("status actualizado correctamente");
        }
        [HttpDelete("{StatusId}")]
        public async Task<IActionResult> DeleteStatusPayment(int StatusId)
        {
            var statusPayment = await _context.StatusPaymentRelations
                .FirstOrDefaultAsync(f => f.StatusId == StatusId);
            if (statusPayment == null)
            {
                return NotFound();
            }

            _context.StatusPaymentRelations.Remove(statusPayment);
            await _context.SaveChangesAsync();
            return Ok("status eliminado");
        }
    }
}
