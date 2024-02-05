using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using TurismoGlobalArandas.Context;
using TurismoGlobalArandas.Models;

namespace TurismoGlobalArandas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationHotelController : ControllerBase
    {
        private readonly TurismoGlobalContext _context;

        public ReservationHotelController(TurismoGlobalContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ReservationHotel>> getReservationsHotel()
        {
            var Reservations = await _context.ReservationHotels
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.TypeReservationGrupal)
                .Include(i => i.Destinations)
                .ToListAsync();
            return Ok(Reservations);
        }

        [HttpGet("{ReservationHotelId}")]
        public async Task<ActionResult> getReservationHotelById(int ReservationHotelId)
        {
            var Reservation = await _context.ReservationHotels
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.TypeReservationGrupal)
                .Include(i => i.Destinations)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (Reservation == null)
            {
                return NotFound();
            }
            return Ok(Reservation);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationHotel>> PostReservationHotel(
            ReservationHotel Reservation
        )
        {
            //REGISTER RESERVATION
            Reservation.Invoice = _context.GetInvoiceReservationHotel();
            Reservation.DateSale = DateTime.Now;
            _context.ReservationHotels.Add(Reservation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "getReservationHotelById",
                new { ReservationHotelId = Reservation.ReservationHotelId },
                Reservation
            );
        }

        [HttpPut("{ReservationHotelId}")]
        public async Task<ActionResult> PutReservationHotel(
            int ReservationHotelId,
            ReservationHotel Reservation
        )
        {
            if (Reservation.ReservationHotelId != ReservationHotelId)
            {
                return Ok("Los Id ingresados no coinciden");
            }
            var ReservationOld = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );

            if (ReservationOld == null)
            {
                return BadRequest($"La habitacion con el ID {ReservationHotelId} no existe");
            }
            if (
                Reservation.TypeReservationId == 1 && ReservationOld.TypeReservationId == 2
                || Reservation.TypeReservationId == 2
                    && ReservationOld.TypeReservationId == 2
                    && ReservationOld.TypeReservationGroupId == 1
                    && Reservation.TypeReservationGroupId == 2
            )
            {
                var reservationHotelGroup =
                    await _context.ReservationHotelGroups.FirstOrDefaultAsync(
                        f => f.ReservationHotelId == Reservation.ReservationHotelId
                    );
                if (reservationHotelGroup != null)
                {
                    var groupRates = await _context.GroupRates
                        .Where(
                            w =>
                                w.ReservationHotelGroupId
                                == reservationHotelGroup.ReservationHotelGroupId
                        )
                        .ToListAsync();
                    foreach (var item in groupRates)
                    {
                        _context.GroupRates.Remove(item);
                        await _context.SaveChangesAsync();
                    }
                    _context.ReservationHotelGroups.Remove(reservationHotelGroup);
                    await _context.SaveChangesAsync();
                }
            }
            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.ReservationHotels)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            var PaymentProvider = await _context.PaymentProviders
                .Include(i => i.ReservationHotels)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            decimal? actualAmountReceivedCustomer = 0.0m;
            decimal? actualAmountReceivedProvider = 0.0m;
            if (ReservationOld.TotalCost != Reservation.TotalCost)
            {
                #region actualizar relacion de pagos clientes

                var paymentList = await _context.PaymentRelationLists
                    .Where(
                        w =>
                            w.PaymentReservationId
                            == ReservationPaymentRelation.PaymentReservationId
                    )
                    .ToListAsync();
                foreach (var payment in paymentList)
                {
                    actualAmountReceivedCustomer += payment.Amount;
                }
                ReservationPaymentRelation.AmountTotal = Reservation.TotalCost;
                ReservationPaymentRelation.AmountMissing =
                    Reservation.TotalCost - actualAmountReceivedCustomer;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                await _context.SaveChangesAsync();
                #endregion
                #region actualizae relacion de pagos a proveedores
                var paymentProviderList = await _context.PaymentProviderLists
                    .Where(w => w.PaymentProviderId == PaymentProvider.PaymentProviderId)
                    .ToListAsync();
                foreach (var item in paymentProviderList)
                {
                    actualAmountReceivedProvider += item.Amount;
                }
                PaymentProvider.AmountTotal = Reservation.TotalCost;
                PaymentProvider.AmountMissing =
                    Reservation.TotalCost - actualAmountReceivedProvider;
                _context.PaymentProviders.Update(PaymentProvider);
                await _context.SaveChangesAsync();
                #endregion
            }
            ReservationOld.ReservationHotelId = Reservation.ReservationHotelId;
            ReservationOld.Invoice = Reservation.Invoice;
            ReservationOld.TravelDateStart = Reservation.TravelDateStart;
            ReservationOld.TravelDateEnd = Reservation.TravelDateEnd;
            ReservationOld.TypeHabitation = Reservation.TypeHabitation;
            ReservationOld.Agent = Reservation.Agent;
            ReservationOld.PaymentLimitDateProvider = Reservation.PaymentLimitDateProvider;
            ReservationOld.PhoneContact = Reservation.PhoneContact;
            ReservationOld.Observations = Reservation.Observations;
            ReservationOld.GroupCoordinator = Reservation.GroupCoordinator;
            ReservationOld.PhoneCoordinator = Reservation.PhoneCoordinator;
            ReservationOld.DateSale = Reservation.DateSale;
            ReservationOld.PaymentPeriod = Reservation.PaymentPeriod;
            ReservationOld.PaymentLimitDate = Reservation.PaymentLimitDate;
            ReservationOld.CancellationPolicy = Reservation.CancellationPolicy;
            ReservationOld.CodeVoicher = Reservation.CodeVoicher;
            ReservationOld.TotalCost = Reservation.TotalCost;
            ReservationOld.TypeReservationId = Reservation.TypeReservationId;
            ReservationOld.TypeReservationGroupId = Reservation.TypeReservationGroupId;
            ReservationOld.EmployeeId = Reservation.EmployeeId;
            ReservationOld.CustomerId = Reservation.CustomerId;
            ReservationOld.HotelId = Reservation.HotelId;
            ReservationOld.DestinationId = Reservation.DestinationId;
            ReservationOld.ProviderId = Reservation.ProviderId;
            ReservationOld.IsSoldOut = Reservation.IsSoldOut;
            ReservationOld.IsDeleted = Reservation.IsDeleted;

            if (
                ReservationPaymentRelation.AmountTotal == null
                && PaymentProvider.AmountTotal == null
                && Reservation.TotalCost != null
            )
            {
                if (
                    ReservationPaymentRelation.AmountMissing == null
                    && PaymentProvider.AmountMissing == null
                )
                {
                    ReservationPaymentRelation.AmountMissing = Reservation.TotalCost;
                    PaymentProvider.AmountMissing = Reservation.TotalCost;
                }
                ReservationPaymentRelation.AmountTotal = Reservation.TotalCost;
                PaymentProvider.AmountTotal = Reservation.TotalCost;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                _context.PaymentProviders.Update(PaymentProvider);
                await _context.SaveChangesAsync();
            }

            _context.ReservationHotels.Update(ReservationOld);
            await _context.SaveChangesAsync();

            return Ok("Actualización correcta");
        }

        [HttpDelete("{ReservationHotelId}")]
        public async Task<IActionResult> CancelReservationHotel(int ReservationHotelId)
        {
            var Reservation = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (Reservation == null)
            {
                return NotFound();
            }

            Reservation.IsDeleted = true;
            _context.ReservationHotels.Update(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro archivado");
        }

        [HttpDelete("Delete/{ReservationHotelId}")]
        public async Task<IActionResult> DeleteReservationHotel(int ReservationHotelId)
        {
            //RESERVACIÓN HOTELERIA
            var Reservation = await _context.ReservationHotels.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (Reservation == null)
            {
                return NotFound();
            }
            //IndividualRate
            var individualRate = await _context.IndividualRates.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (individualRate != null)
            {
                _context.IndividualRates.Remove(individualRate);
                await _context.SaveChangesAsync();
            }
            //RELACION DE PAGO CLIENTE
            var paymentRelation = await _context.PaymentsRelationReservations.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (paymentRelation != null)
            {
                //LISTA DE PAGOS CLIENTE
                var paymentRelationList = await _context.PaymentRelationLists
                    .Where(w => w.PaymentReservationId == paymentRelation.PaymentReservationId)
                    .ToListAsync();
                foreach (var item in paymentRelationList)
                {
                    _context.PaymentRelationLists.Remove(item);
                    await _context.SaveChangesAsync();
                }
                _context.PaymentsRelationReservations.Remove(paymentRelation);
            }

            //RESERVACION GRUPAL HOTELERIA
            var reservationHotelGroup = await _context.ReservationHotelGroups.FirstOrDefaultAsync(
                f => f.ReservationHotelId == ReservationHotelId
            );
            if (reservationHotelGroup != null)
            {
                var groupRates = await _context.GroupRates
                    .Where(
                        w =>
                            w.ReservationHotelGroupId
                            == reservationHotelGroup.ReservationHotelGroupId
                    )
                    .ToListAsync();
                foreach (var item in groupRates)
                {
                    _context.GroupRates.Remove(item);
                    await _context.SaveChangesAsync();
                }
                _context.ReservationHotelGroups.Remove(reservationHotelGroup);
            }
            //SERVICIOS ADICIONALES HOTELERIA
            var AditionalServices = await _context.ReservationHotelsServicesAditionals
                .Where(w => w.ReservationHotelId == ReservationHotelId)
                .ToListAsync();
            foreach (var item in AditionalServices)
            {
                _context.ReservationHotelsServicesAditionals.Remove(item);
            }
            //RELACION DE PAGOS A PROVEEDORES
            var PaymentProviders = await _context.PaymentProviders
                .Where(w => w.ReservationHotelId == ReservationHotelId)
                .ToListAsync();
            foreach (var item in PaymentProviders)
            {
                _context.PaymentProviders.Remove(item);
                await _context.SaveChangesAsync();
            }

            _context.ReservationHotels.Remove(Reservation);
            await _context.SaveChangesAsync();
            return Ok("registro eliminado");
        }

        #region CREATE PDF
        [HttpGet("DescargarDatosEnPDF/{ReservationHotelId}")]
        public async Task<ActionResult> DescargarDatosEnPDF(int ReservationHotelId)
        {
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var reservation = await _context.ReservationHotels
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.Hotels)
                .Include(i => i.TypeReservation)
                .Include(i => i.TypeReservationGrupal)
                .Include(i => i.Destinations)
                .Include(i => i.Providers)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            var No_Habitations = await _context.HabitationsReservations
                .Include(i => i.Habitations)
                .Include(i => i.reservationHotel)
                    .Where(f => f.ReservationHotelId == ReservationHotelId)
                    .CountAsync();
            var habitations = await _context.HabitationsReservations
                .Include(i => i.Habitations)
                .Include(i => i.reservationHotel)
                    .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            if (habitations == null)
            {
                return BadRequest("No se encuentran habitaciones registrados");
            }
            var IndividualRate = await _context.IndividualRates
                .Include(i => i.reservationHotel)
                .FirstOrDefaultAsync(f => f.ReservationHotelId == ReservationHotelId);
            var htmlContent = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n " +
                $"   <title>Bootstrap Grid Example</title>\r\n    <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n" +
                $"  </head>\r\n  <style>\r\n    #row_campos {{\r\n      gap: 5px;\r\n      border: 2px solid #000000;\r\n      padding: 5px;\r\n    }}\r\n    .custom-label {{\r\n      display: block;\r\n  " +
                $"    margin-top: 5px; /* Ajusta el espacio entre p y label */\r\n    }}\r\n\r\n    .custom-border {{\r\n      border: 1px solid #ccc;\r\n      padding: 5px; /* Ajusta el espacio dentro de las columnas */\r\n    }}\r\n    p {{\r\n      font-size: 13px;\r\n " +
                $"     font-family: 'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif;\r\n      text-align: justify;\r\n    }}\r\n    input {{\r\n      height: 30px;\r\n    }}\r\n    #Reservation_Type{{\r\n      height: 50px;\r\n    }}\r\n  </style>\r\n  <body>\r\n " +
                $"   <div class=\"container text-center\">\r\n      <div class=\"row p-1 justify-content-center\">\r\n        <div class=\"col-sm border p-2 text-white bg-secondary ml-5\">\r\n          <h6>Confirmación de Reserva</h6>\r\n        </div>\r\n " +
                $"       <div class=\"col-sm\">\r\n          <input\r\n          id=\"Reservation_Type\"\r\n            type=\"text\"\r\n            class=\"form-control custom-border\"\r\n            value=\"INDIVIDUAL\"\r\n            \r\n          />\r\n " +
                $"       </div>\r\n      </div>\r\n      <div id=\"row_campos\" class=\"row mt-3\">\r\n        <div class=\"col-sm text-center\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Nombre(s):</p>\r\n " +
                $"           </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Customers.Name}\"\r\n              />\r\n    " +
                $"        </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Apellidos:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Customers.Lastname}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n    " +
                $"    <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Destino:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n    " +
                $"          <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Destinations.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n      " +
                $"  <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Hotel:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n " +
                $"               class=\"form-control custom-border\"\r\n                value=\"{reservation.Hotels.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n " +
                $"         <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Fecha de viaje:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n    " +
                $"            class=\"form-control custom-border\"\r\n                value=\"{reservation.TravelDateStart + " - " + reservation.TravelDateEnd}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n      </div>\r\n   " +
                $"   <div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-5\">\r\n              <p class=\"b\">No. Habitacion:</p>\r\n            </div>\r\n    " +
                $"        <div class=\"col-7\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{No_Habitations}\"\r\n              />\r\n            </div>\r\n       " +
                $"   </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Adultos:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n        " +
                $"      <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{habitations.Habitations.Adults}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n\r\n    " +
                $"    <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Menores:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n           " +
                $"   <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{habitations.Habitations.Minors}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        \r\n     " +
                $"   <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Edades:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              " +
                $"<input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{habitations.Habitations.Ages}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n   " +
                $"     <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Tipo de Habitación:</p>\r\n            </div>\r\n  " +
                $"          <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{habitations.Habitations.TypeHabitation}\"\r\n              />\r\n            </div>\r\n  " +
                $"        </div>\r\n        </div>\r\n\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Telefono de contacto:</p>\r\n            </div>\r\n           " +
                $" <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.PhoneContact}\"\r\n              />\r\n            </div>\r\n          </div>\r\n " +
                $"       </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-2\">\r\n              <p class=\"b\">Observaciones:</p>\r\n            </div>\r\n " +
                $"           <div class=\"col-10\">\r\n              <textarea\r\n                class=\"form-control\"\r\n                style=\"height: 100px\"\r\n                value=\"holi\"\r\n              ></textarea>\r\n            </div>\r\n          </div>\r\n" +
                $"        </div>\r\n      </div>\r\n      <div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tarifa pública:</p>\r\n " +
                $"           </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{IndividualRate.PublicRate}\"\r\n              />\r\n            </div>\r\n" +
                $"          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-5\">\r\n              <p class=\"b\">Descuento Extra:</p>\r\n            </div>\r\n            <div class=\"col-7\">\r\n " +
                $"             <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{IndividualRate.ExtraDiscount}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n " +
                $"         <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p class=\"b\">Tarifa al cliente:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n  " +
                $"              class=\"form-control custom-border\"\r\n                value=\"{IndividualRate.ClientRate}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n " +
                $"       <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Fecha compra:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n      " +
                $"          class=\"form-control custom-border\"\r\n                value=\"{reservation.DateSale}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n " +
                $"             <p class=\"b\">Promotora:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Providers.Name}\"\r\n              />\r\n " +
                $"           </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Agente:</p>\r\n            </div>\r\n " +
                $"           <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Employees.Name + " " + reservation.Employees.Lastname}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n   " +
                $"     \r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p>Plazo de pago a cliente:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n     " +
                $"           class=\"form-control custom-border\"\r\n                value=\"{reservation.PaymentLimitDate}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-12\">\r\n          <div class=\"row\">\r\n " +
                $"           <div class=\"col-4\">\r\n              <p>Politicas de cancelación:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n " +
                $"               value=\"{reservation.CancellationPolicy}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-12\">\r\n          <div class=\"row\">\r\n            <div class=\"col-1\">\r\n              <p>Costo total:</p>\r\n          " +
                $"  </div>\r\n            <div class=\"col-11\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.TotalCost}\"\r\n              />\r\n            </div>\r\n          </div>\r\n  " +
                $"      </div>\r\n      </div>\r\n    </div>\r\n\r\n    <script src=\"https://code.jquery.com/jquery-3.2.1.slim.min.js\"></script>\r\n    <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js\"></script>\r\n    <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\"></script>\r\n  </body>\r\n</html>\r\n";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent);
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions
                {
                    Format = PaperFormat.Letter,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = "5px",
                        Right = "5px",
                        Bottom = "5px",
                        Left = "5px"
                    }
                }
            );
            return File(pdfContent, "application/pdf", "converted.pdf");
        }

        [HttpGet("DescargarReciboEnPDF")]
        public async Task<ActionResult> DescargarReciboEnPDF()
        {
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var htmlContent =
                "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n" +
                "    <link\r\n      rel=\"stylesheet\"\r\n      href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n  </head>\r\n  <style>\r\n" +
                "    #row-container {\r\n      border: 1px solid #3f3e3e;\r\n    }\r\n    hr {\r\n      color: #000000;\r\n      height: 0px;\r\n      border: 1px dashed;\r\n    }\r\n" +
                "  </style>\r\n  <body>\r\n    <div class=\"container-fluid text-center p-5\">\r\n      <div id=\"row-container\" class=\"container-fluid\">\r\n        <div class=\"row p-3\">\r\n" +
                "          <div class=\"col\">\r\n            <img src=\"/logo.jpg\" alt=\"imagen\" width=\"100\" height=\"100\" />\r\n          </div>\r\n          <div class=\"col\">\r\n" +
                "            <div class=\"row\">\r\n              <div class=\"col-12\">\r\n                <strong> RECIBO PROVISIONAL </strong>\r\n              </div>\r\n              <div class=\"col-12\">\r\n" +
                "                <label> Arandas, Jalisco a:</label>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <label>FOLIO: <strong>00001</strong></label>\r\n" +
                "            <div class=\"col-12\">\r\n              <strong>COPIA AGENCIA</strong>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"row p-5 border m-2\">\r\n" +
                "          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>Recibi de:</strong>\r\n              </div>\r\n " +
                "             <div class=\"col-8 border-bottom border-black\">\r\n                <span>Recibi</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n" +
                "          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>La cantidad de:</strong>\r\n " +
                "             </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n                <span>cantidad</span>\r\n              </div>\r\n            </div>\r\n" +
                "          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>Concepto:</strong>\r\n " +
                "             </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n                <span>concepto</span>\r\n              </div>\r\n            </div>\r\n" +
                "          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>Metodo de pago:</strong>\r\n " +
                "             </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n                <span>Metodo</span>\r\n              </div>\r\n            </div>\r\n" +
                "          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>Referencia de pago:</strong>\r\n" +
                "              </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n                <span>referencia</span>\r\n              </div>\r\n            </div>\r\n" +
                "          </div>\r\n        </div>\r\n        <div class=\"row m-5 text-center\">\r\n          <div class=\"col-12\">\r\n            <div class=\"col-12 border-top border-black\">\r\n " +
                "             <span>Firma de recibido</span>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n      <hr />\r\n      <div id=\"row-container\" class=\"container-fluid\">\r\n" +
                "        <div class=\"row p-3\">\r\n          <div class=\"col\">\r\n            <img src=\"/logo.jpg\" alt=\"imagen\" width=\"100\" height=\"100\" />\r\n          </div>\r\n          <div class=\"col\">\r\n " +
                "           <div class=\"row\">\r\n              <div class=\"col-12\">\r\n                <strong> RECIBO PROVISIONAL </strong>\r\n              </div>\r\n              <div class=\"col-12\">\r\n " +
                "               <label> Arandas, Jalisco a:</label>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <label>FOLIO: <strong>00001</strong></label>\r\n " +
                "           <div class=\"col-12\">\r\n              <strong>COPIA CLIENTE</strong>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"row p-5 border m-2\">\r\n" +
                "          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>Recibi de:</strong>\r\n              </div>\r\n " +
                "             <div class=\"col-8 border-bottom border-black\">\r\n                <span>Recibi</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col-12\">\r\n " +
                "           <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <strong>La cantidad de:</strong>\r\n              </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n" +
                "                <span>cantidad</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n" +
                "              <div class=\"col-4\">\r\n                <strong>Concepto:</strong>\r\n              </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n " +
                "               <span>concepto</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n   " +
                "           <div class=\"col-4\">\r\n                <strong>Metodo de pago:</strong>\r\n              </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n     " +
                "           <span>Metodo</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col-12\">\r\n            <div class=\"row\">\r\n            " +
                "  <div class=\"col-4\">\r\n                <strong>Referencia de pago:</strong>\r\n              </div>\r\n              <div class=\"col-8 border-bottom border-black\">\r\n    " +
                "            <span>referencia</span>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"row m-5 text-center\">\r\n         " +
                " <div class=\"col-12\">\r\n            <div class=\"col-12 border-top border-black\">\r\n              <span>Firma de recibido</span>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </body>\r\n</html>\r\n";
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            await page.SetContentAsync(htmlContent);
            var pdfContent = await page.PdfStreamAsync(
                new PdfOptions
                {
                    Format = PaperFormat.Letter,
                    PrintBackground = true,
                    MarginOptions = new MarginOptions
                    {
                        Top = "5px",
                        Right = "5px",
                        Bottom = "5px",
                        Left = "5px"
                    }
                }
            );
            return File(pdfContent, "application/pdf", "converted.pdf");
        }
        #endregion
    }
}
