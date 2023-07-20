using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitationsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public HabitationsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Habitations>> getHabitations()
        {
            var habitations = await _context.Habitations
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(habitations);
        }
        [HttpGet("{HabitationId}")]
        public async Task<ActionResult> getHabitationById(int HabitationId)
        {
            var habitation = await _context.Habitations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.HabitationId == HabitationId);
            if (habitation == null)
            {
                return NotFound();
            }
            return Ok(habitation);
        }
        [HttpPost]
        public async Task<ActionResult<Habitations>> PostHabitation(Habitations habitation)
        {
            _context.Habitations.Add(habitation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getHabitationById", new { HabitationId = habitation.HabitationId }, habitation);
        }
        [HttpPut("{HabitationId}")]
        public async Task<ActionResult> PutHabitation(int HabitationId, Habitations habitation)
        {
            if (habitation.HabitationId != HabitationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var habitationOld = await _context.Habitations
                .FirstOrDefaultAsync(f => f.HabitationId == HabitationId);

            if (habitationOld == null)
            {
                return BadRequest($"La habitacion con el ID {HabitationId} no existe");
            }
            habitationOld.HabitationId = habitation.HabitationId;
            habitationOld.Invoice = habitation.Invoice;
            habitationOld.Ages = habitation.Ages;
            habitationOld.Adults = habitation.Adults;
            habitationOld.Minors = habitation.Minors;
            habitationOld.SaleDate = habitation.SaleDate;
            habitationOld.Cost = habitation.Cost;
            habitationOld.IsDeleted = habitation.IsDeleted;

            _context.Habitations.Update(habitationOld);
            await _context.SaveChangesAsync();
            return Ok("La habitación se actualizo correctamente");
        }
        [HttpDelete("{HabitationId}")]
        public async Task<IActionResult> DeleteHabitation(int HabitationId)
        {
            var habitation = await _context.Habitations
                .FirstOrDefaultAsync(f => f.HabitationId == HabitationId);
            if (habitation == null)
            {
                return NotFound();
            }

            habitation.IsDeleted = true;
            _context.Habitations.Update(habitation);
            await _context.SaveChangesAsync();
            return Ok("Habitación archivado");
        }
    }
}
