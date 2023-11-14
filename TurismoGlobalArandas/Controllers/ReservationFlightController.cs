using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationFlightController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public ReservationFlightController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationFlight>> getReservationFlights()
        {
            var flights = await _context.ReservationFlights
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            return Ok(flights);
        }
        [HttpGet("{FlightId}")]
        public async Task<ActionResult> getReservationFlightById(int FlightId)
        {
            var flight = await _context.ReservationFlights
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.FlightId == FlightId);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }
        [HttpPost]
        public async Task<ActionResult<ReservationFlight>> PostReservationFlight(ReservationFlight flight)
        {
            flight.DateSale = DateTime.Now;
            _context.ReservationFlights.Add(flight);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getReservationFlightById", new { FlightId = flight.FlightId }, flight);
        }
        [HttpPut("{FlightId}")]
        public async Task<ActionResult> PutReservationFlight(int FlightId, ReservationFlight reservationFlight)
        {
            if (reservationFlight.FlightId != FlightId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var FlightOld = await _context.ReservationFlights.FirstOrDefaultAsync(f => f.FlightId == FlightId);
            if (FlightOld == null)
            {
                return BadRequest($"El vuelo con el ID {FlightId} no existe");
            }
            FlightOld.FlightId = reservationFlight.FlightId;
            FlightOld.Invoice = reservationFlight.Invoice;
            FlightOld.DateTravel = reservationFlight.DateTravel;
            FlightOld.DateSale = reservationFlight.DateSale;
            FlightOld.DepartureAirport = reservationFlight.DepartureAirport;
            FlightOld.ArrivalAirport = reservationFlight.ArrivalAirport;
            FlightOld.CustomerId = reservationFlight.CustomerId;
            FlightOld.Airline = reservationFlight.Airline;
            FlightOld.ConfirmationKey = reservationFlight.ConfirmationKey;
            FlightOld.PriceNeto = reservationFlight.PriceNeto;
            FlightOld.PricePublic = reservationFlight.PricePublic;
            FlightOld.isSimpleFlight = reservationFlight.isSimpleFlight;
            FlightOld.isRoundFlight = reservationFlight.isRoundFlight;
            FlightOld.isMultidestinationFlight = reservationFlight.isMultidestinationFlight;
            FlightOld.PaymentMethodAgency = reservationFlight.PaymentMethodAgency;
            FlightOld.PaymentMethodClient = reservationFlight.PaymentMethodClient;
            FlightOld.ContactPhone = reservationFlight.ContactPhone;
            FlightOld.IsSoldOut = reservationFlight.IsSoldOut;
            FlightOld.IsDeleted = reservationFlight.IsDeleted;
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.reservationFlight)
                .FirstOrDefaultAsync(f => f.ReservationFlightId == FlightId);
            if (ReservationPaymentRelation.AmountTotal == null && reservationFlight.PriceNeto != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationFlight.PriceNeto;
                }
                ReservationPaymentRelation.AmountTotal = reservationFlight.PriceNeto;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                await _context.SaveChangesAsync();
            }
            _context.ReservationFlights.Update(FlightOld);
            await _context.SaveChangesAsync();
            return Ok("El vuelo se actualizo correctamente");
        }
        [HttpDelete("{FlightId}")]
        public async Task<IActionResult> DeleteReservationFlight(int FlightId)
        {
            var flight = await _context.ReservationFlights
                .FirstOrDefaultAsync(f => f.FlightId == FlightId);
            if (flight == null)
            {
                return NotFound();
            }

            flight.IsDeleted = true;
            _context.ReservationFlights.Update(flight);
            await _context.SaveChangesAsync();
            return Ok("Cliente archivado");
        }
    }
}
