using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeReservationController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public TypeReservationController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TypeReservation>> getTypeReservations()
        {
            var TypeReservations = await _context.TypeReservations
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(TypeReservations);
        }

        [HttpGet("{TypeReservationId}")]
        public async Task<ActionResult> getTypeReservationById(int TypeReservationId)
        {
            var habitationsReservation = await _context.TypeReservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.TypeReservationId == TypeReservationId);
            if (habitationsReservation == null)
            {
                return NotFound();
            }
            return Ok(habitationsReservation);
        }

        [HttpPost]
        public async Task<ActionResult<TypeReservation>> PostTypeReservation(
            TypeReservation typeReservation
        )
        {
            _context.TypeReservations.Add(typeReservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getTypeReservationById",
                new { TypeReservationId = typeReservation.TypeReservationId },
                typeReservation
            );
        }

        [HttpPut("{TypeReservationId}")]
        public async Task<ActionResult> PutTypeReservation(
            int TypeReservationId,
            TypeReservation typeReservation
        )
        {
            if (typeReservation.TypeReservationId != TypeReservationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var TypeReservationOld =
                await _context.TypeReservations
                .FirstOrDefaultAsync(f => f.TypeReservationId == TypeReservationId);

            if (TypeReservationOld == null)
            {
                return BadRequest($"La habitacion con el ID {TypeReservationId} no existe");
            }
            TypeReservationOld.TypeReservationId = typeReservation.TypeReservationId;
            TypeReservationOld.Name = typeReservation.Name;
            TypeReservationOld.Description = typeReservation.Description;
            TypeReservationOld.IsDeleted = typeReservation.IsDeleted;

            _context.TypeReservations.Update(TypeReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{TypeReservationId}")]
        public async Task<IActionResult> DeleteTYpeReservation(int TypeReservationId)
        {
            var typeReservation = await _context.TypeReservations.FirstOrDefaultAsync(
                f => f.TypeReservationId == TypeReservationId
            );
            if (typeReservation == null)
            {
                return NotFound();
            }

            typeReservation.IsDeleted = true;
            _context.TypeReservations.Update(typeReservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
