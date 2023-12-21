using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderServicesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ProviderServicesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ServicesProvider>> getServices()
        {
            var services = await _context.ServicesProviders
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(services);
        }

        [HttpGet("{ServiceId}")]
        public async Task<ActionResult> getServiceById(int ServiceId)
        {
            var service = await _context.ServicesProviders
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ServiceId == ServiceId);
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpGet("Provider/{ProviderId}")]
        public async Task<ActionResult> getServiceByProvider(int ProviderId)
        {
            var Commission = await _context.ServicesProviders
                .Include(i => i.Providers)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ProviderId == ProviderId);
            if (Commission == null)
            {
                return NotFound();
            }
            return Ok(Commission);
        }

        [HttpPost]
        public async Task<ActionResult<ServicesProvider>> PostService(
            ServicesProvider serviceProvider
        )
        {
            _context.ServicesProviders.Add(serviceProvider);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getServiceByProvider",
                new { ProviderId = serviceProvider.ProviderId },
                serviceProvider
            );
        }

        [HttpPut("{ServiceId}")]
        public async Task<ActionResult> PutService(int ServiceId, ServicesProvider service)
        {
            if (service.ServiceId != ServiceId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ServiceOld = await _context.ServicesProviders.FirstOrDefaultAsync(
                f => f.ServiceId == ServiceId
            );
            if (ServiceOld == null)
            {
                return BadRequest($"La habitacion con el ID {ServiceId} no existe");
            }
            ServiceOld.ServiceId = service.ServiceId;
            ServiceOld.Name = service.Name;
            ServiceOld.Color = service.Color;
            ServiceOld.CommissionAgency = service.CommissionAgency;
            ServiceOld.CommissionClient = service.CommissionClient;
            ServiceOld.CommissionEmployee = service.CommissionEmployee;
            ServiceOld.Description = service.Description;
            ServiceOld.IsDeleted = service.IsDeleted;

            _context.ServicesProviders.Update(ServiceOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{ServiceId}")]
        public async Task<IActionResult> DeleteDiscountProvider(int ServiceId)
        {
            var service = await _context.ServicesProviders.FirstOrDefaultAsync(
                f => f.ServiceId == ServiceId
            );
            if (service == null)
            {
                return NotFound();
            }

            service.IsDeleted = true;
            _context.ServicesProviders.Update(service);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
