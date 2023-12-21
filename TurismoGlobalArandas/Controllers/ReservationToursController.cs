using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationToursController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationToursController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationTours>> getReservationsTours()
        {
            var Reservations = await _context.ReservationTours
                .Include(i => i.Destinations)
                .Where(w => !w.IsDeleted).ToListAsync();
            return Ok(Reservations);
        }
        [HttpGet("{ReservationTourId}")]
        public async Task<ActionResult> getReservationTour(int ReservationTourId)
        {
            var Reservation = await _context.ReservationTours
                .Include(i => i.Destinations)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationTours>> PostReservationTour(ReservationTours reservation)
        {
            reservation.Invoice = _context.GetInvoiceReservationTours();
            _context.ReservationTours.Add(reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationTour",
                new { ReservationTourId = reservation.ReservationTourId },
                reservation
            );

        }

        [HttpPut("{ReservationTourId}")]
        public async Task<ActionResult> PutReservationTour(int ReservationTourId, ReservationTours reservationTours)
        {
            if (reservationTours.ReservationTourId != ReservationTourId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La reservación con el ID {ReservationTourId} no existe");
            }
            ReservationOld.ReservationTourId = reservationTours.ReservationTourId;
            ReservationOld.Invoice = reservationTours.Invoice;
            ReservationOld.TourName = reservationTours.TourName;
            ReservationOld.DestinationId = reservationTours.DestinationId;
            ReservationOld.DateSale = reservationTours.DateSale;
            ReservationOld.PaymentLimitDate = reservationTours.PaymentLimitDate;
            ReservationOld.PaymentLimitDateProvider = reservationTours.PaymentLimitDateProvider;
            ReservationOld.DateActivity = reservationTours.DateActivity;
            ReservationOld.OwnerName = reservationTours.OwnerName;
            ReservationOld.NumberAdults = reservationTours.NumberAdults;
            ReservationOld.NumberMinors = reservationTours.NumberMinors;
            ReservationOld.AdultCost = reservationTours.AdultCost;
            ReservationOld.MinorCost = reservationTours.MinorCost;
            ReservationOld.PublicRate = reservationTours.PublicRate;
            ReservationOld.NetPrice = reservationTours.NetPrice;
            ReservationOld.ExchangeRate = reservationTours.ExchangeRate;
            ReservationOld.IsInternational = reservationTours.IsInternational;
            ReservationOld.IsNational = reservationTours.IsNational;
            ReservationOld.IncludeTransportation = reservationTours.IncludeTransportation;
            ReservationOld.IsSoldOut = reservationTours.IsSoldOut;
            ReservationOld.IsDeleted = reservationTours.IsDeleted;
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationHotels)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (ReservationPaymentRelation.AmountTotal == null && reservationTours.NetPrice != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationTours.NetPrice;
                }
                ReservationPaymentRelation.AmountTotal = reservationTours.NetPrice;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                await _context.SaveChangesAsync();
            }
            _context.ReservationTours.Update(ReservationOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationTourId}")]
        public async Task<IActionResult> DeleteReservationTour(int ReservationTourId)
        {
            var Reservation = await _context.ReservationTours.FirstOrDefaultAsync(
                f => f.ReservationTourId == ReservationTourId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationTours.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
