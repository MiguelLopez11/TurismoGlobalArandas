using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Reservations>> getReservations()
        {
            var Reservations = await _context.Reservations
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.CategoryReservation)
                .Include(i => i.HabitationsReservation)
                .Where(w => !w.IsDeleted).ToListAsync();
            return Ok(Reservations);
        }

        [HttpGet("{ReservationId}")]
        public async Task<ActionResult> getReservationById(int ReservationId)
        {
            var Reservation = await _context.Reservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationId == ReservationId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }

        [HttpPost]
        public async Task<ActionResult<Reservations>> PostReservation(Reservations Reservation)
        {
            _context.Reservations.Add(Reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationById",
                new { getReservationById = Reservation.ReservationId },
                Reservation
            );
        }

        [HttpPut("{ReservationId}")]
        public async Task<ActionResult> PutHabitation(int ReservationId, Reservations Reservation)
        {
            if (Reservation.ReservationId != ReservationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.Reservations.FirstOrDefaultAsync(
                f => f.ReservationId == ReservationId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La habitacion con el ID {ReservationId} no existe");
            }
            ReservationOld.ReservationId = Reservation.ReservationId;
            ReservationOld.ReservationInvoice = Reservation.ReservationInvoice;
            ReservationOld.TravelDate = Reservation.TravelDate;
            ReservationOld.TypeHabitation = Reservation.TypeHabitation;
            ReservationOld.NumberHabitations = Reservation.NumberHabitations;
            ReservationOld.PhoneContact = Reservation.PhoneContact;
            ReservationOld.Observations = Reservation.Observations;
            ReservationOld.GroupCoordinator = Reservation.GroupCoordinator;
            ReservationOld.PhoneCoordinator = Reservation.PhoneCoordinator;
            ReservationOld.DateSale = Reservation.DateSale;
            ReservationOld.Promoter = Reservation.Promoter;
            ReservationOld.PaymentPeriod = Reservation.PaymentPeriod;
            ReservationOld.PaymentLimitDate = Reservation.PaymentLimitDate;
            ReservationOld.TypeReservationId = Reservation.TypeReservationId;
            ReservationOld.CategoryReservationId = Reservation.CategoryReservationId;
            ReservationOld.EmployeeId = Reservation.EmployeeId;
            ReservationOld.CustomerId = Reservation.CustomerId;
            ReservationOld.HotelId = Reservation.HotelId;
            ReservationOld.HabitationsReservationId = Reservation.HabitationsReservationId;
            ReservationOld.InlcudesTrasport = Reservation.InlcudesTrasport;
            ReservationOld.InlcudesBreakfast = Reservation.InlcudesBreakfast;
            ReservationOld.IsDeleted = Reservation.IsDeleted;

            _context.Reservations.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationId}")]
        public async Task<IActionResult> DeleteHabitation(int ReservationId)
        {
            var Reservation = await _context.Reservations.FirstOrDefaultAsync(
                f => f.ReservationId == ReservationId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.Reservations.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
