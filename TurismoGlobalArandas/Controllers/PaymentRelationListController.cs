using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentRelationListController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public PaymentRelationListController(TurismoGlobalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentRelationList>> getPaymentsRelationList()
        {
            var payments = await _context.PaymentRelationLists
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(payments);
        }
        [HttpGet("{PaymentId}")]
        public async Task<ActionResult> getPaymentRelationList(int PaymentId)
        {
            var customer = await _context.PaymentRelationLists
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentRelationList>> PostPaymentRelationList(PaymentRelationList payment)
        {
            _context.PaymentRelationLists.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentRelationList", new { PaymentId = payment.PaymentId }, payment);
        }
        [HttpPut("{PaymentId}")]
        public async Task<ActionResult> PutPaymentRelationList(int PaymentId, PaymentRelationList payment)
        {
            if (payment.PaymentId != PaymentId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentRelationLists.FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (paymentOld == null)
            {
                return BadRequest($"El cliente con el ID {PaymentId} no existe");
            }
            paymentOld.PaymentId = payment.PaymentId;
            paymentOld.Invoice = payment.Invoice;
            paymentOld.Amount = payment.Amount;
            paymentOld.PaymentDate = payment.PaymentDate;
            paymentOld.Observations = payment.Observations;
            paymentOld.PaymentReservationHotelId = payment.PaymentReservationHotelId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentRelationLists.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{PaymentId}")]
        public async Task<IActionResult> DeletePaymentRelationList(int PaymentId)
        {
            var payment = await _context.PaymentRelationLists
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentRelationLists.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Registro archivado");
        }
    }
}
