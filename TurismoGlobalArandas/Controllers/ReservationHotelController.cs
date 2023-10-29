 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationHotelController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationHotel>> getReservationsHotel()
        {
            var Reservations = await _context.ReservationHotels
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.TypeReservationGrupal)
                .Include(i => i.Destinations)
                .ToListAsync();
            return Ok(Reservations);
        }

        [HttpGet("{ReservationHotelId}")]
        public async Task<ActionResult> getReservationHotelById(int ReservationHotelId)
        {
            var Reservation = await _context.ReservationHotels
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.TypeReservationGrupal)
                .Include(i => i.Destinations)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationHotel>> PostReservationHotel(ReservationHotel Reservation)
        {
            
            Reservation.DateSale = DateTime.Now;
            _context.ReservationHotels.Add(Reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationHotelById",
                new { ReservationHotelId = Reservation.ReservationHotelId },
                Reservation
            );
        }

        [HttpPut("{ReservationHotelId}")]
        public async Task<ActionResult> PutReservationHotel(int ReservationHotelId, ReservationHotel Reservation)
        {
            if (Reservation.ReservationHotelId != ReservationHotelId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La habitacion con el ID {ReservationHotelId} no existe");
            }
            ReservationOld.ReservationHotelId = Reservation.ReservationHotelId;
            ReservationOld.ReservationInvoice = Reservation.ReservationInvoice;
            ReservationOld.TravelDateStart = Reservation.TravelDateStart;
            ReservationOld.TravelDateEnd = Reservation.TravelDateEnd;
            ReservationOld.TypeHabitation = Reservation.TypeHabitation;
            ReservationOld.Agent = Reservation.Agent;
            ReservationOld.PaymentLimitDateProvider = Reservation.PaymentLimitDateProvider;
            ReservationOld.NumberHabitations = Reservation.NumberHabitations;
            ReservationOld.PhoneContact = Reservation.PhoneContact;
            ReservationOld.Observations = Reservation.Observations;
            ReservationOld.GroupCoordinator = Reservation.GroupCoordinator;
            ReservationOld.PhoneCoordinator = Reservation.PhoneCoordinator;
            ReservationOld.DateSale = Reservation.DateSale;
            ReservationOld.PaymentPeriod = Reservation.PaymentPeriod;
            ReservationOld.PaymentLimitDate = Reservation.PaymentLimitDate;
            ReservationOld.CancellationPolicy = Reservation.CancellationPolicy;
            ReservationOld.CodeVoicher = Reservation.CodeVoicher;
            ReservationOld.TotalCost = Reservation.TotalCost;
            ReservationOld.TypeReservationId = Reservation.TypeReservationId;
            ReservationOld.TypeReservationGroupId = Reservation.TypeReservationGroupId;
            ReservationOld.EmployeeId = Reservation.EmployeeId;
            ReservationOld.CustomerId = Reservation.CustomerId;
            ReservationOld.HotelId = Reservation.HotelId;
            ReservationOld.DestinationId = Reservation.DestinationId;
            ReservationOld.ProviderId = Reservation.ProviderId;
            ReservationOld.IsDeleted = Reservation.IsDeleted;

            _context.ReservationHotels.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationHotelId}")]
        public async Task<IActionResult> DeleteReservationHotel(int ReservationHotelId)
        {
            var Reservation = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationHotels.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
