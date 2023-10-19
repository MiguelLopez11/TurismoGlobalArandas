using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentConceptsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public PaymentConceptsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<PaymentConcept>> getPaymentConcepts()
        {
            var concepts = await _context.PaymentConcepts
                .ToListAsync();
            return Ok(concepts);
        }
        [HttpGet("{PaymentConceptId}")]
        public async Task<ActionResult> getPaymentConceptById(int PaymentConceptId)
        {
            var concept = await _context.PaymentConcepts
                .FirstOrDefaultAsync(f => f.PaymentConceptId == PaymentConceptId);
            if (concept == null)
            {
                return NotFound();
            }
            return Ok(concept);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentConcept>> PostPaymentConcept(PaymentConcept concept)
        {
            _context.PaymentConcepts.Add(concept);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentConceptById", new { PaymentConceptId = concept.PaymentConceptId }, concept);
        }
        [HttpPut("{PaymentConceptId}")]
        public async Task<ActionResult> PutPaymentConcept(int PaymentConceptId, PaymentConcept concept)
        {
            if (concept.PaymentConceptId != PaymentConceptId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ConceptOld = await _context.PaymentConcepts
                .FirstOrDefaultAsync(f => f.PaymentConceptId == PaymentConceptId);

            if (ConceptOld == null)
            {
                return BadRequest($"La habitacion con el ID {PaymentConceptId} no existe");
            }
            ConceptOld.PaymentConceptId = concept.PaymentConceptId;
            ConceptOld.Name = concept.Name;
            ConceptOld.Description = concept.Description;
            ConceptOld.IsDeleted = concept.IsDeleted;

            _context.PaymentConcepts.Update(ConceptOld);
            await _context.SaveChangesAsync();
            return Ok("Concepto de pago actualizado correctamente");
        }
        [HttpDelete("{PaymentConceptId}")]
        public async Task<IActionResult> DeletePaymentConcept(int PaymentConceptId)
        {
            var concept = await _context.PaymentConcepts
                .FirstOrDefaultAsync(f => f.PaymentConceptId == PaymentConceptId);
            if (concept == null)
            {
                return NotFound();
            }
            concept.IsDeleted = true;
            _context.PaymentConcepts.Update(concept);
            await _context.SaveChangesAsync();
            return Ok("Concepto de pago eliminado");
        }
    }
}
