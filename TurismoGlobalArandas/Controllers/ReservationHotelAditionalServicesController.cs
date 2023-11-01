using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelAditionalServicesController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationHotelAditionalServicesController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<
            ActionResult<ReservationHotelsServicesAditionals>
        > GetReservationHotelsServicesAditionals()
        {
            var AditionalServices = await _context.ReservationHotelsServicesAditionals
                .Include(i => i.AditionalServices)
                .Include(i => i.reservationHotel)
                .Include(i => i.ReservationFlight)
                .Include(i => i.ReservationVehicle)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(AditionalServices);
        }

        [HttpGet("ReservationHotel/{ReservationHotelId}")]
        public async Task<
            ActionResult<ReservationHotelsServicesAditionals>
        > GetReservationHotelsServicesAditionalHotelId(int ReservationHotelId)
        {
            var habitationsReservations = await _context.ReservationHotelsServicesAditionals
                .Include(i => i.AditionalServices)
                .Include(i => i.reservationHotel)
                .Include(i => i.ReservationFlight)
                .Include(i => i.ReservationVehicle)
                .Where(w => w.ReservationHotelId == ReservationHotelId)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(habitationsReservations);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> getReservationHotelsServicesAditionalById(int Id)
        {
            var habitationsReservation = await _context.ReservationHotelsServicesAditionals
                .Include(i => i.AditionalServices)
                .Include(i => i.reservationHotel)
                .Include(i => i.ReservationFlight)
                .Include(i => i.ReservationVehicle)
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.Id == Id);
            if (habitationsReservation == null)
            {
                return NotFound();
            }
            return Ok(habitationsReservation);
        }

        [HttpPost]
        public async Task<
            ActionResult<ReservationHotelsServicesAditionals>
        > PostReservationHotelsServicesAditional(
            ReservationHotelsServicesAditionals habitationsReservation
        )
        {
            _context.ReservationHotelsServicesAditionals.Add(habitationsReservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationHotelsServicesAditionalById",
                new { Id = habitationsReservation.Id },
                habitationsReservation
            );
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult> PutReservationHotelsServicesAditional(
            int Id,
            ReservationHotelsServicesAditionals reservationsServicesAditionals
        )
        {
            if (reservationsServicesAditionals.Id != Id)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var reservationsServicesAditionalsOld =
                await _context.ReservationHotelsServicesAditionals.FirstOrDefaultAsync(
                    f => f.Id == Id
                );

            if (reservationsServicesAditionalsOld == null)
            {
                return BadRequest($"La habitacion con el ID {Id} no existe");
            }
            reservationsServicesAditionalsOld.AditionalServiceId =
                reservationsServicesAditionals.AditionalServiceId;
            reservationsServicesAditionalsOld.ReservationHotelId =
                reservationsServicesAditionals.ReservationHotelId;
            reservationsServicesAditionalsOld.IsDeleted = reservationsServicesAditionals.IsDeleted;

            _context.ReservationHotelsServicesAditionals.Update(reservationsServicesAditionalsOld);
            await _context.SaveChangesAsync();
            return Ok("Actualización correcta");
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteReservationHotelsServicesAditional(int Id)
        {
            var reservationService =
                await _context.ReservationHotelsServicesAditionals.FirstOrDefaultAsync(
                    f => f.Id == Id
                );
            if (reservationService == null)
            {
                return NotFound();
            }
            if (
                reservationService.AditionalServiceId == 1
                || reservationService.AditionalServiceId == 2
            )
            {
                switch (reservationService.AditionalServiceId)
                {
                    case 1:
                        var flight = await _context.ReservationFlights
                            .Where(w => !w.IsDeleted)
                            .FirstOrDefaultAsync(
                                f => f.FlightId == reservationService.ReservationFlightId
                            );
                        flight.IsDeleted = true;
                        _context.ReservationFlights.Update(flight);
                        await _context.SaveChangesAsync();
                        break;
                    case 2:
                        var Reservation = await _context.ReservationVehicles.FirstOrDefaultAsync(
                            f => f.ReservationVehicleId == reservationService.ReservationVehicleId
                        );
                        if (Reservation == null)
                        {
                            return NotFound();
                        }

                        Reservation.IsDeleted = true;
                        _context.ReservationVehicles.Update(Reservation);
                        await _context.SaveChangesAsync();
                        break;
                    default: return NotFound();
                }
            }
            reservationService.IsDeleted = true;
            _context.ReservationHotelsServicesAditionals.Update(reservationService);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }
    }
}
