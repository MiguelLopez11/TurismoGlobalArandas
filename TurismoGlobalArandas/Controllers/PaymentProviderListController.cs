using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentProviderListController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;
        public PaymentProviderListController(TurismoGlobalContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<PaymentProviderList>> getPaymentsProviderList()
        {
            var payments = await _context.PaymentProviderLists
                .ToListAsync();
            return Ok(payments);
        }
        [HttpGet("{PaymentId}")]
        public async Task<ActionResult> getPaymentProviderList(int PaymentId)
        {
            var payment = await _context.PaymentProviderLists
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }
        [HttpGet("PaymentsProvider/{PaymentProviderId}")]
        public async Task<ActionResult> getPaymentProviderListByPaymentReservationHotel(int PaymentProviderId)
        {
            #region comprobar monto faltante en reservación
            var payments = await _context.PaymentProviderLists
                .Include(i => i.PaymentConcept)
                .Include(i => i.PaymentMethods)
                .Where(w => w.PaymentProviderId == PaymentProviderId)
                .Where(w => !w.IsDeleted)
                .ToListAsync();
            decimal? total = 0;
            foreach (var payment in payments)
            {
                total += payment.Amount;
            }
            var paymentProvider = await _context.PaymentProviders
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(f => f.PaymentProviderId == PaymentProviderId);
            if (paymentProvider == null)
            {
                return BadRequest();
            }
            #endregion
            #region tomar todas las reservaciones
            decimal? AmountMissing = paymentProvider.AmountTotal - total;
            paymentProvider.AmountMissing = AmountMissing;
            //RESERVACION HOTELERIA
            var reservationHotel = await _context.ReservationHotels.
                        FirstOrDefaultAsync(f => f.ReservationHotelId == paymentProvider.ReservationHotelId);
            //RESERVACION VUELOS
            var reservationFlight = await _context.ReservationFlights.
                        FirstOrDefaultAsync(f => f.FlightId == paymentProvider.ReservationFlightId);
            //RESERVACIONES TOURS
            var reservationTour = await _context.ReservationTours.
                        FirstOrDefaultAsync(f => f.ReservationTourId == paymentProvider.ReservationTourId);
            //RESERVACIONES VEHICULOS
            var reservationVehicle = await _context.ReservationVehicles.
                        FirstOrDefaultAsync(f => f.ReservationVehicleId == paymentProvider.ReservationVehicleId);
            #endregion 
            if (paymentProvider.AmountMissing == 0)
            {
                if (paymentProvider.ReservationHotelId != null)
                {
                    reservationHotel.IsSoldOut = true;
                    _context.ReservationHotels.Update(reservationHotel);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationFlightId != null)
                {

                    reservationFlight.IsSoldOut = true;
                    _context.ReservationFlights.Update(reservationFlight);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationTourId != null)
                {

                    reservationTour.IsSoldOut = true;
                    _context.ReservationTours.Update(reservationTour);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationVehicleId != null)
                {

                    reservationVehicle.IsSoldOut = true;
                    _context.ReservationVehicles.Update(reservationVehicle);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                if (paymentProvider.ReservationHotelId != null)
                {
                    reservationHotel.IsSoldOut = false;
                    _context.ReservationHotels.Update(reservationHotel);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationFlightId != null)
                {

                    reservationFlight.IsSoldOut = false;
                    _context.ReservationFlights.Update(reservationFlight);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationTourId != null)
                {

                    reservationTour.IsSoldOut = false;
                    _context.ReservationTours.Update(reservationTour);
                    await _context.SaveChangesAsync();
                }
                if (paymentProvider.ReservationVehicleId != null)
                {

                    reservationVehicle.IsSoldOut = false;
                    _context.ReservationVehicles.Update(reservationVehicle);
                    await _context.SaveChangesAsync();
                }
            }
            _context.PaymentProviders.Update(paymentProvider);
            await _context.SaveChangesAsync();
            return Ok(payments);
        }
        [HttpPost]
        public async Task<ActionResult<PaymentProviderList>> PostPaymentProviderList(PaymentProviderList payment)
        {
            payment.PaymentDate = DateTime.Now;
            payment.Invoice = _context.GetInvoicePaymentRelationList();
            _context.PaymentProviderLists.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("getPaymentProviderList", new { PaymentId = payment.PaymentId }, payment);
        }
        [HttpPut("{PaymentId}")]
        public async Task<ActionResult> PutPaymentRelationList(int PaymentId, PaymentProviderList payment)
        {
            if (payment.PaymentId != PaymentId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var paymentOld = await _context.PaymentProviderLists.FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (paymentOld == null)
            {
                return BadRequest($"El cliente con el ID {PaymentId} no existe");
            }
            paymentOld.PaymentId = payment.PaymentId;
            paymentOld.Invoice = payment.Invoice;
            paymentOld.Amount = payment.Amount;
            paymentOld.PaymentDate = payment.PaymentDate;
            paymentOld.Observations = payment.Observations;
            paymentOld.PaymentMethodId = payment.PaymentMethodId;
            paymentOld.PaymentConceptId = payment.PaymentConceptId;
            paymentOld.PaymentProviderId = payment.PaymentProviderId;
            paymentOld.IsDeleted = payment.IsDeleted;

            _context.PaymentProviderLists.Update(paymentOld);
            await _context.SaveChangesAsync();
            return Ok("El cliente se actualizo correctamente");
        }
        [HttpDelete("{PaymentId}")]
        public async Task<IActionResult> DeletePaymentRelationList(int PaymentId)
        {
            var payment = await _context.PaymentProviderLists
                .FirstOrDefaultAsync(f => f.PaymentId == PaymentId);
            if (payment == null)
            {
                return NotFound();
            }

            payment.IsDeleted = true;
            _context.PaymentProviderLists.Update(payment);
            await _context.SaveChangesAsync();
            return Ok("Registro archivado");
        }
    }
}
