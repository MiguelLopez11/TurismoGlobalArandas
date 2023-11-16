using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class paymentRelationReservationsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public paymentRelationReservationsController(TurismoGlobalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentsRelationReservations>> getPaymentsRelationReservation()
        {
            var Payments = await _context.PaymentsRelationReservations
                .ToListAsync();
            return Ok(Payments);
        }
        [HttpGet("{PaymentReservationId}")]
        public async Task<ActionResult> getPaymentRelationReservation(int PaymentReservationId)
        {
            var Payment = await _context.PaymentsRelationReservations
                .FirstOrDefaultAsync(f => f.PaymentReservationId == PaymentReservationId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<ActionResult> getPaymentRelationByReservationHotel(int ReservationHotelId)
        {
            var Payment = await _context.PaymentsRelationReservations
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpGet("ReservationTour/{ReservationTourId}")]
        public async Task<ActionResult> getPaymentRelationByReservationTour(int ReservationTourId)
        {
            var Payment = await _context.PaymentsRelationReservations
                .FirstOrDefaultAsync(f => f.ReservationTourId == ReservationTourId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpGet("ReservationVehicle/{ReservationVehicleId}")]
        public async Task<ActionResult> getPaymentRelationByReservationVehicle(int ReservationVehicleId)
        {
            var Payment = await _context.PaymentsRelationReservations
                .FirstOrDefaultAsync(f => f.ReservationVehicleId == ReservationVehicleId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpGet("ReservationFlight/{ReservationFlightId}")]
        public async Task<ActionResult> getPaymentRelationByReservationFlight(int ReservationFlightId)
        {
            var Payment = await _context.PaymentsRelationReservations
                .FirstOrDefaultAsync(f => f.ReservationFlightId == ReservationFlightId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentsRelationReservations>> PostPaymentRelationReservationHotel(PaymentsRelationReservations payment)
        {
            _context.PaymentsRelationReservations.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentRelationReservation", new { PaymentReservationId = payment.PaymentReservationId }, payment);
        }
        [HttpPut("{PaymentReservationId}")]
        public async Task<ActionResult> PutPaymentRelationReservationHotel(int PaymentReservationId, PaymentsRelationReservations payment)
        {
            if (payment.PaymentReservationId != PaymentReservationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentsRelationReservations.FirstOrDefaultAsync(f => f.PaymentReservationId == PaymentReservationId);
            if (paymentOld == null)
            {
                return BadRequest($"El cliente con el ID {PaymentReservationId} no existe");
            }
            paymentOld.PaymentReservationId = payment.PaymentReservationId;
            paymentOld.AmountTotal = payment.AmountTotal;
            paymentOld.AmountMissing = payment.AmountMissing;
            paymentOld.ReservationHotelId = payment.ReservationHotelId;
            paymentOld.ReservationTourId = payment.ReservationTourId;
            paymentOld.ReservationVehicleId = payment.ReservationVehicleId;
            paymentOld.ReservationFlightId = payment.ReservationFlightId;
            paymentOld.StatusPaymentRelationId = payment.StatusPaymentRelationId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentsRelationReservations.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{PaymentReservationId}")]
        public async Task<IActionResult> DeletePaymentRelationReservationHotel(int PaymentReservationId)
        {
            var payment = await _context.PaymentsRelationReservations
                .FirstOrDefaultAsync(f => f.PaymentReservationId == PaymentReservationId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentsRelationReservations.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Cliente archivado");
        }
    }
}
