using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeReservationGrupalController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public TypeReservationGrupalController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<TypeReservationGrupal>> getTypeReservationGrupals()
        {
            var typeReservationGrupals = await _context.TypeReservationGrupals
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(typeReservationGrupals);
        }
        [HttpGet("{TypeReservationGrupalId}")]
        public async Task<ActionResult> getTypeReservationGrupalById(int TypeReservationGrupalId)
        {
            var typeReservation = await _context.TypeReservationGrupals
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.TypeReservationGrupalId == TypeReservationGrupalId);
            if (typeReservation == null)
            {
                return NotFound();
            }
            return Ok(typeReservation);
        }
        [HttpPost]
        public async Task<ActionResult<TypeReservationGrupal>> PostTypeReservationGrupal(TypeReservationGrupal typeReservation)
        {
            _context.TypeReservationGrupals.Add(typeReservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getTypeReservationGrupalById", new { TypeReservationGrupalId = typeReservation.TypeReservationGrupalId }, typeReservation);
        }
        [HttpPut("{TypeReservationGrupalId}")]
        public async Task<ActionResult> PutTypeReservationGrupal(int TypeReservationGrupalId, TypeReservationGrupal typeReservation)
        {
            if (typeReservation.TypeReservationGrupalId != TypeReservationGrupalId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var typeReservationOld = await _context.TypeReservationGrupals
                .FirstOrDefaultAsync(f => f.TypeReservationGrupalId == TypeReservationGrupalId);
            if (typeReservationOld == null)
            {
                return BadRequest($"La categoría con el ID {TypeReservationGrupalId} no existe");
            }
            typeReservationOld.TypeReservationGrupalId = typeReservation.TypeReservationGrupalId;
            typeReservationOld.Name = typeReservation.Name;
            typeReservationOld.Description = typeReservation.Description;
            typeReservationOld.IsDeleted = typeReservation.IsDeleted;

            _context.TypeReservationGrupals.Update(typeReservationOld);
            await _context.SaveChangesAsync();
            return Ok("El tipo de reserva grupal se actualizo correctamente");
        }
        [HttpDelete("{TypeReservationGrupalId}")]
        public async Task<IActionResult> DeleteTypeReservationGrupal(int TypeReservationGrupalId)
        {
            var typeReservation = await _context.TypeReservationGrupals
                .FirstOrDefaultAsync(f => f.TypeReservationGrupalId == TypeReservationGrupalId);
            if (typeReservation == null)
            {
                return NotFound();
            }

            typeReservation.IsDeleted = true;
            _context.TypeReservationGrupals.Update(typeReservation);
            await _context.SaveChangesAsync();
            return Ok("El tipo de reserva grupal ah sido archivado");
        }
    }
}
