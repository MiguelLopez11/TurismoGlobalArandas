using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AditionalServicesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public AditionalServicesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<AditionalServices>> getAditionalServices()
        {
            var aditionalServices = await _context.AditionalServices
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(aditionalServices);
        }
        [HttpGet("{AditionalServiceId}")]
        public async Task<ActionResult> getAditionalServiceById(int AditionalServiceId)
        {
            var aditionalService = await _context.AditionalServices
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.AditionalServiceId == AditionalServiceId);
            if (aditionalService == null)
            {
                return NotFound();
            }
            return Ok(aditionalService);
        }
        [HttpPost]
        public async Task<ActionResult<AditionalServices>> PostAditionalService(AditionalServices aditionalService)
        {
            _context.AditionalServices.Add(aditionalService);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getAditionalServiceById", new { AditionalServiceId = aditionalService.AditionalServiceId }, aditionalService);
        }
        [HttpPut("{AditionalServiceId}")]
        public async Task<ActionResult> PutAditionalService(int AditionalServiceId, AditionalServices aditionalService)
        {
            if (aditionalService.AditionalServiceId != AditionalServiceId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var AditionalServiceOld = await _context.AditionalServices.FirstOrDefaultAsync(f => f.AditionalServiceId == AditionalServiceId);
            if (AditionalServiceOld == null)
            {
                return BadRequest($"El cliente con el ID {AditionalServiceId} no existe");
            }
            AditionalServiceOld.AditionalServiceId = aditionalService.AditionalServiceId;
            AditionalServiceOld.Name = aditionalService.Name;
            AditionalServiceOld.Cost = aditionalService.Cost;
            AditionalServiceOld.Description = aditionalService.Description;
            AditionalServiceOld.IsDeleted = aditionalService.IsDeleted;

            _context.AditionalServices.Update(AditionalServiceOld);
            await _context.SaveChangesAsync();
            return Ok("El Servicio adicional se actualizo correctamente");
        }
        [HttpDelete("{AditionalServiceId}")]
        public async Task<IActionResult> DeleteAditionalService(int AditionalServiceId)
        {
            var aditionalService = await _context.AditionalServices
                .FirstOrDefaultAsync(f => f.AditionalServiceId == AditionalServiceId);
            if (aditionalService == null)
            {
                return NotFound();
            }

            aditionalService.IsDeleted = true;
            _context.AditionalServices.Update(aditionalService);
            await _context.SaveChangesAsync();
            return Ok("Servicio adicional archivado");
        }
    }
}
