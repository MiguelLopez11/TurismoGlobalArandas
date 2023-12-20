using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationFlightDestinationsController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ReservationFlightDestinationsController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationFlightDestinations>> getReservationFlightDestinations()
        {
            var flightDestinations = await _context.ReservationFlightDestinations
                .ToListAsync();
            return Ok(flightDestinations);
        }
        [HttpGet("{ReservationFlightDestinationId}")]
        public async Task<ActionResult> getReservationFlightDestinationById(int ReservationFlightDestinationId)
        {
            var flightDestination = await _context.ReservationFlightDestinations
                .FirstOrDefaultAsync(f => f.ReservationFlightDestinationId == ReservationFlightDestinationId);
            if (flightDestination == null)
            {
                return NotFound();
            }
            return Ok(flightDestination);
        }
        [HttpGet("Flight/{FlightId}")]
        public async Task<ActionResult> getReservationFlightDestinationByFlightId(int FlightId)
        {
            var flightDestination = await _context.ReservationFlightDestinations
                .Where(w => w.ReservationFlightId == FlightId)
                .ToListAsync();
            
            return Ok(flightDestination);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationFlightDestinations>> PostReservationFlightDestination(ReservationFlightDestinations reservationFlightDestination)
        {
            _context.ReservationFlightDestinations.Add(reservationFlightDestination);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getReservationFlightDestinationById", new { ReservationFlightDestinationId = reservationFlightDestination.ReservationFlightDestinationId }, reservationFlightDestination);
        }
        [HttpPut("{ReservationFlightDestinationId}")]
        public async Task<ActionResult> PutAirline(int ReservationFlightDestinationId, ReservationFlightDestinations reservationFlightDestination)
        {
            if (reservationFlightDestination.ReservationFlightDestinationId != ReservationFlightDestinationId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var reservationFlightDestinationOld = await _context
                .ReservationFlightDestinations.FirstOrDefaultAsync(f => f.ReservationFlightDestinationId == ReservationFlightDestinationId);
            if (reservationFlightDestinationOld == null)
            {
                return BadRequest($"La aerolinea con el ID {ReservationFlightDestinationId} no existe");
            }
            reservationFlightDestinationOld.ReservationFlightDestinationId = reservationFlightDestination.ReservationFlightDestinationId;
            reservationFlightDestinationOld.RouteDepartureAirportId = reservationFlightDestination.RouteDepartureAirportId;
            reservationFlightDestinationOld.RouteArrivalAirportId = reservationFlightDestination.RouteArrivalAirportId;
            reservationFlightDestinationOld.AirlineId = reservationFlightDestination.AirlineId;
            reservationFlightDestinationOld.DateTravel = reservationFlightDestination.DateTravel;

            _context.ReservationFlightDestinations.Update(reservationFlightDestinationOld);
            await _context.SaveChangesAsync();
            return Ok("La aerolinea se actualizo correctamente");
        }
        [HttpDelete("{ReservationFlightDestinationId}")]
        public async Task<IActionResult> DeleteReservationFlightDestination(int ReservationFlightDestinationId)
        {
            var flightDestination = await _context.ReservationFlightDestinations
                .FirstOrDefaultAsync(f => f.ReservationFlightDestinationId == ReservationFlightDestinationId);
            if (flightDestination == null)
            {
                return NotFound();
            }
            _context.ReservationFlightDestinations.Remove(flightDestination);
            await _context.SaveChangesAsync();
            return Ok("Servicio adicional archivado");
        }
    }
}
