using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public RoutesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Routes>> getRoutes()
        {
            var routes = await _context.Routes
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(routes);
        }
        [HttpGet("{RouteId}")]
        public async Task<ActionResult> getRouteById(int RouteId)
        {
            var route = await _context.Routes
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.RouteId == RouteId);
            if (route == null)
            {
                return NotFound();
            }
            return Ok(route);
        }
        [HttpPost]
        public async Task<ActionResult<Routes>> PostRoute(Routes route)
        {
            _context.Routes.Add(route);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getRouteById", new { RouteId = route.RouteId }, route);
        }
        [HttpPut("{RouteId}")]
        public async Task<ActionResult> PutRoute(int RouteId, Routes route)
        {
            if (route.RouteId != RouteId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var RouteOld = await _context.Routes
                .FirstOrDefaultAsync(f => f.RouteId == RouteId);

            if (RouteOld == null)
            {
                return BadRequest($"La habitacion con el ID {RouteId} no existe");
            }
            RouteOld.RouteId = route.RouteId;
            RouteOld.RouteName = route.RouteName;
            RouteOld.Description = route.Description;
            RouteOld.IsDeleted = route.IsDeleted;

            _context.Routes.Update(RouteOld);
            await _context.SaveChangesAsync();
            return Ok("Registro actualizado correctamente");
        }
        [HttpDelete("{RouteId}")]
        public async Task<IActionResult> DeleteRoute(int RouteId)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(f => f.RouteId == RouteId);
            if (route == null)
            {
                return NotFound();
            }
            route.IsDeleted = true;
            _context.Routes.Update(route);
            await _context.SaveChangesAsync();
            return Ok("Registro de pago eliminado");
        }
    }
}
