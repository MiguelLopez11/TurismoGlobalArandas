using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvidersController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ProvidersController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Providers>> getProviders()
        {
            var providers = await _context.Providers
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(providers);
        }
        [HttpGet("{ProviderId}")]
        public async Task<ActionResult> getProviderById(int ProviderId)
        {
            var provider = await _context.Providers
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.ProviderId == ProviderId);
            if (provider == null)
            {
                return NotFound();
            }
            return Ok(provider);
        }
        [HttpPost]
        public async Task<ActionResult<Providers>> PostProvider(Providers Provider)
        {
            _context.Providers.Add(Provider);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getProviderById", new { ProviderId = Provider.ProviderId }, Provider);
        }
        [HttpPut("{ProviderId}")]
        public async Task<ActionResult> PutProvider(int ProviderId, Providers Provider)
        {
            if (Provider.ProviderId != ProviderId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ProviderOld = await _context.Providers.FirstOrDefaultAsync(f => f.ProviderId == ProviderId);
            if (ProviderOld == null)
            {
                return BadRequest($"El cliente con el ID {ProviderId} no existe");
            }
            ProviderOld.ProviderId = Provider.ProviderId;
            ProviderOld.Name = Provider.Name;
            ProviderOld.Email = Provider.Email;
            ProviderOld.PhoneNumber = Provider.PhoneNumber;
            ProviderOld.IsDeleted = Provider.IsDeleted;

            _context.Providers.Update(ProviderOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{ProviderId}")]
        public async Task<IActionResult> DeleteProvider(int ProviderId)
        {
            var provider = await _context.Providers
                .FirstOrDefaultAsync(f => f.ProviderId == ProviderId);
            if (provider == null)
            {
                return NotFound();
            }

            provider.IsDeleted = true;
            _context.Providers.Update(provider);
            await _context.SaveChangesAsync();
            return Ok("Proveedor archivado");
        }
    }
}
