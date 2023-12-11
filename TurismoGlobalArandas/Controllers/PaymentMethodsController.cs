using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public PaymentMethodsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentMethods>> getPaymentMethods()
        {
            var methods = await _context.PaymentMethods
                .ToListAsync();
            return Ok(methods);
        }
        [HttpGet("{PaymentMethodId}")]
        public async Task<ActionResult> getPaymentMethodById(int PaymentMethodId)
        {
            var method = await _context.PaymentMethods
                .FirstOrDefaultAsync(f => f.PaymentMethodId == PaymentMethodId);
            if (method == null)
            {
                return NotFound();
            }
            return Ok(method);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentMethods>> PostPaymentMethod(PaymentMethods method)
        {
            _context.PaymentMethods.Add(method);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentMethodById", new { PaymentMethodId = method.PaymentMethodId }, method);
        }
        [HttpPut("{PaymentMethodId}")]
        public async Task<ActionResult> PutPaymentMethod(int PaymentMethodId, PaymentMethods method)
        {
            if (method.PaymentMethodId != PaymentMethodId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var MethodOld = await _context.PaymentMethods
                .FirstOrDefaultAsync(f => f.PaymentMethodId == PaymentMethodId);

            if (MethodOld == null)
            {
                return BadRequest($"La habitacion con el ID {PaymentMethodId} no existe");
            }
            MethodOld.PaymentMethodId = method.PaymentMethodId;
            MethodOld.Name = method.Name;
            MethodOld.IsDeleted = method.IsDeleted;

            _context.PaymentMethods.Update(MethodOld);
            await _context.SaveChangesAsync();
            return Ok("metodo de pago actualizado correctamente");
        }
        [HttpDelete("{PaymentMethodId}")]
        public async Task<IActionResult> DeletePaymentMethod(int PaymentMethodId)
        {
            var method = await _context.PaymentMethods
                .FirstOrDefaultAsync(f => f.PaymentMethodId == PaymentMethodId);
            if (method == null)
            {
                return NotFound();
            }

            _context.PaymentMethods.Remove(method);
            await _context.SaveChangesAsync();
            return Ok("metodo de pago eliminado");
        }
    }
}
