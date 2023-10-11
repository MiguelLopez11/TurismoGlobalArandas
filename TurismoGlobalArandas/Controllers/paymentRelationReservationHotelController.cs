 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class paymentRelationReservationHotelController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public paymentRelationReservationHotelController(TurismoGlobalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentsRelationReservationHotels>> getPaymentsRelationReservationHotel()
        {
            var Payments = await _context.PaymentsRelationReservationHotels
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(Payments);
        }
        [HttpGet("{PaymentReservationHotelId}")]
        public async Task<ActionResult> getPaymentRelationReservationHotel(int PaymentReservationHotelId)
        {
            var Payment = await _context.PaymentsRelationReservationHotels
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.PaymentReservationHotelId == PaymentReservationHotelId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<ActionResult> getPaymentRelationReservationHotelByReservationHotel(int ReservationHotelId)
        {
            var Payment = await _context.PaymentsRelationReservationHotels
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentsRelationReservationHotels>> PostPaymentRelationReservationHotel(PaymentsRelationReservationHotels payment)
        {
            _context.PaymentsRelationReservationHotels.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentRelationReservationHotels", new { PaymentReservationHotelId = payment.PaymentReservationHotelId }, payment);
        }
        [HttpPut("{PaymentReservationHotelId}")]
        public async Task<ActionResult> PutPaymentRelationReservationHotel(int PaymentReservationHotelId, PaymentsRelationReservationHotels payment)
        {
            if (payment.PaymentReservationHotelId != PaymentReservationHotelId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentsRelationReservationHotels.FirstOrDefaultAsync(f => f.PaymentReservationHotelId == PaymentReservationHotelId);
            if (paymentOld == null)
            {
                return BadRequest($"El cliente con el ID {PaymentReservationHotelId} no existe");
            }
            paymentOld.PaymentReservationHotelId = payment.PaymentReservationHotelId;
            paymentOld.AmountTotal = payment.AmountTotal;
            paymentOld.AmountMissing = payment.AmountMissing;
            paymentOld.ReservationHotelId = payment.ReservationHotelId;
            paymentOld.StatusPaymentRelationId = payment.StatusPaymentRelationId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentsRelationReservationHotels.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{PaymentReservationHotelId}")]
        public async Task<IActionResult> DeletePaymentRelationReservationHotel(int PaymentReservationHotelId)
        {
            var payment = await _context.PaymentsRelationReservationHotels
                .FirstOrDefaultAsync(f => f.PaymentReservationHotelId == PaymentReservationHotelId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentsRelationReservationHotels.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Cliente archivado");
        }
    }
}
