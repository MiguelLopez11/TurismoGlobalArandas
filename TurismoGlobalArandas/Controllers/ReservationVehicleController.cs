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
                .ToListAsync();
            return Ok(reservationVehicles);
        }
        [HttpGet("{ReservationVehicleId}")]
        public async Task<ActionResult> getReservationVehicle(int ReservationVehicleId)
        {
            var reservationVehicle = await _context.ReservationVehicles
                .Include(i => i.Providers)
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
            reservationVehicle.Invoice = _context.GetInvoiceReservationVehicles();
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
            ReservationOld.CustomerId = reservationVehicle.CustomerId;
            ReservationOld.DateSale = reservationVehicle.DateSale;
            ReservationOld.DateTravel = reservationVehicle.DateTravel;
            ReservationOld.PaymentLimitDate = reservationVehicle.PaymentLimitDate;
            ReservationOld.PaymentLimitDateProvider = reservationVehicle.PaymentLimitDateProvider;
            ReservationOld.DepartureLocation = reservationVehicle.DepartureLocation;
            ReservationOld.ArrivalLocation = reservationVehicle.ArrivalLocation;
            ReservationOld.ProviderId = reservationVehicle.ProviderId;
            ReservationOld.PricePublic = reservationVehicle.PricePublic;
            ReservationOld.PriceNeto = reservationVehicle.PriceNeto;
            ReservationOld.Description = reservationVehicle.Description;
            ReservationOld.IsSoldOut = reservationVehicle.IsSoldOut;
            ReservationOld.IsDeleted = reservationVehicle.IsDeleted;
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationVehicles)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            var PaymentProvider = await _context.PaymentProviders
                .Include(i => i.ReservationVehicles)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (ReservationPaymentRelation.AmountTotal == null && PaymentProvider.AmountTotal == null &&  reservationVehicle.PriceNeto != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null && PaymentProvider.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationVehicle.PriceNeto;
                    PaymentProvider.AmountMissing = reservationVehicle.PriceNeto;
                }
                ReservationPaymentRelation.AmountTotal = reservationVehicle.PriceNeto;
                PaymentProvider.AmountTotal = reservationVehicle.PriceNeto;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                _context.PaymentProviders.Update(PaymentProvider);
                await _context.SaveChangesAsync();
            }
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
