using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProvidersController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public PaymentProvidersController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentProviders>> getPaymentProviders()
        {
            var Payments = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .ToListAsync();
            return Ok(Payments);
        }

        [HttpGet("{PaymentProviderId}")]
        public async Task<ActionResult> getPaymentProvider(int PaymentProviderId)
        {
            var Payment = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.PaymentProviderId == PaymentProviderId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }

        [HttpGet("ReservacionHotel/{ReservationHotelId}")]
        public async Task<ActionResult> getPaymentProviderByReservationHotel(int ReservationHotelId)
        {
            var Payment = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }

        [HttpGet("ReservacionTour/{ReservationTourId}")]
        public async Task<ActionResult> getPaymentProviderByReservationTour(int ReservationTourId)
        {
            var Payment = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }

        [Authorize]
        [HttpGet("ReservacionVuelo/{ReservationFlightId}")]
        public async Task<ActionResult> getPaymentProviderByReservationFlight(
            int ReservationFlightId
        )
        {
            var Payment = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationFlightId == ReservationFlightId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }

        [HttpGet("ReservacionVehiculo/{ReservationVehicleId}")]
        public async Task<ActionResult> getPaymentProviderByReservationVehicle(
            int ReservationVehicleId
        )
        {
            var Payment = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .Include(i => i.reservationFlight)
                .Include(i => i.ReservationVehicles)
                .Include(i => i.ReservationTours)
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentProviders>> PostPaymentProvider(
            PaymentProviders payment
        )
        {
            //var Invoice = _context.GetInvoice();
            //payment.Invoice = Invoice;
            //payment.PaymentDate = DateTime.Today;
            _context.PaymentProviders.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getPaymentProvider",
                new { PaymentProviderId = payment.PaymentProviderId },
                payment
            );
        }

        [HttpPut("{PaymentProviderId}")]
        public async Task<ActionResult> PutPaymentProvider(
            int PaymentProviderId,
            PaymentProviders payment
        )
        {
            if (payment.PaymentProviderId != PaymentProviderId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentProviders.FirstOrDefaultAsync(
                f => f.PaymentProviderId == PaymentProviderId
            );
            if (paymentOld == null)
            {
                return BadRequest($"El pago con el ID {PaymentProviderId} no existe");
            }
            paymentOld.PaymentProviderId = payment.PaymentProviderId;
            paymentOld.AmountTotal = payment.AmountTotal;
            paymentOld.AmountMissing = payment.AmountMissing;
            paymentOld.ReservationDate = payment.ReservationDate;
            paymentOld.ReservationHotelId = payment.ReservationHotelId;
            paymentOld.ReservationFlightId = payment.ReservationFlightId;
            paymentOld.ReservationTourId = payment.ReservationTourId;
            paymentOld.ReservationVehicleId = payment.ReservationVehicleId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentProviders.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }

        [HttpDelete("{PaymentProviderId}")]
        public async Task<IActionResult> DeletePaymentProvider(int PaymentProviderId)
        {
            var payment = await _context.PaymentProviders.FirstOrDefaultAsync(
                f => f.PaymentProviderId == PaymentProviderId
            );
            if (payment == null)
            {
                return NotFound();
            }
            var payments = await _context.PaymentProviderLists
                .Where(w => w.PaymentProviderId == PaymentProviderId)
                .ToListAsync();
            foreach (var item in payments)
            {
                item.IsDeleted = true;
                _context.PaymentProviderLists.Update(item);
                await _context.SaveChangesAsync();
            }
            payment.IsDeleted = true;
            _context.PaymentProviders.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("PAGO archivado");
        }
    }
}
