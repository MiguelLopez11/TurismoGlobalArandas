using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public CommissionsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Commissions>> getCommissions()
        {
            var commissions = await _context.Commissions
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted).ToListAsync();
            return Ok(commissions);
        }

        [HttpGet("{CommissionId}")]
        public async Task<ActionResult> getCommissionById(int CommissionId)
        {
            var Commission = await _context.Commissions
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.CommissionId == CommissionId);
            if (Commission == null)
            {
                return NotFound();
            }
            return Ok(Commission);
        }
        [HttpGet("/Proveedor/{ProveedorId}")]
        public async Task<ActionResult> getCommissionByProvider(int ProveedorId)
        {
            var Commission = await _context.Commissions
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ProviderId == ProveedorId);
            if (Commission == null)
            {
                return NotFound();
            }
            return Ok(Commission);
        }

        [HttpPost]
        public async Task<ActionResult<Commissions>> PostCommission(
            Commissions Commission
        )
        {
            var Provider = await _context.Commissions.FirstOrDefaultAsync(w => w.ProviderId == Commission.ProviderId);
            if (Provider != null)
            {
                return BadRequest("Proveedor ya tiene registrado un descuento");
            }
            _context.Commissions.Add(Commission);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getCommissionById",
                new { CommissionId = Commission.CommissionId },
                Commission
            );
        }

        [HttpPut("{CommissionId}")]
        public async Task<ActionResult> PutCommission(
            int CommissionId,
            Commissions Commission
        )
        {
            if (Commission.CommissionId != CommissionId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var CommissionOld = await _context.Commissions.FirstOrDefaultAsync(
                f => f.CommissionId == CommissionId
            );

            if (CommissionOld == null)
            {
                return BadRequest($"La habitacion con el ID {CommissionId} no existe");
            }
            CommissionOld.CommissionId = Commission.CommissionId;
            CommissionOld.Color = Commission.Color;
            CommissionOld.CommissionAgency = Commission.CommissionAgency;
            CommissionOld.CommissionClient = Commission.CommissionClient;
            CommissionOld.CommissionEmployee = Commission.CommissionEmployee;
            CommissionOld.Description = Commission.Description;
            CommissionOld.IsDeleted = Commission.IsDeleted;

            _context.Commissions.Update(CommissionOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{CommissionId}")]
        public async Task<IActionResult> DeleteDiscountProvider(int CommissionId)
        {
            var commission = await _context.Commissions.FirstOrDefaultAsync(
                f => f.CommissionId == CommissionId
            );
            if (commission == null)
            {
                return NotFound();
            }

            commission.IsDeleted = true;
            _context.Commissions.Update(commission);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
