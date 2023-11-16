using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelGroupController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationHotelGroupController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationHotelGroup>> getReservationsHotelGroup()
        {
            var Reservations = await _context.ReservationHotelGroups
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted).ToListAsync();
            return Ok(Reservations);
        }

        [HttpGet("{ReservationHotelGroupId}")]
        public async Task<ActionResult> getReservationHotelGroupById(int ReservationHotelGroupId)
        {
            var Reservation = await _context.ReservationHotelGroups
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelGroupId == ReservationHotelGroupId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }
        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<ActionResult> getReservationHotelGroupByReservationHotelId(int ReservationHotelId)
        {
            var Reservation = await _context.ReservationHotelGroups
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationHotelGroup>> PostReservationHotelGroup(ReservationHotelGroup reservationHotelGroup)
        {
            var reservation = await _context.ReservationHotelGroups
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == reservationHotelGroup.ReservationHotelId);
            if (reservation == null)
            {
                _context.ReservationHotelGroups.Add(reservationHotelGroup);
                await _context.SaveChangesAsync();
                return CreatedAtAction(
                    "getReservationHotelGroupByReservationHotelId",
                    new { ReservationHotelId = reservationHotelGroup.ReservationHotelGroupId },
                    reservationHotelGroup
                );
            }
            else
            {
                return Ok("Ya cuenta con una reservación grupal");
            }
        }
        [HttpPost("IfExist/{ReservationHotelId}")]
        public async Task<ActionResult> comprobateIfExist(int ReservationHotelId)
        {
            var reservation = await _context.ReservationHotelGroups
                .Include(i => i.reservationHotel)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (reservation == null)
            {
                return StatusCode(404, "No se encuentra una reservación de grupo");
            }
            else
            {
                return StatusCode(200, "Si existe una reservación en grupo");
            }
        }

        [HttpPut("{ReservationHotelGroupId}")]
        public async Task<ActionResult> PutReservationHotelGroup(int ReservationHotelGroupId, ReservationHotelGroup reservationHotelGroup)
        {
            if (reservationHotelGroup.ReservationHotelGroupId != ReservationHotelGroupId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationHotelGroups.FirstOrDefaultAsync(
                f => f.ReservationHotelGroupId == ReservationHotelGroupId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La reservación con el ID {ReservationHotelGroupId} no existe");
            }
            ReservationOld.ReservationHotelGroupId = reservationHotelGroup.ReservationHotelGroupId;
            ReservationOld.GroupName = reservationHotelGroup.GroupName;
            ReservationOld.ConfirmationKey = reservationHotelGroup.ConfirmationKey;
            ReservationOld.DateArrival = reservationHotelGroup.DateArrival;
            ReservationOld.Coordinator = reservationHotelGroup.Coordinator;
            ReservationOld.PhoneNumber = reservationHotelGroup.PhoneNumber;
            ReservationOld.DateStart = reservationHotelGroup.DateStart;
            ReservationOld.DateEnd = reservationHotelGroup.DateEnd;
            ReservationOld.RangePublicClient = reservationHotelGroup.RangePublicClient;
            ReservationOld.RangeJunior = reservationHotelGroup.RangeJunior;
            ReservationOld.RangeMinor = reservationHotelGroup.RangeMinor;
            ReservationOld.NightsNumber = reservationHotelGroup.NightsNumber;
            ReservationOld.ReservationHotelId = reservationHotelGroup.ReservationHotelId;
            ReservationOld.IsDeleted = reservationHotelGroup.IsDeleted;

            _context.ReservationHotelGroups.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationHotelGroupId}")]
        public async Task<IActionResult> DeleteReservationHotelGroup(int ReservationHotelGroupId)
        {
            var Reservation = await _context.ReservationHotelGroups.FirstOrDefaultAsync(
                f => f.ReservationHotelGroupId == ReservationHotelGroupId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationHotelGroups.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}