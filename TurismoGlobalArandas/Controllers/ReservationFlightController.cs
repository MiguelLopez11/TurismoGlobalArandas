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
                .Include(i => i.DepartureAirport)
                .Include(i => i.ArrivalAirport)
                .Include(i => i.Airline)
                .Include(i => i.Customers)
                .Where(w => !w.IsReservadedByHotel)
                .ToListAsync();
            return Ok(flights);
        }
        [HttpGet("ReservadedByReservationHotel")]
        public async Task<ActionResult<ReservationFlight>> getReservationFlightsReservadedByReservationHotel()
        {
            var flights = await _context.ReservationFlights
                .Include(i => i.DepartureAirport)
                .Include(i => i.ArrivalAirport)
                .Include(i => i.Airline)
                .Include(i => i.Customers)
                .Where(w => w.IsReservadedByHotel)
                .ToListAsync();
            return Ok(flights);
        }
        [HttpGet("{FlightId}")]
        public async Task<ActionResult> getReservationFlightById(int FlightId)
        {
            var flight = await _context.ReservationFlights
                .Include(i => i.DepartureAirport)
                .Include(i => i.ArrivalAirport)
                .Include(i => i.Airline)
                .Include(i => i.Customers)
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
            flight.Invoice = _context.GetInvoiceReservationFlight();
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
            FlightOld.PaymentLimitDate = reservationFlight.PaymentLimitDate;
            FlightOld.PaymentLimitDateProvider = reservationFlight.PaymentLimitDateProvider;
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
            FlightOld.IsReservadedByHotel = reservationFlight.IsReservadedByHotel;
            FlightOld.EmployeeId = reservationFlight.EmployeeId;
            FlightOld.ProviderId = reservationFlight.ProviderId;
            FlightOld.IsDeleted = reservationFlight.IsDeleted;

            var ReservationPaymentRelation = await _context.PaymentsRelationReservations
                .Include(i => i.reservationFlight)
                .FirstOrDefaultAsync(f => f.ReservationFlightId == FlightId);

            var PaymentProviders = await _context.PaymentProviders
                .Include(i => i.reservationFlight)
                .FirstOrDefaultAsync(f => f.ReservationFlightId == FlightId);

            if (ReservationPaymentRelation.AmountTotal == null && PaymentProviders.AmountTotal == null && reservationFlight.PriceNeto != null)
            {
                if (ReservationPaymentRelation.AmountMissing == null && PaymentProviders.AmountMissing == null)
                {
                    ReservationPaymentRelation.AmountMissing = reservationFlight.PriceNeto;
                    PaymentProviders.AmountMissing = reservationFlight.PriceNeto;
                }
                ReservationPaymentRelation.AmountTotal = reservationFlight.PriceNeto;
                PaymentProviders.AmountTotal = reservationFlight.PriceNeto;
                _context.PaymentsRelationReservations.Update(ReservationPaymentRelation);
                _context.PaymentProviders.Update(PaymentProviders);
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
        [HttpDelete("Delete/{FlightId}")]
        public async Task<IActionResult> RemoveReservationFlight(int FlightId)
        {
            var flight = await _context.ReservationFlights
                .FirstOrDefaultAsync(f => f.FlightId == FlightId);
            if (flight == null)
            {
                return NotFound();
            }
            var paymentRelation = await _context.PaymentsRelationReservations.FirstOrDefaultAsync(
                f => f.ReservationFlightId == FlightId
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
            var PaymentProviders = await _context.PaymentProviders
                .Where(w => w.ReservationFlightId == FlightId)
                .ToListAsync();
            foreach (var item in PaymentProviders)
            {
                _context.PaymentProviders.Remove(item);
                await _context.SaveChangesAsync();
            }
            _context.ReservationFlights.Remove(flight);
            await _context.SaveChangesAsync();
            return Ok("Vuelo eliminado");
        }
        [HttpGet("DescargarDatosEnPDF/{FlightId}")]
        public async Task<ActionResult> DescargarDatosEnPDF(int FlightId)
        {
            await new BrowserFetcher().DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(
                new LaunchOptions { Headless = true }
            );
            var reservation = await _context.ReservationFlights
                .Include(i => i.Employees)
                .Include(i => i.Customers)
                .Include(i => i.DepartureAirport)
                .Include(i => i.ArrivalAirport)
                .Include(i => i.Airline)
                .Include(i => i.Providers)
                .FirstOrDefaultAsync(f => f.FlightId == FlightId);
            await using var page = await browser.NewPageAsync();
            await page.EmulateMediaTypeAsync(MediaType.Screen);
            var htmlContent = $"<!DOCTYPE html>\r\n<html lang=\"en\">\r\n  <head>\r\n    <meta charset=\"UTF-8\" />\r\n    " +
                $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\" />\r\n    <title>Bootstrap Grid Example</title>\r\n    " +
                $"<link\r\n      rel=\"stylesheet\"\r\n      href=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css\"\r\n    />\r\n " +
                $" </head>\r\n  <style>\r\n    #row_campos {{\r\n      gap: 5px;\r\n      border: 2px solid #000000;\r\n      padding: 5px;\r\n    }}\r\n   " +
                $" #row_campos2 {{\r\n      gap: 5px;\r\n      border: 1px solid #dddddd;;\r\n      padding: 5px;\r\n   }}\r\n    " +
                $".custom-label {{\r\n      display: block;\r\n      margin-top: 5px; /* Ajusta el espacio entre p y label */\r\n    }}\r\n\r\n    " +
                $".custom-border {{\r\n      border: 1px solid #ccc;\r\n      padding: 5px; /* Ajusta el espacio dentro de las columnas */\r\n    }}\r\n    " +
                $"p {{\r\n      font-size: 14px;\r\n      font-family: 'Franklin Gothic Medium', 'Arial', Arial, sans-serif;\r\n      text-align: justify;\r\n    }}\r\n " +
                $"   input {{\r\n      height: 50px;\r\n    }}\r\n    #Reservation_Type {{\r\n      height: 40px;\r\n    }}\r\n\r\n    tr:nth-child(even) {{\r\n     " +
                $" background-color: #dddddd;\r\n    }}\r\n    .container {{\r\n      max-width: 90%;\r\n    }}\r\n  </style>\r\n  <body>\r\n    <div class=\"container text-center\">\r\n   " +
                $"   <div class=\"row p-1 justify-content-center\">\r\n        <div class=\"col-sm border p-2 text-white bg-secondary ml-5\">\r\n          <h6>Confirmación de Reserva</h6>\r\n " +
                $"       </div>\r\n        <div class=\"col-sm\">\r\n          <input\r\n            id=\"Reservation_Type\"\r\n            type=\"text\"\r\n          " +
                $"  class=\"form-control custom-border\"\r\n            value=\"RESERVACIÓN DE VUELO\"\r\n          />\r\n        </div>\r\n      </div>\r\n      <div id=\"row_campos\" class=\"row mt-3\">\r\n " +
                $"       <div class=\"col-sm text-center\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Nombre(s):</p>\r\n           " +
                $" </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n          " +
                $"      value=\"{reservation.Customers.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n         " +
                $" <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Apellidos:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n      " +
                $"        <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Customers.Lastname}\"\r\n              />\r\n " +
                $"           </div>\r\n          </div>\r\n        </div>\r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n          " +
                $"  <div class=\"col-3\">\r\n              <p class=\"b\">Clave de Confirmación:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n    " +
                $"            type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.ConfirmationKey}\"\r\n              />\r\n            </div>\r\n " +
                $"         </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n             " +
                $" <p class=\"b\">Precio público:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n              " +
                $"  class=\"form-control custom-border\"\r\n                value=\"{reservation.PricePublic}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n  " +
                $"      \r\n        <div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-2\">\r\n              " +
                $"<p class=\"b\">Precio neto:</p>\r\n            </div>\r\n            <div class=\"col-4\">\r\n              <input\r\n                type=\"text\"\r\n               " +
                $" class=\"form-control custom-border\"\r\n                value=\"{reservation.PriceNeto}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n " +
                $"       <div class=\"w-100\"></div>\r\n      </div>";
            if (reservation.isSimpleFlight == true)
            {
                htmlContent += $"<div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n       " +
                    $" <div id=\"row_campos2\"  class=\"row m-1\">\r\n          <div class=\"col\">\r\n           " +
                    $" <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <p class=\"b\">Fecha de reservación de vuelo:</p>\r\n   " +
                    $"           </div>\r\n              <div class=\"col-8\">\r\n                <input\r\n                  type=\"text\"\r\n   " +
                    $"               class=\"form-control custom-border\"\r\n                  value=\"{reservation.DateTravel}\"\r\n                />\r\n " +
                    $"             </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n  " +
                    $"            <div class=\"col-5\">\r\n                <p class=\"b\">Ruta de aeropuerto salida:</p>\r\n              </div>\r\n            " +
                    $"  <div class=\"col-7\">\r\n                <input\r\n                  type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n " +
                    $"                 value=\"{reservation.DepartureAirport.RouteName}\"\r\n                />\r\n              </div>\r\n            </div>\r\n          </div>\r\n " +
                    $"         <div class=\"w-100\"></div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n" +
                    $"                <p class=\"b\">Ruta de aeropuerto llegada:</p>\r\n              </div>\r\n              <div class=\"col-8\">\r\n                <input\r\n " +
                    $"                 type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n                  value=\"{reservation.ArrivalAirport.RouteName}\"\r\n " +
                    $"               />\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n" +
                    $"              <div class=\"col-4\">\r\n                <p class=\"b\">Aerolinea:</p>\r\n              </div>\r\n              <div class=\"col-8\">\r\n " +
                    $"               <input\r\n                  type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n                  value=\"{reservation.Airline.Name}\"\r\n " +
                    $"               />\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>";
            }
            else
            {
                var destinations = await _context.ReservationFlightDestinations
                    .Include(i => i.DepartureAirport)
                    .Include(i => i.ArrivalAirport)
                    .Include(i => i.Airline)
                    .Where(w => w.ReservationFlightId == reservation.FlightId)
                    .ToListAsync();
                foreach (var item in destinations)
                {
                    htmlContent += $"<div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n       " +
                    $" <div id=\"row_campos2\"  class=\"row m-1\">\r\n          <div class=\"col\">\r\n           " +
                    $" <div class=\"row\">\r\n              <div class=\"col-4\">\r\n                <p class=\"b\">Fecha de reservación de vuelo:</p>\r\n   " +
                    $"           </div>\r\n              <div class=\"col-8\">\r\n                <input\r\n                  type=\"text\"\r\n   " +
                    $"               class=\"form-control custom-border\"\r\n                  value=\"{item.DateTravel}\"\r\n                />\r\n " +
                    $"             </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n  " +
                    $"            <div class=\"col-5\">\r\n                <p class=\"b\">Ruta de aeropuerto salida:</p>\r\n              </div>\r\n            " +
                    $"  <div class=\"col-7\">\r\n                <input\r\n                  type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n " +
                    $"                 value=\"{item.DepartureAirport.RouteName}\"\r\n                />\r\n              </div>\r\n            </div>\r\n          </div>\r\n " +
                    $"         <div class=\"w-100\"></div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n              <div class=\"col-4\">\r\n" +
                    $"                <p class=\"b\">Ruta de aeropuerto llegada:</p>\r\n              </div>\r\n              <div class=\"col-8\">\r\n                <input\r\n " +
                    $"                 type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n                  value=\"{item.ArrivalAirport.RouteName}\"\r\n " +
                    $"               />\r\n              </div>\r\n            </div>\r\n          </div>\r\n          <div class=\"col\">\r\n            <div class=\"row\">\r\n" +
                    $"              <div class=\"col-4\">\r\n                <p class=\"b\">Aerolinea:</p>\r\n              </div>\r\n              <div class=\"col-8\">\r\n " +
                    $"               <input\r\n                  type=\"text\"\r\n                  class=\"form-control custom-border\"\r\n                  value=\"{item.Airline.Name}\"\r\n " +
                    $"               />\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </div>\r\n      </div>";
                }
            }
            htmlContent += $"<div id=\"row_campos\" style=\"gap: 10px\" class=\"row mt-3\">\r\n        <div class=\"col-sm\">\r\n         " +
                $" <div class=\"row\">\r\n            <div class=\"col-3\">\r\n              <p class=\"b\">Fecha compra:</p>\r\n           " +
                $" </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n                type=\"text\"\r\n               " +
                $" class=\"form-control custom-border\"\r\n                value=\"{reservation.DateSale}\"\r\n              />\r\n            " +
                $"</div>\r\n          </div>\r\n        </div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n           " +
                $" <div class=\"col-3\">\r\n              <p class=\"b\">Promotora:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n" +
                $"              <input\r\n                type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                " +
                $"value=\"{reservation.Providers.Name}\"\r\n              />\r\n            </div>\r\n          </div>\r\n        </div>\r\n        " +
                $"<div class=\"w-100\"></div>\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-3\">\r\n " +
                $"             <p class=\"b\">Agente:</p>\r\n            </div>\r\n            <div class=\"col-9\">\r\n              <input\r\n         " +
                $"       type=\"text\"\r\n                class=\"form-control custom-border\"\r\n                value=\"{reservation.Employees.Name + " " + reservation.Employees.Lastname}\"\r\n              />\r\n " +
                $"           </div>\r\n          </div>\r\n        </div>\r\n\r\n        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n   " +
                $"           <p>Plazo de pago a cliente:</p>\r\n            </div>\r\n            <div class=\"col-8\">\r\n              <input\r\n                type=\"text\"\r\n " +
                $"               class=\"form-control custom-border\"\r\n                value=\"{reservation.PaymentLimitDate}\"\r\n              />\r\n            </div>\r\n          </div>\r\n" +
                $"        </div>\r\n   <div class=\"w-100\"></div> \r\n     <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p>Método de pago cliente:</p>\r\n" +
                $"            </div>\r\n  \r\n          <div class=\"col-8\">\r\n              <textarea\r\n                        rows=\"4\"\r\n  " +
                $"              class=\"w-100 custom-border\"\r\n              >{reservation.PaymentMethodClient}</textarea>\r\n            </div>\r\n          </div>\r\n        </div>\r\n        " +
                $"        <div class=\"col-sm\">\r\n          <div class=\"row\">\r\n            <div class=\"col-4\">\r\n              <p>Método de pago agencia:</p>\r\n            </div>\r\n" +
                $"            <div class=\"col-8\">\r\n              <textarea\r\n                       rows=\"4\"\r\n                " +
                $"class=\"w-100 custom-border\"\r\n              >{reservation.PaymentMethodAgency}</textarea>\r\n            </div>\r\n          </div>\r\n        </div>\r\n\r\n    <script src=\"https://code.jquery.com/jquery-3.2.1.slim.min.js\"></script>\r\n " +
                $"   <script src=\"https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js\"></script>\r\n   " +
                $" <script src=\"https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js\"></script>\r\n  </body>\r\n</html>";
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
    }
}
