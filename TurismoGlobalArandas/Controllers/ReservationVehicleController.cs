using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationVehicleController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationVehicleController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationVehicle>> getReservationVehicles()
        {
            var reservationVehicles = await _context.ReservationVehicles
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted).ToListAsync();
            return Ok(reservationVehicles);
        }
        [HttpGet("{ReservationVehicleId}")]
        public async Task<ActionResult> getReservationVehicle(int ReservationVehicleId)
        {
            var reservationVehicle = await _context.ReservationVehicles
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (reservationVehicle == null)
            {
                return NotFound();
            }
            return Ok(reservationVehicle);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationVehicle>> PostReservationVehicle(ReservationVehicle reservationVehicle)
        {
            reservationVehicle.DateSale = DateTime.Now; 
            _context.ReservationVehicles.Add(reservationVehicle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationVehicle",
                new { ReservationVehicleId = reservationVehicle.ReservationVehicleId },
                reservationVehicle
            );

        }

        [HttpPut("{ReservationVehicleId}")]
        public async Task<ActionResult> PutReservationVehicle(int ReservationVehicleId, ReservationVehicle reservationVehicle)
        {
            if (reservationVehicle.ReservationVehicleId != ReservationVehicleId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == ReservationVehicleId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La reservación con el ID {ReservationVehicleId} no existe");
            }
            ReservationOld.ReservationVehicleId = reservationVehicle.ReservationVehicleId;
            ReservationOld.Invoice = reservationVehicle.Invoice;
            ReservationOld.DateSale = reservationVehicle.DateSale;
            ReservationOld.DateTravel = reservationVehicle.DateTravel;
            ReservationOld.DepartureLocation = reservationVehicle.DepartureLocation;
            ReservationOld.ArrivalLocation = reservationVehicle.ArrivalLocation;
            ReservationOld.ProviderId = reservationVehicle.ProviderId;
            ReservationOld.PricePublic = reservationVehicle.PricePublic;
            ReservationOld.PriceNeto = reservationVehicle.PriceNeto;
            ReservationOld.Description = reservationVehicle.Description;
            ReservationOld.IsDeleted = reservationVehicle.IsDeleted;

            _context.ReservationVehicles.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationVehicleId}")]
        public async Task<IActionResult> DeleteReservationVehicle(int ReservationVehicleId)
        {
            var Reservation = await _context.ReservationVehicles.FirstOrDefaultAsync(
                f => f.ReservationVehicleId == ReservationVehicleId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationVehicles.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
