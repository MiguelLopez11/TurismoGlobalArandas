using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupRateController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public GroupRateController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<GroupRate>> getGroupRates()
        {
            var GroupRates = await _context.GroupRates
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(GroupRates);
        }
        [HttpGet("{GroupRateId}")]
        public async Task<ActionResult> getGroupRateById(int GroupRateId)
        {
            var GroupRate = await _context.GroupRates
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.GroupRateId == GroupRateId);
            if (GroupRate == null)
            {
                return NotFound();
            }
            return Ok(GroupRate);
        }
        [HttpPost]
        public async Task<ActionResult<GroupRate>> PostGroupRate(GroupRate GroupRate)
        {
            _context.GroupRates.Add(GroupRate);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getGroupRateById", new { GroupRateId = GroupRate.GroupRateId }, GroupRate);
        }
        [HttpPut("{GroupRateId}")]
        public async Task<ActionResult> PutGroupRate(int GroupRateId, GroupRate GroupRate)
        {
            if (GroupRate.GroupRateId != GroupRateId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var GroupRateOld = await _context.GroupRates
                .FirstOrDefaultAsync(f => f.GroupRateId == GroupRateId);
            if (GroupRateOld == null)
            {
                return BadRequest($"La categoría con el ID {GroupRateId} no existe");
            }
            GroupRateOld.GroupRateId = GroupRate.GroupRateId;
            GroupRateOld.GroupRateName = GroupRate.GroupRateName;
            GroupRateOld.Confirmation = GroupRate.Confirmation;
            GroupRateOld.DateArrival = GroupRate.DateArrival;
            GroupRateOld.Coordinator = GroupRate.Coordinator;
            GroupRateOld.PhoneNumber = GroupRate.PhoneNumber;
            GroupRateOld.ReservationHotelId = GroupRate.ReservationHotelId;
            GroupRateOld.TypeReservationGrupalId = GroupRate.TypeReservationGrupalId;
            GroupRateOld.IsDeleted = GroupRate.IsDeleted;

            _context.GroupRates.Update(GroupRateOld);
            await _context.SaveChangesAsync();
            return Ok("La tarifa se actualizo correctamente");
        }
        [HttpDelete("{GroupRateId}")]
        public async Task<IActionResult> DeleteGroupRate(int GroupRateId)
        {
            var GroupRate = await _context.GroupRates
                .FirstOrDefaultAsync(f => f.GroupRateId == GroupRateId);
            if (GroupRate == null)
            {
                return NotFound();
            }

            GroupRate.IsDeleted = true;
            _context.GroupRates.Update(GroupRate);
            await _context.SaveChangesAsync();
            return Ok("Tarifa archivado");
        }
    }
}
