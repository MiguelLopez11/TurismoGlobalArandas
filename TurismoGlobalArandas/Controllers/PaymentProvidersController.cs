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
                .ToListAsync();
            return Ok(Payments);
        }
        [HttpGet("{PaymentId}")]
        public async Task<ActionResult> getPaymentProvider(int PaymentId)
        {
            var Payment = await _context.PaymentProviders
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (Payment == null)
            {
                return NotFound();
            }
            return Ok(Payment);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentProviders>> PostPaymentProvider(PaymentProviders payment)
        {
            _context.PaymentProviders.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentProvider", new { PaymentId = payment.PaymentId }, payment);
        }
        [HttpPut("{PaymentId}")]
        public async Task<ActionResult> PutPaymentProvider(int PaymentId, PaymentProviders payment)
        {
            if (payment.PaymentId != PaymentId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentProviders
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (paymentOld == null)
            {
                return BadRequest($"El pago con el ID {PaymentId} no existe");
            }
            paymentOld.PaymentId = payment.PaymentId;
            paymentOld.Invoice = payment.Invoice;
            paymentOld.PaymentDate = payment.PaymentDate;
            paymentOld.ReservationHotelId = payment.ReservationHotelId;
            paymentOld.ReservationTourId = payment.ReservationTourId;
            paymentOld.ReservationVehicleId = payment.ReservationVehicleId;
            paymentOld.Observations = payment.Observations;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentProviders.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{PaymentId}")]
        public async Task<IActionResult> DeletePaymentProvider(int PaymentId)
        {
            var payment = await _context.PaymentProviders
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentProviders.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Cliente archivado");
        }
    }
}
